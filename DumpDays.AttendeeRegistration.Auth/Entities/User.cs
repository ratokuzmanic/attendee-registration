using System;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Auth.Entities
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
                Username = username;
                Role     = role;
            }
        }

        public class LongDetails
        {
            public Guid   Id       { get; }
            public string Username { get; }
            public string Password { get; }
            public Roles  Role     { get; }
            public bool   IsActive { get; }
            public bool   IsSetup  { get; }

            public LongDetails
            (
                Guid   id,
                string username,
                string password,
                Roles  role,
                bool   isActive,
                bool   isSetup
            )
            {
                Id       = id;
                Username = username;
                Password = password;
                Role     = role;
                IsActive = isActive;
                IsSetup  = isSetup;
            }
        }
    }
}
