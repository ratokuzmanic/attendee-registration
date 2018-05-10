using System.Web.Http;
using DumpDays.AttendeeRegistration.Auth.Middleware;
using Owin;

namespace DumpDays.AttendeeRegistration.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.RegisterRoutes(config);

            app.Use<BlockRequestsWithInvalidBearerToken>();
            app.UseAutofacMiddleware(IoCConfig.RegisterDependencies(config));
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
