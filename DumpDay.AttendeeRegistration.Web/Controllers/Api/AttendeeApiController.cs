using System;
using System.Linq;
using System.Web.Http;
using System.Globalization;
using System.Collections.Generic;
using DumpDay.AttendeeRegistration.Data.Models;
using DumpDay.AttendeeRegistration.Domain.Commands;
using DumpDay.AttendeeRegistration.Domain.Repositories;
using DumpDay.AttendeeRegistration.Domain.Services;
using ent = DumpDay.AttendeeRegistration.Domain.Entities;

namespace DumpDay.AttendeeRegistration.Web.Controllers.Api
{
    [RoutePrefix("api/attendee")]
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
        [Route("get-one-short-details/{id:int}")]
        public ent::Attendee.ShortDetails GetOne(int id)
        {
            return _attendeeRepository.GetOne_shortDetails(id);
        }

        [HttpGet]
        [Route("get-all-long-details")]
        public IReadOnlyCollection<ent::Attendee.LongDetails> GetAll()
        {
            return _attendeeRepository.GetAll_longDetails().ToList();
        }

        [HttpGet]
        [Route("get-all-csv")]
        public string GetAll_csv()
        {
            var allAttenders = _attendeeRepository.GetAll_longDetails().ToList();
            return AttendeeCsvSerializerService.Serialize(allAttenders);
        }

        [HttpPost]
        [Route("create")]
        public ent::Attendee.ShortDetails Create(CreateDto newAttendee)
        {
            var createCommand = new AttendeeCommand.Create(
                firstName:   newAttendee.FirstName,
                lastName:    newAttendee.LastName,
                birthdate:   DateTime.ParseExact(newAttendee.Birthdate, new [] {
                                 "d.M.yyyy.",
                                 "d.M.yyyy",
                                 "d/M/yyyy",
                                 "d-M-yyyy"
                             }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                institution: newAttendee.Institution,
                workStatus:  (WorkStatuses)newAttendee.WorkStatus,
                createdOn:   DateTime.Now
            );
            return _attendeeCommand.Execute(createCommand);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public void Delete(int id)
        {
            var deleteCommand = new AttendeeCommand.Delete(id);
            _attendeeCommand.Execute(deleteCommand);
        }

        public class CreateDto
        {
            public string FirstName   { get; set; }
            public string LastName    { get; set; }
            public string Birthdate   { get; set; }
            public string Institution { get; set; }
            public int    WorkStatus  { get; set; }
        }
    }
}
