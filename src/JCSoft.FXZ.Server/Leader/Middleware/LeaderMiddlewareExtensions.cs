using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.Leader.Middleware
{
    public static class LeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseLeaderMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<LeaderMiddleware>(builder);
    }
}
