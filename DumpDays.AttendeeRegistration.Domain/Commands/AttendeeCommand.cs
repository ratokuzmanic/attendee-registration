using System;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Domain.Mappers;
using DumpDays.AttendeeRegistration.Domain.Entities;

namespace DumpDays.AttendeeRegistration.Domain.Commands
{
    public interface IAttendeeCommand
    {
        Attendee.ShortDetails         Execute(AttendeeCommand.Create command);
        IMaybe<Attendee.ShortDetails> Execute(AttendeeCommand.Print  command);
        ActionResult                  Execute(AttendeeCommand.Delete command);
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

            return AttendeeMapper.MapShortDetails(attendee);
        }

        public IMaybe<Attendee.ShortDetails> Execute(Print command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(command.Id);
            if (attendee == null) return None<Attendee.ShortDetails>.Exists;

            attendee.IsAccreditationPrinted = true;

            _attendeeRegistrationContext.SaveChanges();
            return Some<Attendee.ShortDetails>.Exists(AttendeeMapper.MapShortDetails(attendee));
        }

        public ActionResult Execute(Delete command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(command.Id);
            if(attendee == null) return ActionResult.Failure;

            _attendeeRegistrationContext.Attendees.Remove(attendee);
            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
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
