using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Attributes;
using DumpDays.AttendeeRegistration.Data.Models;
using DumpDays.AttendeeRegistration.Domain.Commands;
using DumpDays.AttendeeRegistration.Domain.Repositories;
using DumpDays.AttendeeRegistration.Domain.Services;
using Attendee = DumpDays.AttendeeRegistration.Domain.Entities.Attendee;

namespace DumpDays.AttendeeRegistration.Web.Controllers.Api
{
    [RoutePrefix("api/attendee")]
    [AllowModeratorsAndAdministrators]
    public class AttenderApiController : ApiController
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IAttendeeCommand    _attendeeCommand;

        public AttenderApiController(IAttendeeRepository attendeeRepository, IAttendeeCommand attendeeCommand)
        {
            _attendeeRepository = attendeeRepository;
            _attendeeCommand = attendeeCommand;
        }

        [HttpGet]
        [Route("getOneShortDetails/{id:Guid}")]
        public Attendee.ShortDetails GetOne(Guid id)
        {
            return _attendeeRepository.GetOne_shortDetails(id);
        }

        [HttpGet]
        [Route("getOneForPrinting/{id:Guid}")]
        public Attendee.ShortDetails Print(Guid id)
        {
            var printCommand = new AttendeeCommand.Print(id);
            return _attendeeCommand.Execute(printCommand);
        }

        [HttpGet]
        [Route("getAllLongDetails")]
        public IReadOnlyCollection<Attendee.LongDetails> GetAll()
        {
            return _attendeeRepository.GetAll_longDetails().ToList();
        }

        [HttpGet]
        [Route("getAllStatisticsDetails")]
        [AllowAdministrators]
        public IReadOnlyCollection<Attendee.StatisticsDetails> GetAll_statistics()
        {
            return _attendeeRepository.GetAll_statisticsDetails().ToList();
        }

        [HttpGet]
        [Route("getAllCsv")]
        [AllowAdministrators]
        public string GetAll_csv()
        {
            var allAttenders = _attendeeRepository.GetAll_longDetails().ToList();
            return AttendeeCsvSerializerService.Serialize(allAttenders);
        }

        [HttpPost]
        [Route("create")]
        public Attendee.ShortDetails Create(CreateDto newAttendee)
        {
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
            return _attendeeCommand.Execute(createCommand);
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        [AllowAdministrators]
        public void Delete(Guid id)
        {
            var deleteCommand = new AttendeeCommand.Delete(id);
            _attendeeCommand.Execute(deleteCommand);
        }

        public class CreateDto
        {
            public string FirstName   { get; set; }
            public string LastName    { get; set; }
            public string Birthdate   { get; set; }
            public string Email       { get; set; }
            public int    WorkStatus  { get; set; }
        }
    }
}
