using Microsoft.AspNetCore.Builder;

namespace FXZServer.Middleware
{
    public static class PathMiddlewareExtensions
    {
        public static IApplicationBuilder UsePathMiddleware(this IApplicationBuilder builder) =>
           builder.UseMiddleware<PathMiddleware>(builder);
    }
}
