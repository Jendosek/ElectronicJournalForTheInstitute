using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ElectronicJournalForTheInstitute.Middleware;

public class LoginRequiredMiddleware
{
    private readonly RequestDelegate _next;

    public LoginRequiredMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

        var isStaticAsset = path.Contains(".css") || path.Contains(".js") || path.Contains(".ico") ||
                            path.Contains(".png") || path.Contains(".jpg") || path.Contains(".jpeg") ||
                            path.Contains(".svg") || path.Contains(".woff") || path.Contains(".woff2");

        bool isPublicPage = path == "/" ||
                            path == "/index" ||
                            path == "/aboutproject" ||
                            path.StartsWith("/users/login") ||
                            path.StartsWith("/users/register") ||
                            path.StartsWith("/users/welcome") ||
                            path.StartsWith("/features/users/login") ||
                            path.StartsWith("/features/users/register") ||
                            path.StartsWith("/features/users/welcome") ||
                            isStaticAsset;

        if (context.User.Identity?.IsAuthenticated != true && !isPublicPage)
        {
            context.Response.Redirect("/Index");
            return;
        }

        await _next(context);
    }
}