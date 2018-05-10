using System;
using System.Linq;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Auth.Mappers;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Data.Contexts;

namespace DumpDays.AttendeeRegistration.Auth.Repositories
{
    public interface IUserRepository
    {
        IMaybe<User.LongDetails>     MaybeGetOne(string username);
        IMaybe<User.ShortDetails>    MaybeGetOne(Guid id);
        IQueryable<User.LongDetails> GetAll     ();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public UserRepository(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        public IMaybe<User.LongDetails> MaybeGetOne(string username)
        {
            var maybeUser = _attendeeRegistrationContext.Users.SingleOrDefault(user => user.Username == username);

            return maybeUser == null
                ? None<User.LongDetails>.Exists
                : Some<User.LongDetails>.Exists(UserMapper.MapLongDetails(maybeUser));
        }

        public IMaybe<User.ShortDetails> MaybeGetOne(Guid id)
        {
            var maybeUser = _attendeeRegistrationContext.Users.Find(id);

            return maybeUser == null
                ? None<User.ShortDetails>.Exists
                : Some<User.ShortDetails>.Exists(UserMapper.MapShortDetails(maybeUser));
        }

        public IQueryable<User.LongDetails> GetAll()
        {
            return
                _attendeeRegistrationContext.Users
                .ToList()
                .Select(UserMapper.MapLongDetails)
                .AsQueryable();
        }
    }
}
