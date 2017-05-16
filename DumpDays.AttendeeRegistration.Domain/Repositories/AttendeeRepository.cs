using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Domain.Entities;
using FluentAssertions;

namespace DumpDays.AttendeeRegistration.Domain.Repositories
{
    using map = Mappers.AttendeeMapper;

    public interface IAttendeeRepository
    {
        Attendee.ShortDetails                  GetOne_shortDetails(Guid id);
        IQueryable<Attendee.ShortDetails>      GetAll_shortDetails();
        IQueryable<Attendee.LongDetails>       GetAll_longDetails();
        IQueryable<Attendee.StatisticsDetails> GetAll_statisticsDetails();
    }

    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public AttendeeRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public Attendee.ShortDetails GetOne_shortDetails(Guid id)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(id);
            attendee.Should().NotBeNull();

            return map.MapShortDetails(attendee);
        }

        public IQueryable<Attendee.ShortDetails> GetAll_shortDetails()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(map.MapShortDetails)
                .AsQueryable();
        }

        public IQueryable<Attendee.LongDetails> GetAll_longDetails()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(map.MapLongDetails)
                .AsQueryable();
        }

        public IQueryable<Attendee.StatisticsDetails> GetAll_statisticsDetails()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(map.MapStatisticsDetails)
                .AsQueryable();
        }
    }
}
