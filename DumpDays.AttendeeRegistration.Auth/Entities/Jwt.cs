using System;
using System.Collections.Generic;
using DumpDays.AttendeeRegistration.Common;
using Jose;
using Newtonsoft.Json;

namespace DumpDays.AttendeeRegistration.Auth.Entities
{
    public abstract class Jwt
    {
        private static readonly byte[]       SigningKey               = Configuration.SigningKey;
        private static readonly JwsAlgorithm SigningAlgorithm         = JwsAlgorithm.HS512;
        private static readonly string       TokenDateTimeFormat      = Configuration.TokenDateTimeFormat;
        private static readonly TimeSpan     BearerTokenValiditySpan  = Configuration.BearerTokenValiditySpan;
        private static readonly TimeSpan     RefreshTokenValiditySpan = Configuration.RefreshTokenValiditySpan;

        public class BearerToken
        {
            public string   Username  { get; }
            public Roles    Role      { get; }
            public DateTime ExpiresOn { get; }

            public BearerToken
            (
                string username,
                Roles  role
            )
            {
                Username  = username;
                Role      = role;
                ExpiresOn = DateTime.Now.Add(BearerTokenValiditySpan);
            }

            private BearerToken
            (
                string bearerToken
            )
            {
                var payload = DecodeToken(bearerToken);

                Username  = payload["username"];
                Role      = (Roles)Enum.Parse(typeof(Roles), payload["role"]);
                ExpiresOn = DateTime.ParseExact(payload["expires_on"], TokenDateTimeFormat, null);
            }

            public static IMaybe<BearerToken> CreateIfValid(string token)
            {
                return IsValid(token)
                    ? Some<BearerToken>.Exists(new BearerToken(token))
                    : None<BearerToken>.Exists;
            }

            public string GetEncodedToken()
            {
                var payload = new Dictionary<string, string>()
                {
                    { "username",   Username                                },
                    { "role",       ((int)Role).ToString()                  },
                    { "expires_on", ExpiresOn.ToString(TokenDateTimeFormat) }
                };
                return EncodeToken(payload);
            }

            public bool HasExpired
                => ExpiresOn < DateTime.Now;
        }

        public class RefreshToken
        {
            public Guid     Id        { get; }
            public DateTime ExpiresOn { get; }

            public RefreshToken
            (
                Guid     id,
                DateTime startedOn
            )
            {
                Id        = id;
                ExpiresOn = startedOn.Add(RefreshTokenValiditySpan);
            }

            private RefreshToken
            (
                string refreshToken
            )
            {
                var payload = DecodeToken(refreshToken);

                Id        = Guid.Parse(payload["session_id"]);
                ExpiresOn = DateTime.ParseExact(payload["expires_on"], TokenDateTimeFormat, null);
            }

            public static IMaybe<RefreshToken> CreateIfValid(string token)
            {
                return IsValid(token)
                    ? Some<RefreshToken>.Exists(new RefreshToken(token))
                    : None<RefreshToken>.Exists;
            }

            public string GetEncodedToken()
            {
                var payload = new Dictionary<string, string>()
                {
                    { "session_id", Id.ToString()                           },
                    { "expires_on", ExpiresOn.ToString(TokenDateTimeFormat) }
                };
                return EncodeToken(payload);
            }

            public bool HasExpired
                => ExpiresOn < DateTime.Now;
        }

        public class TokenBundle
        {
            public BearerToken  BearerToken  { get; }
            public RefreshToken RefreshToken { get; }

            public TokenBundle
            (
                BearerToken  bearerToken,
                RefreshToken refreshToken
            )
            {
                BearerToken  = bearerToken;
                RefreshToken = refreshToken;
            }

            public string GetJson()
                => JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    { "bearer_token",  BearerToken.GetEncodedToken()  },
                    { "refresh_token", RefreshToken.GetEncodedToken() }
                });
        }

        private static string EncodeToken(Dictionary<string, string> payload)
            => JWT.Encode(payload, SigningKey, SigningAlgorithm);

        private static Dictionary<string, string> DecodeToken(string token)
            => JWT.Decode<Dictionary<string, string>>(token, SigningKey, SigningAlgorithm);

        public static bool IsValid(string token)
        {
            try
            {
                JWT.Decode(token, SigningKey, SigningAlgorithm);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
