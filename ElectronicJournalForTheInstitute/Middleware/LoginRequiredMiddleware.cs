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
        var path = context.Request.Path.Value?.ToLower();

        bool isPublicPage = path == "/" || 
                            path == "/index" || 
                            path.StartsWith("/users/login") || 
                            path.StartsWith("/users/register") || 
                            path.StartsWith("/users/welcome") || 
                            path.StartsWith("/aboutproject") ||
                            path.Contains(".css") || path.Contains(".js") || path.Contains(".ico");
        
        if (context.User.Identity?.IsAuthenticated != true && !isPublicPage)
        {
            context.Response.Redirect("/Index"); 
            return;
        }

        await _next(context); 
    }
}