using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Domain.Extensions;

namespace DumpDays.AttendeeRegistration.Domain.Entities
{
    public abstract class Attendee
    {
        public class ShortDetails
        {
            public Guid   Id        { get; }
            public string FirstName { get; }
            public string LastName  { get; }
            public string FullName  { get; }
            public bool   IsMinor   { get; }

            public ShortDetails
            (
                Guid     id,
                string   firstName,
                string   lastName,
                DateTime birthdate
            )
            {
                Id        = id;
                FirstName = firstName;
                LastName  = lastName;
                FullName  = $"{FirstName} {LastName}";
                IsMinor   = birthdate.IsMinor();
            }
        }

        public class LongDetails
        {
            public Guid         Id                     { get; }
            public string       FirstName              { get; }
            public string       LastName               { get; }
            public string       FullName               { get; }
            public string       Email                  { get; }
            public DateTime     Birthdate              { get; }
            public WorkStatuses WorkStatus             { get; }
            public bool         IsAccreditationPrinted { get; }
            public DateTime     CreatedOn              { get; }

            public LongDetails
            (
                Guid         id,
                string       firstName,
                string       lastName,
                string       email,
                DateTime     birthdate,
                WorkStatuses workStatus,
                bool         isAccreditationPrinted,
                DateTime     createdOn
            )
            {
                Id                     = id;
                FirstName              = firstName;
                LastName               = lastName;
                FullName               = $"{FirstName} {LastName}";
                Email                  = email;
                Birthdate              = birthdate;
                WorkStatus             = workStatus;
                IsAccreditationPrinted = isAccreditationPrinted;
                CreatedOn              = createdOn;
            }

            public string Serialize()
                => string.Join(";",
                    FirstName,
                    LastName,
                    Email,
                    Birthdate.ToString("d.M.yyyy.", null),
                    Enum.GetName(typeof(WorkStatuses), WorkStatus),
                    IsAccreditationPrinted.ToString(),
                    CreatedOn.ToString(Constants.DateTimeFormat, null));
        }

        public class StatisticsDetails
        {
            public bool         DidRegisterOnline      { get; }
            public bool         IsAccreditationPrinted { get; }
            public int          HourOfRegistration     { get; }
            public AgeGroups    AgeGroup               { get; }
            public WorkStatuses WorkStatus             { get; }

            public StatisticsDetails
            (
                bool         didRegisterOnline,
                bool         isAccreditationPrinted,
                int          hourOfRegistration,
                AgeGroups    ageGroup,
                WorkStatuses workStatus
            )
            {
                DidRegisterOnline      = didRegisterOnline;
                IsAccreditationPrinted = isAccreditationPrinted;
                HourOfRegistration     = hourOfRegistration;
                AgeGroup               = ageGroup;
                WorkStatus             = workStatus;
            }
        }

        public static string Serialize(IList<LongDetails> attendees)
            => attendees.Select(attendee => attendee.Serialize())
                .Aggregate(
                    seed: new StringBuilder().AppendLine(string.Join(";", 
                        "Firstname", 
                        "Lastname", 
                        "Email", 
                        "Birthday", 
                        "Work status", 
                        "Is accreditation printed", 
                        "Created on")),
                    func: (aggregate, attendee) => aggregate.AppendLine(attendee))
                .ToString();
    }
}
