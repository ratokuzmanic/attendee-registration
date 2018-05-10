using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Auth.Attributes
{
    public class AllowRolesHigherThan : ActionFilterAttribute
    {
        public Roles MinimalRole { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var encodedBearerToken = actionContext.Request.Headers.Authorization?.Scheme;

            if(encodedBearerToken == null)
            {
                new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "You need to login to access this resource"
                ).SetErrorResponseIn(actionContext);
                return;
            }

            var maybeBearerToken = Jwt.BearerToken.CreateIfValid(encodedBearerToken);
            maybeBearerToken.Case(
                some: bearerToken =>
                {
                    if (bearerToken.Role < MinimalRole)
                    {
                        new ErrorResponse(
                            code: HttpStatusCode.Forbidden,
                            message: "You do not have permission to access this resource"
                        ).SetErrorResponseIn(actionContext);
                    }
                },
                none: () => new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "Bearer token is corrupted"
                ).SetErrorResponseIn(actionContext)
            );
            if (maybeBearerToken.HasValue) return;

            base.OnActionExecuting(actionContext);
        }
    }

    public class AllowAdministrators : AllowRolesHigherThan
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            MinimalRole = Roles.Administrator;
            base.OnActionExecuting(actionContext);
        }
    }

    public class AllowModeratorsAndAdministrators : AllowRolesHigherThan
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            MinimalRole = Roles.Moderator;
            base.OnActionExecuting(actionContext);
        }
    }
}
