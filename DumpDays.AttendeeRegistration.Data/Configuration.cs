using System.Configuration;

namespace DumpDays.AttendeeRegistration.Data
{
    public static class Configuration
    {
        public static string RootAdminUsername
            => ReadFromConfig("RootAdminUsername");

        private static string ReadFromConfig(string field)
            => ConfigurationManager.AppSettings[field];
    }
}
