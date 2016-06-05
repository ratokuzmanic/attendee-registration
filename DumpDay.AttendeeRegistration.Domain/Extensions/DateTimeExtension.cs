using System;

namespace DumpDay.AttendeeRegistration.Domain.Extensions
{
    public static class DateTimeExtension
    {
        private const int AgeOfMajority = 18;

        public static bool IsMinor(this DateTime birthdate)
            => DateTime.Now.Date < birthdate.AddYears(AgeOfMajority);
    }
}
