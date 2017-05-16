using System;
using System.Linq;
using System.Net.Http.Headers;
using Jose;
using Microsoft.Owin;

namespace DumpDays.AttendeeRegistration.Auth.Services
{
    public class JwtService
    {
        public static string ReadBearerTokenFromHeader(IHeaderDictionary headerDictionary)
            => headerDictionary.Get("Authorization")?.Split(' ')[1];
        
        public static string ReadBearerTokenFromHeader(HttpRequestHeaders header)
            => header.GetValues("Authorization")?.First()?.Split(' ')[1];

        public static bool IsValid(string bearerToken)
        {
            try
            {
                JWT.Decode(bearerToken, Configuration.AuthSecretKey, JwsAlgorithm.HS256);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
