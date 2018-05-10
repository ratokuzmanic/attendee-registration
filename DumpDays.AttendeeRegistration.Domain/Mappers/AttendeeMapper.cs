using DumpDays.AttendeeRegistration.Domain.Extensions;
using DumpDays.AttendeeRegistration.Domain.Entities;
using data = DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Domain.Mappers
{
    public class AttendeeMapper
    {
        public static Attendee.ShortDetails MapShortDetails(data::Attendee attendee)
        {
            return new Attendee.ShortDetails
            (
                id:        attendee.Id,
                firstName: attendee.FirstName,
                lastName:  attendee.LastName,
                birthdate: attendee.Birthdate
            );
        }

        public static Attendee.LongDetails MapLongDetails(data::Attendee attendee)
        {
            return new Attendee.LongDetails
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

        public static Attendee.StatisticsDetails MapStatisticsDetails(data::Attendee attendee)
        {
            return new Attendee.StatisticsDetails
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
