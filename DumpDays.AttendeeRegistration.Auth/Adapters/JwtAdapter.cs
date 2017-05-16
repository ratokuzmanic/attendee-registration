using System;
using System.Collections.Generic;
using DumpDays.AttendeeRegistration.Data.Models;
using Jose;

namespace DumpDays.AttendeeRegistration.Auth.Adapters
{
    public class JwtAdapter
    {
        public string Username { get; }
        public Roles  Role     { get; }

        public JwtAdapter(string bearerToken)
        {
            var payloadAsDictionary = JWT.Decode<Dictionary<string, string>>
                (bearerToken, Configuration.AuthSecretKey, JwsAlgorithm.HS256);

            Username = payloadAsDictionary["username"];
            Role     = (Roles)Enum.Parse(typeof(Roles), payloadAsDictionary["role"]);
        }
    }
}
