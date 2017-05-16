using System.Data.Entity;
using DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Data.Contexts
{
    public interface IAttendeeRegistrationContext
    {
        IDbSet<Attendee> Attendees { get; set; }
        IDbSet<User>     Users     { get; set; }
    }

    public class AttendeeRegistrationContext : DbContext, IAttendeeRegistrationContext
    {
        public virtual IDbSet<Attendee> Attendees { get; set; }
        public virtual IDbSet<User>     Users     { get; set; }

        public AttendeeRegistrationContext()
            : base("name=DumpDays.AttendeeRegistration")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExistsAndSeedAdminUser());
        }

        public class CreateDatabaseIfNotExistsAndSeedAdminUser : CreateDatabaseIfNotExists<AttendeeRegistrationContext>
        {
            protected override void Seed(AttendeeRegistrationContext context)
            {
                var adminUser = new User()
                {
                    Username = "admin",
                    Password = "INSERT_HASH_OF_ADMIN_PASSWORD_HERE",
                    IsSetup  = true,
                    IsActive = true,
                    Role     = Roles.Administrator
                };

                context.Users.Add(adminUser);
                base.Seed(context);
            }
        }
    }
}
