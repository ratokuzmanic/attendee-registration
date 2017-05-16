using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Domain.Entities;
using FluentAssertions;

namespace DumpDays.AttendeeRegistration.Domain.Repositories
{
    using map = Mappers.UserMapper;

    public interface IUserRepository
    {
        User.ShortDetails            GetOne(Guid id);
        IQueryable<User.LongDetails> GetAll();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public UserRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public User.ShortDetails GetOne(Guid id)
        {
            var user = _attendeeRegistrationContext.Users.Find(id);
            user.Should().NotBeNull();

            return map.MapShortDetails(user);
        }

        public IQueryable<User.LongDetails> GetAll()
        {
            return
                _attendeeRegistrationContext.Users
                .ToList()
                .Select(map.MapLongDetails)
                .AsQueryable();
        }
    }
}
