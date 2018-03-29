using Microsoft.AspNetCore.Builder;

namespace JCSoft.FXZ.Server.Middleware
{
    public static class PathMiddlewareExtensions
    {
        public static IApplicationBuilder UsePathMiddleware(this IApplicationBuilder builder) =>
           builder.UseMiddleware<PathMiddleware>(builder);
    }
}
