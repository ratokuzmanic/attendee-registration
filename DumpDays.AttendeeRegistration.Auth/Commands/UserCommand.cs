using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Auth.Services;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;

namespace DumpDays.AttendeeRegistration.Auth.Commands
{
    public interface IUserCommand
    {
        ActionResult Execute(UserCommand.Create          command);
        ActionResult Execute(UserCommand.SetupPassword   command);
        ActionResult Execute(UserCommand.TriggerActivity command);
        ActionResult Execute(UserCommand.Delete          command);
    }

    public class UserCommand : IUserCommand
    {
        private readonly AttendeeRegistrationContext _attendeeRegistrationContext;

        public UserCommand(AttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public ActionResult Execute(Create command)
        {
            var isUsernameAlreadyTaken =
                _attendeeRegistrationContext.Users
                .Any(_ => _.Username == command.Username);
            if (isUsernameAlreadyTaken) return ActionResult.Failure;

            var user = _attendeeRegistrationContext.Users.Create();
            var randomPassword = PasswordService.GenerateRandom();

            user.Username = command.Username;
            user.Password = PasswordService.Hash(randomPassword);
            user.Role     = command.Role;

            _attendeeRegistrationContext.Users.Add(user);
            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
        }

        public ActionResult Execute(SetupPassword command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            if(user == null) return ActionResult.Failure;

            var isNewPasswordValid = PasswordService.IsCandidate(command.Password);
            if (user.IsSetup || !isNewPasswordValid) return ActionResult.Failure;

            user.Password = PasswordService.Hash(command.Password);
            user.IsSetup  = true;
            user.IsActive = true;

            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
        }

        public ActionResult Execute(TriggerActivity command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            if (user == null) return ActionResult.Failure;
            if (user.Username == Configuration.RootAdminUsername) return ActionResult.Failure;

            user.IsActive = !user.IsActive;

            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
        }

        public ActionResult Execute(Delete command)
        {
            var user = _attendeeRegistrationContext.Users.Find(command.Id);
            if (user == null) return ActionResult.Failure;
            if (user.Username == Configuration.RootAdminUsername) return ActionResult.Failure;

            _attendeeRegistrationContext.Users.Remove(user);
            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
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
                Id = id;
            }
        }
    }
}
