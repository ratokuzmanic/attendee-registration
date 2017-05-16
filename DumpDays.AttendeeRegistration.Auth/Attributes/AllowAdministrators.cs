using System.Web.Http.Controllers;
using DumpDays.AttendeeRegistration.Data.Models;

namespace DumpDays.AttendeeRegistration.Auth.Attributes
{
    public class AllowAdministrators : AllowRoleOrRolesHigherThan
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Role = Roles.Administrator;
            base.OnActionExecuting(actionContext);
        }
    }
}
