using Microsoft.AspNetCore.Builder;

namespace JCSoft.FXZ.Server.ServerRegister.Middlerware
{
    public static class ServerRegisterMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerRegisterMiddleware(this IApplicationBuilder builder) =>
          builder.UseMiddleware<ServerRegisterMiddleware>(builder);
    }
}
