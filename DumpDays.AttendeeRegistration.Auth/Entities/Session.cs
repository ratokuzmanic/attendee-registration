using System;

namespace DumpDays.AttendeeRegistration.Auth.Entities
{
    public class Session
    {
        public Guid             Id        { get; }
        public DateTime         StartedOn { get; }
        public User.LongDetails User      { get; }

        public Session
        (
            Guid             id,
            DateTime         startedOn,
            User.LongDetails user
        )
        {
            Id        = id;
            StartedOn = startedOn;
            User      = user;
        }
    }
}
