using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Auth.Services;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Data.Models;
using FluentAssertions;

namespace DumpDays.AttendeeRegistration.Domain.Commands
{
    public interface IUserCommand
    {
        void Execute(UserCommand.Create          command);
        void Execute(UserCommand.SetupPassword   command);
        void Execute(UserCommand.TriggerActivity command);
        void Execute(UserCommand.Delete          command);
    }

    public class UserCommand : IUserCommand
    {
        private readonly AttendeeRegistrationContext _attendeeRegistrationContext;

        public UserCommand(AttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public void Execute(Create command)
        {
            var isThereUserWithSameUsername =
                _attendeeRegistrationContext.Users
                .Any(_ => _.Username == command.Username);
            if (isThereUserWithSameUsername) return;

            var user = _attendeeRegistrationContext.Users.Create();
            var randomPassword = RandomPasswordGenerator.Generate();

            user.Username = command.Username;
            user.Password = PasswordHashingService.Hash(randomPassword);
            user.Role     = command.Role;

            _attendeeRegistrationContext.Users.Add(user);
            _attendeeRegistrationContext.SaveChanges();
        }

        public void Execute(SetupPassword command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            user.Should().NotBeNull();

            if (user.IsSetup) return;

            user.Password = PasswordHashingService.Hash(command.Password);
            user.IsSetup  = true;
            user.IsActive = true;

            _attendeeRegistrationContext.SaveChanges();
        }

        public void Execute(TriggerActivity command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            user         .Should().NotBeNull();
            user.Username.Should().NotBe("admin");

            user.IsActive = !user.IsActive;

            _attendeeRegistrationContext.SaveChanges();
        }

        public void Execute(Delete command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            user         .Should().NotBeNull();
            user.Username.Should().NotBe("admin");

            _attendeeRegistrationContext.Users.Remove(user);
            _attendeeRegistrationContext.SaveChanges();
        }

        public class Create
        {
            public string Username { get; }
            public Roles  Role     { get; }

            public Create
            (
                string username,
                Roles  role
            )
            {
                username.Should().NotBeNullOrEmpty();
                role    .Should().NotBeNull();

                Username = username;
                Role     = role;
            }
        }

        public class SetupPassword
        {
            public Guid   Id       { get; }
            public string Password { get; }

            public SetupPassword
            (
                Guid   id,
                string password
            )
            {
                id             .Should().NotBeEmpty();
                password       .Should().NotBeNullOrEmpty();
                password.Length.Should().BeGreaterOrEqualTo(Configuration.MinimumLengthOfPassword);

                Id = id;
                Password = password;
            }
        }

        public class TriggerActivity
        {
            public Guid Id { get; }

            public TriggerActivity
            (
                Guid id
            )
            {
                id.Should().NotBeEmpty();
                Id = id;
            }
        }

        public class Delete
        {
            public Guid Id { get; }

            public Delete
            (
                Guid id
            )
            {
                id.Should().NotBeEmpty();
                Id = id;
            }
        }
    }
}
