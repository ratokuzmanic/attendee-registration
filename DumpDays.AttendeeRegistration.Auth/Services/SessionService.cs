using System;
using DumpDays.AttendeeRegistration.Auth.Commands;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Auth.Repositories;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Auth.Services
{
    public interface ISessionService
    {
        IMaybe<Jwt.TokenBundle> MaybeLogin            (SessionService.PotentialUser potentialUser);
        IMaybe<Jwt.BearerToken> MaybeCreateBearerToken(Jwt.RefreshToken             refreshToken );
        ActionResult            Logout                (Jwt.RefreshToken             refreshToken );
    };

    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository    _userRepository;
        private readonly ISessionCommand    _sessionCommand;

        public SessionService
        (
            ISessionRepository sessionRepository, 
            IUserRepository    userRepository, 
            ISessionCommand    sessionCommand
        )
        {
            _sessionRepository = sessionRepository;
            _userRepository    = userRepository;
            _sessionCommand    = sessionCommand;
        }

        public IMaybe<Jwt.TokenBundle> MaybeLogin(PotentialUser potentialUser)
        {
            var maybeUser = _userRepository.MaybeGetOne(potentialUser.Username);

            return maybeUser.Case(
                some: user =>
                {
                    var isUsersAccountDisabled = !user.IsActive;
                    if (isUsersAccountDisabled)
                        return None<Jwt.TokenBundle>.Exists;

                    var isPasswordCorrect = PasswordService.Validate(potentialUser.Password, user.Password);
                    if (!isPasswordCorrect)
                        return None<Jwt.TokenBundle>.Exists;

                    var sesssion = _sessionCommand.Execute(new SessionCommand.Create(
                        user: user,
                        startedOn: DateTime.Now
                    ));

                    return Some<Jwt.TokenBundle>.Exists(new Jwt.TokenBundle(
                        bearerToken: new Jwt.BearerToken(username: user.Username, role: user.Role),
                        refreshToken: new Jwt.RefreshToken(id: sesssion.Id, startedOn: sesssion.StartedOn)
                    ));
                },
                none: () => None<Jwt.TokenBundle>.Exists);
        }

        public IMaybe<Jwt.BearerToken> MaybeCreateBearerToken(Jwt.RefreshToken refreshToken)
        {
            if (refreshToken.HasExpired)
                return None<Jwt.BearerToken>.Exists;

            var maybeSession = _sessionRepository.MaybeGetOne(refreshToken.Id);
            return maybeSession.Case(
                some: session => Some<Jwt.BearerToken>.Exists(new Jwt.BearerToken(
                    username: session.User.Username, role: session.User.Role
                )),
                none: () => None<Jwt.BearerToken>.Exists
            );
        }

        public ActionResult Logout(Jwt.RefreshToken refreshToken)
        {
            return _sessionCommand.Execute(new SessionCommand.Delete(
                id: refreshToken.Id
            ));
        }

        public class PotentialUser
        {
            public string Username { get; }
            public string Password { get; }

            public PotentialUser
            (
                string username,
                string password
            )
            {
                Username = username;
                Password = password;
            }
        }
    }
}