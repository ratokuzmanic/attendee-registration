using System.Web.Http.Controllers;
using DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Auth.Attributes
{
    public class AllowModeratorsAndAdministrators : AllowRoleOrRolesHigherThan
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Role = Roles.Moderator;
            base.OnActionExecuting(actionContext);
        }
    }
}
