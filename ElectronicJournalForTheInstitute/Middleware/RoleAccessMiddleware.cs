using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ElectronicJournalForTheInstitute.Middleware;

public class RoleAccessMiddleware
{
    private readonly RequestDelegate _next;

    public RoleAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        if (context.User.Identity?.IsAuthenticated == true)
        {
            bool isUserManagementPage = path.StartsWith("/manageusers");

            if (isUserManagementPage && !context.User.IsInRole("admin"))
            {
                context.Response.Redirect("/Index"); 
                return;
            }
        }

        await _next(context); 
    }
}