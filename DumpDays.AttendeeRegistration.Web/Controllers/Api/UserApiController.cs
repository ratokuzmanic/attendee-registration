using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Attributes;
using DumpDays.AttendeeRegistration.Data.Models;
using DumpDays.AttendeeRegistration.Domain.Commands;
using DumpDays.AttendeeRegistration.Domain.Repositories;
using User = DumpDays.AttendeeRegistration.Domain.Entities.User;

namespace DumpDays.AttendeeRegistration.Web.Controllers.Api
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
        public User.ShortDetails GetOne(Guid id)
        {
            return _userRepository.GetOne(id);
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
        public void Create(CreateDto newUser)
        {
            var createCommand = new UserCommand.Create(
                username: newUser.Username,
                role:     (Roles)newUser.Role
            );
            _userCommand.Execute(createCommand);
        }

        [HttpPost]
        [Route("setupPassword/{id:Guid}")]
        public void SetupPassword([FromUri]Guid id, SetupPasswordDto newPassword)
        {
            var setupPasswordCommand = new UserCommand.SetupPassword(
                id:       id,
                password: newPassword.Password
            );
            _userCommand.Execute(setupPasswordCommand);
        }

        [HttpGet]
        [Route("triggerActivity/{id:Guid}")]
        [AllowAdministrators]
        public void TriggerActivity(Guid id)
        {
            var triggerActivityCommand = new UserCommand.TriggerActivity(id);
            _userCommand.Execute(triggerActivityCommand);
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        [AllowAdministrators]
        public void Delete(Guid id)
        {
            var deleteCommand = new UserCommand.Delete(id);
            _userCommand.Execute(deleteCommand);
        }

        public class CreateDto
        {
            public string Username { get; set; }
            public int    Role     { get; set; }
        }

        public class SetupPasswordDto
        {
            public string Password { get; set; }
        }
    }
}
