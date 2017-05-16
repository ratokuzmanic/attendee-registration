using System;
using System.Configuration;
using System.Globalization;

namespace DumpDays.AttendeeRegistration.Domain
{
    public class Configuration
    {
        public static int AgeOfMajority 
            => ReadFromConfigAsInt("AgeOfMajority");

        public static int MinimumLengthOfPassword 
            => ReadFromConfigAsInt("MinimumLengthOfPassword");

        public static DateTime EventStart
            => ReadFromConfigAsDateTime("EventStart");

        private static int ReadFromConfigAsInt(string field)
            => int.Parse(ConfigurationManager.AppSettings[field]);

        private static DateTime ReadFromConfigAsDateTime(string field)
            => DateTime.ParseExact(ConfigurationManager.AppSettings[field], "d.M.yyyy. HH:mm", CultureInfo.InvariantCulture);
    }
}
