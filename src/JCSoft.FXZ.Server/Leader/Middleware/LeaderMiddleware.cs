using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Leader.Middleware
{
    public class LeaderMiddleware : BaseMiddlerware
    {
        private readonly ILogger<LeaderMiddleware> _logger;

        public LeaderMiddleware(RequestDelegate next, 
            IApplicationBuilder app, 
            IClientRequestRepository clientRequestRepository, 
            ILoggerFactory factory) : 
            base(next, app, clientRequestRepository, factory)
        {
            _logger = factory.CreateLogger<LeaderMiddleware>();
        }

        internal override async void SubInvoke(HttpContext context)
        {
            _logger.LogInformation($"the server is leader server");
            await _next.Invoke(context);
        }
    }
}
