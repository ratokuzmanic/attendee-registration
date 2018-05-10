using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Domain.Entities;
using DumpDays.AttendeeRegistration.Domain.Mappers;

namespace DumpDays.AttendeeRegistration.Domain.Repositories
{
    public interface IAttendeeRepository
    {
        IMaybe<Attendee.ShortDetails>          GetOne(Guid id);
        IQueryable<Attendee.LongDetails>       GetAll();
        IQueryable<Attendee.StatisticsDetails> GetAll_statistics();
    }

    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public AttendeeRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public IMaybe<Attendee.ShortDetails> GetOne(Guid id)
        {
            var attendee = _attendeeRegistrationContext.Attendees.Find(id);
            if (attendee == null) return None<Attendee.ShortDetails>.Exists;

            return Some<Attendee.ShortDetails>.Exists(AttendeeMapper.MapShortDetails(attendee));
        }

        public IQueryable<Attendee.LongDetails> GetAll()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(AttendeeMapper.MapLongDetails)
                .AsQueryable();
        }

        public IQueryable<Attendee.StatisticsDetails> GetAll_statistics()
        {
            return
                _attendeeRegistrationContext.Attendees
                .ToList()
                .Select(AttendeeMapper.MapStatisticsDetails)
                .AsQueryable();
        }
    }
}
