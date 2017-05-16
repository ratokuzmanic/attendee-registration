using System;
using System.Collections.Generic;

namespace DumpDays.AttendeeRegistration.Domain.Extensions
{
    public enum AgeGroups
    {
        Todler,
        Child,
        Teenager,
        YoungAdult,
        Adult,
        OldAdult,
        Senior
    }

    public static class DateTimeExtension
    {
        private static readonly Dictionary<AgeGroups, int> UpperLimitForAgeGroups = new Dictionary<AgeGroups, int>()
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
        {
            foreach (var upperLimitForAgeGroup in UpperLimitForAgeGroups)
            {
                if (DateTime.Now.Date <= birthdate.AddYears(upperLimitForAgeGroup.Value))
                    return upperLimitForAgeGroup.Key;
            }
            return AgeGroups.Senior;
        }
    }
}
