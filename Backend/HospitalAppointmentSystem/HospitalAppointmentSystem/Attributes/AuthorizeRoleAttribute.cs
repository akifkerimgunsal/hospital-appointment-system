using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public AuthorizeRoleAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "DefaultRole";
            if (userRole != _role)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
