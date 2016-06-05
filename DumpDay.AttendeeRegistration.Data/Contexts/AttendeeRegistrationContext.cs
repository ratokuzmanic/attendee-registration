using System.Data.Entity;
using DumpDay.AttendeeRegistration.Data.Models;

namespace DumpDay.AttendeeRegistration.Data.Contexts
{
    public interface IAttendeeRegistrationContext
    {
        IDbSet<Attendee> Attendees { get; set; }
    }

    public class AttendeeRegistrationContext : DbContext, IAttendeeRegistrationContext
    {
        public virtual IDbSet<Attendee> Attendees { get; set; }

        public AttendeeRegistrationContext()
            : base("DumpDay.AttendeeRegistration")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AttendeeRegistrationContext>());
        }
    }
}
