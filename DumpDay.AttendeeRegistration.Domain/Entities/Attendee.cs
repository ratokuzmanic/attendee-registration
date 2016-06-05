using System;
using FluentAssertions;
using DumpDay.AttendeeRegistration.Data.Models;
using DumpDay.AttendeeRegistration.Domain.Extensions;

namespace DumpDay.AttendeeRegistration.Domain.Entities
{
    public abstract class Attendee
    {
        public class ShortDetails
        {
            public int    Id        { get; }
            public string FirstName { get; }
            public string LastName  { get; }
            public string FullName  { get; }
            public bool   IsMinor   { get; }

            public ShortDetails
            (
                int      id,
                string   firstName,
                string   lastName,
                DateTime birthdate
            )
            {
                id       .Should().BeGreaterOrEqualTo(0);
                firstName.Should().NotBeNullOrEmpty();
                lastName .Should().NotBeNullOrEmpty();

                Id        = id;
                FirstName = firstName;
                LastName  = lastName;
                FullName  = $"{FirstName} {LastName}";
                IsMinor   = birthdate.IsMinor();
            }
        }

        public class LongDetails
        {
            public int          Id          { get; }
            public string       FirstName   { get; }
            public string       LastName    { get; }
            public DateTime     Birthdate   { get; }
            public string       Institution { get; }
            public WorkStatuses WorkStatus  { get; }
            public DateTime     CreatedOn   { get; }

            public LongDetails
            (
                int          id,
                string       firstName,
                string       lastName,
                DateTime     birthdate,
                string       institution,
                WorkStatuses workStatus,
                DateTime     createdOn
            )
            {
                id        .Should().BeGreaterOrEqualTo(0);
                firstName .Should().NotBeNullOrEmpty();
                lastName  .Should().NotBeNullOrEmpty();
                workStatus.Should().NotBeNull();

                Id          = id;
                FirstName   = firstName;
                LastName    = lastName;
                Birthdate   = birthdate;
                Institution = institution;
                WorkStatus  = workStatus;
                CreatedOn   = createdOn;
            }
        }
        
    }
}
