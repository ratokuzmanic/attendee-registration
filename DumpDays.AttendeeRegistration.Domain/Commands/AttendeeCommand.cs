using System;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Data.Models;
using FluentAssertions;
using Attendee = DumpDays.AttendeeRegistration.Domain.Entities.Attendee;

namespace DumpDays.AttendeeRegistration.Domain.Commands
{
    using map = Mappers.AttendeeMapper;

    public interface IAttendeeCommand
    {
        Attendee.ShortDetails Execute(AttendeeCommand.Create command);
        Attendee.ShortDetails Execute(AttendeeCommand.Print  command);
        void                  Execute(AttendeeCommand.Delete command);
    }

    public class AttendeeCommand : IAttendeeCommand
    {
        private readonly AttendeeRegistrationContext _attendeeRegistrationContext;

        public AttendeeCommand(AttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public Attendee.ShortDetails Execute(Create command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Create();

            attendee.FirstName  = command.FirstName;
            attendee.LastName   = command.LastName;
            attendee.Email      = command.Email;
            attendee.Birthdate  = command.Birthdate;
            attendee.WorkStatus = command.WorkStatus;
            attendee.CreatedOn  = command.CreatedOn;

            _attendeeRegistrationContext.Attendees.Add(attendee);
            _attendeeRegistrationContext.SaveChanges();

            return map.MapShortDetails(attendee);
        }

        public Attendee.ShortDetails Execute(Print command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(command.Id);
            attendee.Should().NotBeNull();

            attendee.IsAccreditationPrinted = true;

            _attendeeRegistrationContext.SaveChanges();
            return map.MapShortDetails(attendee);
        }

        public void Execute(Delete command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(command.Id);
            attendee.Should().NotBeNull();

            _attendeeRegistrationContext.Attendees.Remove(attendee);
            _attendeeRegistrationContext.SaveChanges();
        }

        public class Create
        {
            public string       FirstName   { get; }
            public string       LastName    { get; }
            public string       Email       { get; }
            public DateTime     Birthdate   { get; }
            public WorkStatuses WorkStatus  { get; }
            public DateTime     CreatedOn   { get; }

            public Create
            (
                string       firstName,
                string       lastName,
                string       email,
                DateTime     birthdate,
                WorkStatuses workStatus,
                DateTime     createdOn
            )
            {
                firstName .Should().NotBeNullOrEmpty();
                lastName  .Should().NotBeNullOrEmpty();
                email     .Should().NotBeNullOrEmpty();
                workStatus.Should().NotBeNull();

                FirstName   = firstName;
                LastName    = lastName;
                Email       = email;
                Birthdate   = birthdate;
                WorkStatus  = workStatus;
                CreatedOn   = createdOn;
            }
        }

        public class Print
        {
            public Guid Id { get; }

            public Print
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
