using System;
using System.Configuration;
using System.Text;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Auth
{
    public static class Configuration
    {
        public static byte[] SigningKey
            => ReadFromConfigAsByteStream("SigningKey");

        public static string TokenDateTimeFormat
            => ReadFromConfig("TokenDateTimeFormat");

        public static TimeSpan BearerTokenValiditySpan
            => ReadFromConfigAsTimeSpan("BearerTokenValiditySpan");

        public static TimeSpan RefreshTokenValiditySpan
            => ReadFromConfigAsTimeSpan("RefreshTokenValiditySpan");

        public static string RootAdminUsername
            => ReadFromConfig("RootAdminUsername");

        public static int WorkFactor
            => ReadFromConfigAsInt("WorkFactor");

        public static int DefaultRandomPasswordLength
            => ReadFromConfigAsInt("DefaultRandomPasswordLength");

        public static int MinimumLengthOfPassword
            => ReadFromConfigAsInt("MinimumLengthOfPassword");

        private static string ReadFromConfig(string field)
            => ConfigurationManager.AppSettings[field];

        private static int ReadFromConfigAsInt(string field)
            => int.Parse(ConfigurationManager.AppSettings[field]);

        private static byte[] ReadFromConfigAsByteStream(string field)
            => Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings[field]);

        private static TimeSpan ReadFromConfigAsTimeSpan(string field)
            => TimeSpan.ParseExact(ConfigurationManager.AppSettings[field], Constants.TimeSpanFormat, null);
    }
}
