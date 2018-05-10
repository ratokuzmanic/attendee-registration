using DumpDays.AttendeeRegistration.Auth.Entities;
using data = DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Auth.Mappers
{
    public class SessionMapper
    {
        public static Session Map(data::Session session)
        {
            return new Session
            (
                id:        session.Id,
                startedOn: session.StartedOn,
                user:      UserMapper.MapLongDetails(session.User)
            );
        }
    }
}
