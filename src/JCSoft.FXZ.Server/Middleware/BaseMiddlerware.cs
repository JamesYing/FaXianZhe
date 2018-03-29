using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Values;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Middleware
{
    public abstract class BaseMiddlerware
    {
        protected readonly RequestDelegate _next;
        protected readonly IApplicationBuilder _app;
        protected readonly IClientRequestRepository _repository;
        public BaseMiddlerware(RequestDelegate next,
            IApplicationBuilder app,
            IClientRequestRepository clientRequestRepository,
            ILoggerFactory factory)
        {
            _next = next;
            _app = app;
            _repository = clientRequestRepository;
            MiddlewareName = this.GetType().Name;
        }

        public string MiddlewareName { get; set; }
        public ClientRequest Request { get; set; }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            Request = _repository.GetRequest();
            if (Request.IsRegisterServer)
            {
                  SubInvoke(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        internal abstract void SubInvoke(HttpContext context);
    }
}
