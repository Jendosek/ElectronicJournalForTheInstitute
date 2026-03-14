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
        var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

        if (context.User.Identity?.IsAuthenticated == true && !context.User.IsInRole("admin"))
        {
            var isAdminOnlyCrudPage = path.Contains("/create") || path.Contains("/edit") || path.Contains("/delete");
            var isStudentMutation = path.StartsWith("/students") && HttpMethods.IsPost(context.Request.Method);

            if (isAdminOnlyCrudPage || isStudentMutation)
            {
                context.Response.Redirect("/Index");
                return;
            }
        }

        await _next(context);
    }
}