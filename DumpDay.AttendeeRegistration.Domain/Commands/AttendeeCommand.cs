using System;
using FluentAssertions;
using DumpDay.AttendeeRegistration.Data.Models;
using ent  = DumpDay.AttendeeRegistration.Domain.Entities;
using data = DumpDay.AttendeeRegistration.Data.Contexts;

namespace DumpDay.AttendeeRegistration.Domain.Commands
{
    using map = Mappers.Mappers;

    public interface IAttendeeCommand
    {
        ent::Attendee.ShortDetails Execute(AttendeeCommand.Create command);
        void                       Execute(AttendeeCommand.Delete command);
    }

    public class AttendeeCommand : IAttendeeCommand
    {
        private readonly data::AttendeeRegistrationContext _attendeeRegistrationContext;

        public AttendeeCommand(data::AttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public ent::Attendee.ShortDetails Execute(Create command)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Create();

            attendee.FirstName   = command.FirstName;
            attendee.LastName    = command.LastName;
            attendee.Birthdate   = command.Birthdate;
            attendee.WorkStatus  = command.WorkStatus;
            attendee.Institution = command.Institution;
            attendee.CreatedOn   = command.CreatedOn;

            _attendeeRegistrationContext.Attendees.Add(attendee);
            _attendeeRegistrationContext.SaveChanges();

            return map.Attendee.MapShortDetails(attendee);
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
            public DateTime     Birthdate   { get; }
            public string       Institution { get; }
            public WorkStatuses WorkStatus  { get; }
            public DateTime     CreatedOn   { get; }

            public Create
            (
                string       firstName,
                string       lastName,
                DateTime     birthdate,
                string       institution,
                WorkStatuses workStatus,
                DateTime     createdOn
            )
            {
                firstName .Should().NotBeNullOrEmpty();
                lastName  .Should().NotBeNullOrEmpty();
                workStatus.Should().NotBeNull();

                FirstName   = firstName;
                LastName    = lastName;
                Birthdate   = birthdate;
                Institution = institution;
                WorkStatus  = workStatus;
                CreatedOn   = createdOn;
            }
        }

        public class Delete
        {
            public int Id { get; }

            public Delete
            (
                int id
            )
            {
                id.Should().BeGreaterOrEqualTo(0);
                Id = id;
            }
        }
    }
}
