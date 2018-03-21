using System;
using System.Threading.Tasks;
using FXZServer.Client.Repository;
using FXZServer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Linq;

namespace FXZServer.Middleware
{
    public class PathMiddleware
    {
        private FXZOptions _options;
        private RequestDelegate _next;
        private readonly IApplicationBuilder _app;
        private IClientRequestRepository _repository;
        public PathMiddleware(RequestDelegate next, 
            IApplicationBuilder app, 
            IClientRequestRepository clientRequestRepository, 
            IOptions<FXZOptions> options)
        {
            _options = options.Value;
            _next = next;
            _app = app;
            _repository = clientRequestRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CheckFXZPath(context);

            await _next.Invoke(context);
        }


        private void CheckFXZPath(HttpContext context)
        {
            if (context.Request.Path.HasValue)
            {
                var paths = context.Request.Path.Value.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (paths != null && paths.Length > 0)
                {
                    var request = _repository.GetRequest();
                    request.IsRegisterServer = paths.Any(p => p.Equals(_options.FxzPath, StringComparison.OrdinalIgnoreCase));
                    _repository.UpdateRequest(request);
                }
            }
            
        }
    }
}
