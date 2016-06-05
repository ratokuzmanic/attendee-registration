using System.Web.Mvc;
using System.Web.Routing;

namespace DumpDay.AttendeeRegistration.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:     "Default",
                url:      "{*url}",
                defaults: new { controller = "Home", action = "Shell" }
            );
        }
    }
}
