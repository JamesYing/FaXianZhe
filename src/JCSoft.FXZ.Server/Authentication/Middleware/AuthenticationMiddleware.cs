using JCSoft.FXZ.Server.Client;
using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication.Middleware
{
    public class AuthenticationMiddleware : BaseMiddlerware
    {
        private readonly IAuthenticationProvider _authenication;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, 
            IApplicationBuilder app, 
            IClientRequestRepository clientRequestRepository,
            IAuthenticationProviderFactory authencationProviderFactory,
            ILoggerFactory loggerFactory) : 
            base(next, app, clientRequestRepository, loggerFactory)
        {
            _authenication = authencationProviderFactory.CreateAuthencationProvider();
            _logger = loggerFactory.CreateLogger<AuthenticationMiddleware>();
        }


        internal override async void SubInvoke(HttpContext context)
        {
            var response = await _authenication.CheckAuthenicated();
            if (!response.IsError)
            {
                if (response.Data)
                {
                    _logger.LogInformation($"the client has authectication, request is :{Request}");
                    await _next.Invoke(context);
                }
                else
                {
                    _logger.LogError($"the client has not been authentication,client ip :{Request}");
                    await context.Response.WriteAsync("you don't has been authentication, please check");
                    return;
                }
            }
            else
            {
                _logger.LogError($"the request has been exception, the errmessage:{response}");
                await context.Response.WriteAsync($"you have a error, the info is {response}");
                return;
            }
            
        }
    }
}
