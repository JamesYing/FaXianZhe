using Microsoft.AspNetCore.Builder;

namespace FXZServer.ServerRegister.Middlerware
{
    public static class ServerRegisterMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerRegisterMiddleware(this IApplicationBuilder builder) =>
          builder.UseMiddleware<ServerRegisterMiddleware>(builder);
    }
}
