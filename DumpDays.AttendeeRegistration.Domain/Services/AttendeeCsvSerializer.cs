using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DumpDays.AttendeeRegistration.Data.Models;
using Attendee = DumpDays.AttendeeRegistration.Domain.Entities.Attendee;

namespace DumpDays.AttendeeRegistration.Domain.Services
{
    public class AttendeeCsvSerializerService
    {
        public static string Serialize(IList<Attendee.LongDetails> allAttendees)
        {
            return
                allAttendees.Select(SerializeAttendee)
                .Aggregate(
                    seed: new StringBuilder().AppendLine("Firstname;Lastname;Email;Birthday;Work status;Is accreditation printed;Created on;"),
                    func: (aggregate, currentAttendee) => aggregate.AppendLine(currentAttendee))
                .ToString();
        }

        private static string SerializeAttendee(Attendee.LongDetails attendee)
        {
            return string.Join(";", new[]
            {
                attendee.FirstName,
                attendee.LastName,
                attendee.Email,
                attendee.Birthdate.ToString("d.M.yyyy.", CultureInfo.InvariantCulture),
                Enum.GetName(typeof(WorkStatuses), attendee.WorkStatus),
                attendee.IsAccreditationPrinted.ToString(),
                attendee.CreatedOn.ToString("d.M.yyyy. HH:mm", CultureInfo.InvariantCulture),
            });
        }
    }
}
