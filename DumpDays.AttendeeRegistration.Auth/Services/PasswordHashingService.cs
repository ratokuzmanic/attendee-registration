using Crypt = BCrypt.Net.BCrypt;

namespace DumpDays.AttendeeRegistration.Auth.Services
{
    public class PasswordHashingService
    {
        public static string Hash(string password)
            => Crypt.HashPassword(password, Crypt.GenerateSalt(Configuration.WorkFactor));

        public static bool Validate(string password, string passwordHash)
            => Crypt.Verify(password, passwordHash);
    }
}
