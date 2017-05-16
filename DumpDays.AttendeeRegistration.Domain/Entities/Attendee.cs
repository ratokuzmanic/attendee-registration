using System;
using DumpDays.AttendeeRegistration.Data.Models;
using DumpDays.AttendeeRegistration.Domain.Extensions;
using FluentAssertions;

namespace DumpDays.AttendeeRegistration.Domain.Entities
{
    public abstract class Attendee
    {
        public class ShortDetails
        {
            public Guid   Id        { get; }
            public string FirstName { get; }
            public string LastName  { get; }
            public string FullName  { get; }
            public bool   IsMinor   { get; }

            public ShortDetails
            (
                Guid     id,
                string   firstName,
                string   lastName,
                DateTime birthdate
            )
            {
                id       .Should().NotBeEmpty();
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
            public Guid         Id                     { get; }
            public string       FirstName              { get; }
            public string       LastName               { get; }
            public string       FullName               { get; }
            public string       Email                  { get; }
            public DateTime     Birthdate              { get; }
            public WorkStatuses WorkStatus             { get; }
            public bool         IsAccreditationPrinted { get; }
            public DateTime     CreatedOn              { get; }

            public LongDetails
            (
                Guid         id,
                string       firstName,
                string       lastName,
                string       email,
                DateTime     birthdate,
                WorkStatuses workStatus,
                bool         isAccreditationPrinted,
                DateTime     createdOn
            )
            {
                id        .Should().NotBeEmpty();
                firstName .Should().NotBeNullOrEmpty();
                lastName  .Should().NotBeNullOrEmpty();
                email     .Should().NotBeNullOrEmpty();
                workStatus.Should().NotBeNull();

                Id                     = id;
                FirstName              = firstName;
                LastName               = lastName;
                FullName               = $"{FirstName} {LastName}";
                Email                  = email;
                Birthdate              = birthdate;
                WorkStatus             = workStatus;
                IsAccreditationPrinted = isAccreditationPrinted;
                CreatedOn              = createdOn;
            }
        }

        public class StatisticsDetails
        {
            public bool         DidRegisterOnline      { get; }
            public bool         IsAccreditationPrinted { get; }
            public int          HourOfRegistration     { get; }
            public AgeGroups    AgeGroup               { get; }
            public WorkStatuses WorkStatus             { get; }

            public StatisticsDetails
            (
                bool         didRegisterOnline,
                bool         isAccreditationPrinted,
                int          hourOfRegistration,
                AgeGroups    ageGroup,
                WorkStatuses workStatus
            )
            {
                ageGroup  .Should().NotBeNull();
                workStatus.Should().NotBeNull();

                DidRegisterOnline      = didRegisterOnline;
                IsAccreditationPrinted = isAccreditationPrinted;
                HourOfRegistration     = hourOfRegistration;
                AgeGroup               = ageGroup;
                WorkStatus             = workStatus;
            }
        }
    }
}
