using System;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Auth.Mappers;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;

namespace DumpDays.AttendeeRegistration.Auth.Repositories
{
    public interface ISessionRepository
    {
        IMaybe<Session> MaybeGetOne(Guid id);
    }

    public class SessionRepository : ISessionRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public SessionRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public IMaybe<Session> MaybeGetOne(Guid id)
        {
            var maybeSession = _attendeeRegistrationContext.Sessions.Find(id);

            return maybeSession == null
                ? None<Session>.Exists
                : Some<Session>.Exists(SessionMapper.Map(maybeSession));
        }
    }
}
