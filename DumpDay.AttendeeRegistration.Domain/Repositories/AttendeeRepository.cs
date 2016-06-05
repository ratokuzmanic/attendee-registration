using System.Linq;
using FluentAssertions;
using DumpDay.AttendeeRegistration.Data.Contexts;
using ent = DumpDay.AttendeeRegistration.Domain.Entities;

namespace DumpDay.AttendeeRegistration.Domain.Repositories
{
    using map = Mappers.Mappers;

    public interface IAttendeeRepository
    {
        ent::Attendee.ShortDetails             GetOne_shortDetails(int id);
        IQueryable<ent::Attendee.ShortDetails> GetAll_shortDetails();
        IQueryable<ent::Attendee.LongDetails>  GetAll_longDetails();
    }

    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public AttendeeRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public ent::Attendee.ShortDetails GetOne_shortDetails(int id)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(id);
            attendee.Should().NotBeNull();

            return map.Attendee.MapShortDetails(attendee);
        }

        public IQueryable<ent::Attendee.ShortDetails> GetAll_shortDetails()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(map.Attendee.MapShortDetails)
                .AsQueryable();
        }

        public IQueryable<ent::Attendee.LongDetails> GetAll_longDetails()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(map.Attendee.MapLongDetails)
                .AsQueryable();
        }
    }
}
