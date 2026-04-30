using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PawSpital.Security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class RequireRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public RequireRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRole = context.HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrWhiteSpace(userRole) || !_roles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
        {
            context.Result = new RedirectToActionResult("Login", "Home", null);
        }
    }
}

