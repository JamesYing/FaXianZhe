using FXZServer;
using FXZServer.Client.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.IO;
using System.Net.Http;

namespace JCSoft.FXZ.test.Server
{
    public abstract class BaseMiddlewareTests : IDisposable
    {
        protected Mock<IClientRequestRepository> Repository;
        protected TestServer Server { get; private set; }
        protected string Url { get; private set; }
        protected HttpResponseMessage ResponseMessage { get;  set; }
        protected HttpClient Client { get;  set; }

        public BaseMiddlewareTests()
        {
            Url = "http://localhost:12345";
            Repository = new Mock<IClientRequestRepository>();
        }

        protected virtual void GivenTheTestServerIsConfigured()
        {
            var builder = new WebHostBuilder()
              .ConfigureServices(x => GivenTheTestServerServicesAreConfigured(x))
              .UseUrls(Url)
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .Configure(app => GivenTheTestServerPipelineIsConfigured(app));

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        protected virtual void GivenTheTestServerPipelineIsConfigured(IApplicationBuilder app)
        {
           
        }

        protected virtual void SetResponseContext(IApplicationBuilder app, string content)
        {
            app.Run(async x =>
            {
                await x.Response.WriteAsync(content);
            });
        }

        protected virtual void GivenTheTestServerServicesAreConfigured(IServiceCollection services)
        {
            services.AddLogging();
        }

        public void Dispose()
        {
            Client?.Dispose();
            ResponseMessage?.Dispose();
        }

        protected void WhenICallMiddleware(string url)
        {
            ResponseMessage = Client.GetAsync(url).Result;
        }

        protected virtual string GetRequestUrl()
        {
            return Url;
        }

        protected ClientRequest IsRegisterRequest
        {
            get
            {
                return new ClientRequest
                {
                    IsRegisterServer = true
                };
            }
        }

        protected ClientRequest NotRegisterRequest
        {
            get
            {
                return new ClientRequest
                {
                    IsRegisterServer = false
                };
            }
        }

        protected virtual void GivenTheRequestRepositoryIs(ClientRequest request)
        {

        }
    }
}
