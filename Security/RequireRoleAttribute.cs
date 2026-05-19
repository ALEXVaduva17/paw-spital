using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PawSpital.Security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class RequireRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;
    public RequireRoleAttribute(params string[] roles) { _roles = roles; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new RedirectToActionResult("Login", "Home", null);
            return;
        }

        if (!_roles.Any(r => user.IsInRole(r)))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
        }
    }
}
