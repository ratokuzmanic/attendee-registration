using DumpDays.AttendeeRegistration.Auth.Entities;
using data = DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Auth.Mappers
{
    public class UserMapper
    {
        public static User.ShortDetails MapShortDetails(data::User user)
        {
            return new User.ShortDetails
            (
                username: user.Username,
                role:     user.Role
            );
        }

        public static User.LongDetails MapLongDetails(data::User user)
        {
            return new User.LongDetails
            (
                id:       user.Id,
                username: user.Username,
                password: user.Password,
                role:     user.Role,
                isActive: user.IsActive,
                isSetup:  user.IsSetup
            );
        }
    }
}
