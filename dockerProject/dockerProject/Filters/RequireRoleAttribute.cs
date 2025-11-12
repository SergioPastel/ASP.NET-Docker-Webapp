using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

// Class to require specific user roles for accessing controllers or actions

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequireRoleAttribute : Attribute, IAsyncActionFilter
{
    private readonly string[] _roles;

    // No parameters => require any authenticated user (session role present)
    // Pass roles => require one of those roles
    public RequireRoleAttribute(params string[] roles) => _roles = roles ?? new string[0];

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var http = context.HttpContext;
        var role = http.Session.GetString("UserRole");

        // No role in session -> not logged in
        if (string.IsNullOrEmpty(role))
        {
            // redirect to your login page (or return 403/Forbid if preferred)
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        // If _roles was specified, ensure session role is one of them
        if (_roles.Length > 0 && !_roles.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase)))
        {
            // Option: redirect to AccessDenied or return Forbid()
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            return;
        }

        await next();
    }
}
