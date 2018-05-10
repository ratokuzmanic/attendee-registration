using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;
using DumpDays.AttendeeRegistration.Auth.Attributes;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Auth.Services;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthApiController : ApiController
    {
        private readonly ISessionService _sessionService;

        public AuthApiController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDto potentialUser)
        {
            var maybeTokens = _sessionService.MaybeLogin(new SessionService.PotentialUser(
                username: potentialUser.Username,
                password: potentialUser.Password
            ));

            return maybeTokens.Case(
                some: TokenBundleResponse,
                none: () => new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "Username and password do not match any valid user"
                ).SetErrorResponseIn(this)
            );
        }

        [HttpPost]
        [Route("getNewBearerToken")]
        public IHttpActionResult GetNewBearerToken(TokenDto possibleToken)
        {
            var maybeRefreshToken = Jwt.RefreshToken.CreateIfValid(possibleToken.RefreshToken);
            return maybeRefreshToken.Case(
                some: refreshToken =>
                {
                    var maybeNewBearerToken = _sessionService.MaybeCreateBearerToken(refreshToken);

                    return maybeNewBearerToken.Case(
                        some: bearerToken => Ok(bearerToken.GetEncodedToken()),
                        none: () => new ErrorResponse(
                            code: HttpStatusCode.Unauthorized,
                            message: "Refresh token has expired"
                        ).SetErrorResponseIn(this)
                    );
                },
                none: () => new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "Refresh token is corrupted"
                ).SetErrorResponseIn(this)
            );
        }

        [HttpPost]
        [Route("logout")]
        [AllowModeratorsAndAdministrators]
        public IHttpActionResult Logout(TokenDto possibleToken)
        {
            var maybeRefreshToken = Jwt.RefreshToken.CreateIfValid(possibleToken.RefreshToken);
            return maybeRefreshToken.Case(
                some: refreshToken => 
                    _sessionService.Logout(refreshToken) == ActionResult.Success
                        ? Ok()
                        : new ErrorResponse(
                            code: HttpStatusCode.NotFound,
                            message: "There is no active session matching this refresh token"
                        ).SetErrorResponseIn(this)
                ,
                none: () => new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "Refresh token is corrupted"
                ).SetErrorResponseIn(this)
            );
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class TokenDto
        {
            public string RefreshToken { get; set; }
        }

        private static IHttpActionResult TokenBundleResponse(Jwt.TokenBundle tokenBundle)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(tokenBundle.GetJson())
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            return new ResponseMessageResult(response);
        }
    }
}
