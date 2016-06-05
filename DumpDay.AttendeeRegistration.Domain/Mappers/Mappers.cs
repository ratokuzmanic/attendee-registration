using ent  = DumpDay.AttendeeRegistration.Domain.Entities;
using data = DumpDay.AttendeeRegistration.Data.Models;

namespace DumpDay.AttendeeRegistration.Domain.Mappers
{
    public abstract class Mappers
    {
        public class Attendee
        {
            public static ent::Attendee.ShortDetails MapShortDetails(data::Attendee attendee)
            {
                return new ent::Attendee.ShortDetails
                (
                    id:        attendee.Id,
                    firstName: attendee.FirstName,
                    lastName:  attendee.LastName,
                    birthdate: attendee.Birthdate
                );
            }

            public static ent::Attendee.LongDetails MapLongDetails(data::Attendee attendee)
            {
                return new ent::Attendee.LongDetails
                (
                    id:          attendee.Id,
                    firstName:   attendee.FirstName,
                    lastName:    attendee.LastName,
                    birthdate:   attendee.Birthdate,
                    institution: attendee.Institution,
                    workStatus:  attendee.WorkStatus,
                    createdOn:   attendee.CreatedOn
                );
            }
        }
        
    }
}
