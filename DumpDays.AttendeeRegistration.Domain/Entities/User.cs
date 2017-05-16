using System;
using DumpDays.AttendeeRegistration.Data.Models;
using FluentAssertions;

namespace DumpDays.AttendeeRegistration.Domain.Entities
{
    public abstract class User
    {
        public class ShortDetails
        {
            public string Username { get; }
            public Roles  Role     { get; }

            public ShortDetails
            (
                string username,
                Roles  role
            )
            {
                username.Should().NotBeNullOrEmpty();
                role    .Should().NotBeNull();

                Username = username;
                Role     = role;
            }
        }

        public class LongDetails
        {
            public Guid   Id       { get; }
            public string Username { get; }
            public Roles  Role     { get; }
            public bool   IsActive { get; }
            public bool   IsSetup  { get; }

            public LongDetails
            (
                Guid   id,
                string username,
                Roles  role,
                bool   isActive,
                bool   isSetup
            )
            {
                id      .Should().NotBeEmpty();
                username.Should().NotBeNullOrEmpty();
                role    .Should().NotBeNull();

                Id       = id;
                Username = username;
                Role     = role;
                IsActive = isActive;
                IsSetup  = isSetup;
            }
        }
    }
}
