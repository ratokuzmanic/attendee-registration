using DumpDays.AttendeeRegistration.Auth.Extensions;
using DumpDays.AttendeeRegistration.Auth.Middleware;
using DumpDays.AttendeeRegistration.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DumpDays.AttendeeRegistration.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.BlockRequestWithInvalidBearerToken(new BlockRequestWithInvalidBearerToken.Options()
            {
                ErrorCode    = "401",
                ErrorMessage = "Invalid token"
            });
        }
    }
}