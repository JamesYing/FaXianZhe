using FXZServer.Authentication.Middleware;
using FXZServer.Configurations;
using FXZServer.Leader.Middleware;
using FXZServer.ServerRegister.Middlerware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FXZServer.Middleware
{
    public static class FXZServerrMiddleware
    {
        public static Task<IApplicationBuilder> UseFXZServer(this IApplicationBuilder builder)
        {
            builder.UsePathMiddleware();
            //first verfily client auth
            builder.UseAuthenticationMiddleware();

            if (UsingLeader(builder))
            {
                builder.UseLeaderMiddleware();
            }

            builder.UseServerRegisterMiddleware();

            return Task.FromResult(builder);
        }

        private static bool UsingLeader(IApplicationBuilder builder)
        {
            var options = GetOptionsByDependencies(builder);
            if (options != null && options.Value != null)
            {
                return options.Value.IsLeader;
            }

            return false;
        }

        private static IOptions<FXZOptions> GetOptionsByDependencies(IApplicationBuilder builder)
        {
            var options = (IOptions<FXZOptions>)builder.ApplicationServices.GetService(typeof(IOptions<FXZOptions>));

            return options;
        }
    }
}
