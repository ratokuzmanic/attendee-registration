using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Services;
using DumpDays.AttendeeRegistration.Data.Contexts;
using DumpDays.AttendeeRegistration.Data.Models;
using Jose;

namespace DumpDays.AttendeeRegistration.Web.Controllers.Api
{
    [RoutePrefix("api/auth")]
    public class AuthApiController : ApiController
    {
        private readonly IAttendeeRegistrationContext _attendeeRegistrationContext;

        public AuthApiController(IAttendeeRegistrationContext attendeeRegistrationContext)
        {
            _attendeeRegistrationContext = attendeeRegistrationContext;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDto potentialUser)
        {
            var user = _attendeeRegistrationContext.Users.SingleOrDefault(_ => _.Username == potentialUser.Username);
            if (user == null) return Unauthorized();

            var isUsersAccountDisabled = !user.IsActive;
            if(isUsersAccountDisabled) return Unauthorized();

            var areCredentialsValid = PasswordHashingService.Validate(potentialUser.Password, user.Password);
            if (!areCredentialsValid) return Unauthorized();

            var bearerToken = _generateJwt(user.Username, user.Role);
            
            return Ok(bearerToken);
        }

        private static string _generateJwt(string username, Roles role)
        {
            var payload = new Dictionary<string, string>()
            {
                { "username", username               },
                { "role",     ((int)role).ToString() }
            };

            return JWT.Encode(payload, Auth.Configuration.AuthSecretKey, JwsAlgorithm.HS256);
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
