using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Attributes;
using DumpDays.AttendeeRegistration.Common;
using DumpDays.AttendeeRegistration.Domain.Commands;
using DumpDays.AttendeeRegistration.Domain.Repositories;
using DumpDays.AttendeeRegistration.Domain.Entities;

namespace DumpDays.AttendeeRegistration.Api.Controllers
{
    [RoutePrefix("api/attendee")]
    [AllowModeratorsAndAdministrators]
    public class AttendeeApiController : ApiController
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IAttendeeCommand    _attendeeCommand;

        public AttendeeApiController(IAttendeeRepository attendeeRepository, IAttendeeCommand attendeeCommand)
        {
            _attendeeRepository = attendeeRepository;
            _attendeeCommand = attendeeCommand;
        }

        [HttpGet]
        [Route("getOne/{id:Guid}")]
        public IHttpActionResult GetOne(Guid id)
        {
            return _attendeeRepository.GetOne(id).Case(
                some: Ok,
                none: () => new ErrorResponse(
                    code: HttpStatusCode.NotFound,
                    message: "User doesn't exist"
                ).SetErrorResponseIn(this)
            );
        }

        [HttpGet]
        [Route("getOne/print/{id:Guid}")]
        public IHttpActionResult Print(Guid id)
        {
            var printCommand = new AttendeeCommand.Print(id);
            return _attendeeCommand.Execute(printCommand).Case(
                some: Ok,
                none: () => new ErrorResponse(
                    code: HttpStatusCode.NotFound,
                    message: "User doesn't exist"
                ).SetErrorResponseIn(this)
            );
        }

        [HttpGet]
        [Route("getAll")]
        public IReadOnlyCollection<Attendee.LongDetails> GetAll()
        {
            return _attendeeRepository.GetAll().ToList();
        }

        [HttpGet]
        [Route("getAll/statistics")]
        [AllowAdministrators]
        public IReadOnlyCollection<Attendee.StatisticsDetails> GetAll_statistics()
        {
            return _attendeeRepository.GetAll_statistics().ToList();
        }

        [HttpGet]
        [Route("getAll/csv")]
        [AllowAdministrators]
        public IHttpActionResult GetAll_csv()
        {
            var attendees = _attendeeRepository.GetAll().ToList();
            return Ok(Attendee.Serialize(attendees));
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateDto newAttendee)
        {
            if(newAttendee.IsNotValid)
                return new ErrorResponse(
                    code: HttpStatusCode.BadRequest,
                    message: "Required field(s) are missing"
                ).SetErrorResponseIn(this);

            var createCommand = new AttendeeCommand.Create(
                firstName:  newAttendee.FirstName,
                lastName:   newAttendee.LastName,
                email:      newAttendee.Email,
                birthdate:  DateTime.ParseExact(newAttendee.Birthdate, new [] {
                                "d.M.yyyy.",
                                "d.M.yyyy",
                                "d/M/yyyy",
                                "d-M-yyyy"
                            }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                workStatus: (WorkStatuses)newAttendee.WorkStatus,
                createdOn:  DateTime.Now
            );
            return Ok(_attendeeCommand.Execute(createCommand));
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        [AllowAdministrators]
        public IHttpActionResult Delete(Guid id)
        {
            var deleteCommand = new AttendeeCommand.Delete(id);
            return _attendeeCommand.Execute(deleteCommand) == ActionResult.Success
                ? Ok()
                : new ErrorResponse(
                    code: HttpStatusCode.NotFound,
                    message: "User doesn't exist"
                ).SetErrorResponseIn(this);
        }

        public class CreateDto
        {
            public string FirstName   { get; set; }
            public string LastName    { get; set; }
            public string Birthdate   { get; set; }
            public string Email       { get; set; }
            public int    WorkStatus  { get; set; }

            public bool IsNotValid =>
                new[] {FirstName, LastName, Birthdate, Email}.Any(string.IsNullOrWhiteSpace);
        }
    }
}
