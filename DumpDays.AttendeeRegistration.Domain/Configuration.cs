using System;
using System.Configuration;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Domain
{
    public static class Configuration
    {
        public static int AgeOfMajority 
            => ReadFromConfigAsInt("AgeOfMajority");

        public static DateTime EventStart
            => ReadFromConfigAsDateTime("EventStart");

        private static int ReadFromConfigAsInt(string field)
            => int.Parse(ConfigurationManager.AppSettings[field]);

        private static DateTime ReadFromConfigAsDateTime(string field)
            => DateTime.ParseExact(ConfigurationManager.AppSettings[field], Constants.DateTimeFormat, null);
    }
}
