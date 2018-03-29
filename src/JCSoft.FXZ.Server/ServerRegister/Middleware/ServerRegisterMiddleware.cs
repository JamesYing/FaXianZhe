using JCSoft.FXZ.Server;
using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Managers;
using JCSoft.FXZ.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace JCSoft.FXZ.Server.ServerRegister.Middlerware
{
    public class ServerRegisterMiddleware : BaseMiddlerware
    {
        private ILogger<ServerRegisterMiddleware> _logger;
        private readonly IApiManager _apiManager;
        public ServerRegisterMiddleware(RequestDelegate next, 
            IApplicationBuilder app, 
            IClientRequestRepository clientRequestRepository, 
            ILoggerFactory factory,
            IApiManager apiManager) : 
            base(next, app, clientRequestRepository, factory)
        {
            _logger = factory.CreateLogger<ServerRegisterMiddleware>();
            _apiManager = apiManager;
        }

        internal override async void SubInvoke(HttpContext context)
        {
            Request = _repository.GetRequest();
            var apiService = Request.ToApiService();
            var response = _apiManager.TryAddOrUpdate(apiService);
            var outMsg = string.Empty;
            if (response.IsError)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError($"registe the api services has exception, the msg is {response.ErrorMessage}");
                return;
            }

            await context.Response.WriteAsync(apiService.ID.ToString());
            return;
        }
    }
}
