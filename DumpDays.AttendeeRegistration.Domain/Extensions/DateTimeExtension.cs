using System;
using System.Collections.Generic;
using System.Linq;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Domain.Extensions
{
    public static class DateTimeExtension
    {
        private static readonly Dictionary<AgeGroups, int> AgeGroupUpperLimit = new Dictionary<AgeGroups, int>()
        {
            { AgeGroups.Todler,     5 },
            { AgeGroups.Child,      12 },
            { AgeGroups.Teenager,   19 },
            { AgeGroups.YoungAdult, 29 },
            { AgeGroups.Adult,      44 },
            { AgeGroups.OldAdult,   59 }
        };

        public static bool IsMinor(this DateTime birthdate)
            => DateTime.Now.Date <= birthdate.AddYears(Configuration.AgeOfMajority);

        public static bool DidRegisterOnline(this DateTime createdOn)
            => createdOn < Configuration.EventStart;

        public static AgeGroups GetAgeGroup(this DateTime birthdate)
            => AgeGroupUpperLimit.Any(_ => DateTime.Now.Date <= birthdate.AddYears(_.Value))
                ? AgeGroupUpperLimit.First(_ => DateTime.Now.Date <= birthdate.AddYears(_.Value)).Key
                : AgeGroups.Senior;
    }
}
