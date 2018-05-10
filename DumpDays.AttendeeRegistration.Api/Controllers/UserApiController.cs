using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Attributes;
using DumpDays.AttendeeRegistration.Auth.Commands;
using DumpDays.AttendeeRegistration.Auth.Repositories;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Auth.Entities;

namespace DumpDays.AttendeeRegistration.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCommand    _userCommand;

        public UserApiController(IUserRepository userRepository, IUserCommand userCommand)
        {
            _userRepository = userRepository;
            _userCommand = userCommand;
        }

        [HttpGet]
        [Route("getOne/{id:Guid}")]
        public IHttpActionResult GetOne(Guid id)
        {
            var maybeUser = _userRepository.MaybeGetOne(id);

            return maybeUser.Case(
                some: Ok,
                none: () => new ErrorResponse(
                    code: HttpStatusCode.NotFound,
                    message: "User doesn't exist"
                ).SetErrorResponseIn(this)
            );
        }

        [HttpGet]
        [Route("getAll")]
        [AllowAdministrators]
        public IReadOnlyCollection<User.LongDetails> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        [HttpPost]
        [Route("create")]
        [AllowAdministrators]
        public IHttpActionResult Create(CreateDto newUser)
        {
            if(newUser.IsNotValid)
                return new ErrorResponse(
                    code: HttpStatusCode.BadRequest,
                    message: "Required field(s) are missing"
                ).SetErrorResponseIn(this);

            var createCommand = new UserCommand.Create(
                username: newUser.Username,
                role:     (Roles)newUser.Role
            );
            return _userCommand.Execute(createCommand) == ActionResult.Success
                ? Ok()
                : new ErrorResponse(
                    code: HttpStatusCode.BadRequest,
                    message: "Username is already taken"
                ).SetErrorResponseIn(this);
        }

        [HttpPost]
        [Route("setupPassword/{id:Guid}")]
        public IHttpActionResult SetupPassword([FromUri]Guid id, SetupPasswordDto newPassword)
        {
            var setupPasswordCommand = new UserCommand.SetupPassword(
                id:       id,
                password: newPassword.Password
            );
            return _userCommand.Execute(setupPasswordCommand) == ActionResult.Success
                ? Ok()
                : new ErrorResponse(
                    code: HttpStatusCode.BadRequest,
                    message: "User doesn't exist, has already setup password or a new provided password is invalid"
                ).SetErrorResponseIn(this);
        }

        [HttpGet]
        [Route("triggerActivity/{id:Guid}")]
        [AllowAdministrators]
        public IHttpActionResult TriggerActivity(Guid id)
        {
            var triggerActivityCommand = new UserCommand.TriggerActivity(id);
            return _userCommand.Execute(triggerActivityCommand) == ActionResult.Success
                ? Ok()
                : new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "User doesn't exist or is a root administrator"
                ).SetErrorResponseIn(this);
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        [AllowAdministrators]
        public IHttpActionResult Delete(Guid id)
        {
            var deleteCommand = new UserCommand.Delete(id);
            return _userCommand.Execute(deleteCommand) == ActionResult.Success
                ? Ok()
                : new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "User doesn't exist or is a root administrator"
                ).SetErrorResponseIn(this);
        }

        public class CreateDto
        {
            public string Username { get; set; }
            public int    Role     { get; set; }

            public bool IsNotValid => string.IsNullOrWhiteSpace(Username);
        }

        public class SetupPasswordDto
        {
            public string Password { get; set; }
        }
    }
}
