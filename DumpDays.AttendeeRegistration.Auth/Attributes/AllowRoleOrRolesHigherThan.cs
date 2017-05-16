using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DumpDays.AttendeeRegistration.Auth.Adapters;
using DumpDays.AttendeeRegistration.Auth.Services;
using DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Auth.Attributes
{
    public class AllowRoleOrRolesHigherThan : ActionFilterAttribute
    {
        public Roles Role { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var bearerTokenString = JwtService.ReadBearerTokenFromHeader(actionContext.Request.Headers);

            var token = new JwtAdapter(bearerTokenString);
            if (token.Role < Role)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
