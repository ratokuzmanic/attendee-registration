using Crypt = BCrypt.Net.BCrypt;
using Provider = System.Web.Security.Membership;

namespace DumpDays.AttendeeRegistration.Auth.Services
{
    public static class PasswordService
    {
        public static string Hash(string password)
            => Crypt.HashPassword(password, Crypt.GenerateSalt(Configuration.WorkFactor));

        public static bool Validate(string password, string passwordHash)
            => Crypt.Verify(password, passwordHash);

        public static bool IsCandidate(string possiblePassword)
            => possiblePassword.Length >= Configuration.MinimumLengthOfPassword;

        public static string GenerateRandom()
            => Provider.GeneratePassword
            (
                Configuration.DefaultRandomPasswordLength,
                Configuration.DefaultRandomPasswordLength / 2
            );
    }
}
