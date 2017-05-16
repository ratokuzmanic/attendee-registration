using DumpDays.AttendeeRegistration.Domain.Extensions;
using ent  = DumpDays.AttendeeRegistration.Domain.Entities;
using data = DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Domain.Mappers
{
    public class AttendeeMapper
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
                id:                     attendee.Id,
                firstName:              attendee.FirstName,
                lastName:               attendee.LastName,
                email:                  attendee.Email,
                birthdate:              attendee.Birthdate,
                workStatus:             attendee.WorkStatus,
                isAccreditationPrinted: attendee.IsAccreditationPrinted,
                createdOn:              attendee.CreatedOn
            );
        }

        public static ent::Attendee.StatisticsDetails MapStatisticsDetails(data::Attendee attendee)
        {
            return new ent::Attendee.StatisticsDetails
            (
                didRegisterOnline:      attendee.CreatedOn.DidRegisterOnline(),
                isAccreditationPrinted: attendee.IsAccreditationPrinted,
                hourOfRegistration:     attendee.CreatedOn.Hour,
                ageGroup:               attendee.Birthdate.GetAgeGroup(),
                workStatus:             attendee.WorkStatus
            );
        }
    }
}
