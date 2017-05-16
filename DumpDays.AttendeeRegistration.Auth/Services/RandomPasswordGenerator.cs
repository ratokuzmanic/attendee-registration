using Provider = System.Web.Security.Membership;

namespace DumpDays.AttendeeRegistration.Auth.Services
{
    public class RandomPasswordGenerator
    {
        public static string Generate()
            => Provider.GeneratePassword 
            (
                Configuration.DefaultRandomPasswordLength, 
                Configuration.DefaultRandomPasswordLength / 2
            );
    }
}
