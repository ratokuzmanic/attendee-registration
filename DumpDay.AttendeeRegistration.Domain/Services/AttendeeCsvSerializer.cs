using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DumpDay.AttendeeRegistration.Data.Models;
using ent = DumpDay.AttendeeRegistration.Domain.Entities;

namespace DumpDay.AttendeeRegistration.Domain.Services
{
    public class AttendeeCsvSerializerService
    {
        public static string Serialize(IList<ent::Attendee.LongDetails> allAttendees)
        {
            return
                allAttendees.Select(SerializeAttendee)
                .Aggregate(
                    seed: new StringBuilder().AppendLine("Firstname;Lastname;Birthday;Work status;Institution;Created on;"),
                    func: (aggregate, currentAttendee) => aggregate.AppendLine(currentAttendee))
                .ToString();
        }

        private static string SerializeAttendee(ent::Attendee.LongDetails attendee)
        {
            return string.Join(";", new[]
            {
                attendee.FirstName,
                attendee.LastName,
                attendee.Birthdate.ToString("d.M.yyyy.", CultureInfo.InvariantCulture),
                Enum.GetName(typeof(WorkStatuses), attendee.WorkStatus),
                attendee.Institution,
                attendee.CreatedOn.ToString("d.M.yyyy. HH:mm", CultureInfo.InvariantCulture),
            });
        }
    }
}
