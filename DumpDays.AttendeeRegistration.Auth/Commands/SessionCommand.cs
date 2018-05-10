using System;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Auth.Mappers;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;

namespace DumpDays.AttendeeRegistration.Auth.Commands
{
    public interface ISessionCommand
    {
        Session      Execute(SessionCommand.Create command);
        ActionResult Execute(SessionCommand.Delete command);
    }

    public class SessionCommand : ISessionCommand
    {
        private readonly AttendeeRegistrationContext _attendeeRegistrationContext;

        public SessionCommand(AttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public Session Execute(Create command)
        {
            var session = _attendeeRegistrationContext.Sessions.Create();
            session.User = _attendeeRegistrationContext.Users.Find(command.User.Id);
            session.StartedOn = command.StartedOn;

            _attendeeRegistrationContext.Sessions.Add(session);
            _attendeeRegistrationContext.SaveChanges();

            return SessionMapper.Map(session);
        }

        public ActionResult Execute(Delete command)
        {
            var session = _attendeeRegistrationContext.Sessions.Find(command.Id);
            if(session == null) return ActionResult.Failure;
            
            _attendeeRegistrationContext.Sessions.Remove(session);
            _attendeeRegistrationContext.SaveChanges();
            return ActionResult.Success;
        }

        public class Create
        {
            public User.LongDetails User      { get; }
            public DateTime         StartedOn { get; }

            public Create
            (
                User.LongDetails user,
                DateTime         startedOn
            )
            {
                User      = user;
                StartedOn = startedOn;
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
