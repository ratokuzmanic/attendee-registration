using ent  = DumpDays.AttendeeRegistration.Domain.Entities;
using data = DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Domain.Mappers
{
    public class UserMapper
    {
        public static ent::User.ShortDetails MapShortDetails(data::User user)
        {
            return new ent::User.ShortDetails
            (
                username: user.Username,
                role:     user.Role
            );
        }

        public static ent::User.LongDetails MapLongDetails(data::User user)
        {
            return new ent::User.LongDetails
            (
                id:       user.Id,
                username: user.Username,
                role:     user.Role,
                isActive: user.IsActive,
                isSetup:  user.IsSetup
            );
        }
    }
}
