using System.Data.Entity;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Data.Contexts
{
    public interface IAttendeeRegistrationContext
    {
        IDbSet<Attendee> Attendees { get; set; }
        IDbSet<User>     Users     { get; set; }
        IDbSet<Session>  Sessions  { get; set; }
    }

    public class AttendeeRegistrationContext : DbContext, IAttendeeRegistrationContext
    {
        public virtual IDbSet<Attendee> Attendees { get; set; }
        public virtual IDbSet<User>     Users     { get; set; }
        public virtual IDbSet<Session>  Sessions  { get; set; }

        public AttendeeRegistrationContext()
            : base("name=DumpDays.AttendeeRegistration")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExistsAndSeedAdminUser());
        }

        public class CreateDatabaseIfNotExistsAndSeedAdminUser : DropCreateDatabaseIfModelChanges<AttendeeRegistrationContext>
        {
            protected override void Seed(AttendeeRegistrationContext context)
            {
                var rootAdminUser = new User()
                {
                    Username = Data.Configuration.RootAdminUsername,
                    Password = "$2a$12$TdXibCH/0/Ul1NewTIi05.c0m7gvEZghzyMk1i7SfnLyCjYFrzv8m",
                    IsSetup  = true,
                    IsActive = true,
                    Role     = Roles.Administrator
                };

                context.Users.Add(rootAdminUser);
                base.Seed(context);
            }
        }
    }
}
