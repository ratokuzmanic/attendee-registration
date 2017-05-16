using System.Configuration;
using System.Text;

namespace DumpDays.AttendeeRegistration.Auth
{
    public class Configuration
    {
        public static byte[] AuthSecretKey
            => ReadFromConfigAsByteStream("AuthSecretKey");

        public static int WorkFactor
            => ReadFromConfigAsInt("WorkFactor");

        public static int DefaultRandomPasswordLength
            => ReadFromConfigAsInt("DefaultRandomPasswordLength");

        private static int ReadFromConfigAsInt(string field)
            => int.Parse(ConfigurationManager.AppSettings[field]);

        private static byte[] ReadFromConfigAsByteStream(string field)
            => Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings[field]);
    }
}
