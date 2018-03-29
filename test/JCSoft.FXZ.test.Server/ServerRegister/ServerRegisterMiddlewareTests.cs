using FXZServer;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestStack.BDDfy;
using Xunit;
using Shouldly;
using JCSoft.FXZ.Server.ServerRegister.Middlerware;
using JCSoft.FXZ.Server.Managers;
using System;
using System.Net;
using JCSoft.FXZ.Server;

namespace JCSoft.FXZ.test.Server.ServerRegister
{
    public class ServerRegisterMiddlewareTests : BaseMiddlewareTests
    {
        private Mock<IApiManager> _mockApiManager;
        public ServerRegisterMiddlewareTests()
        {
            _mockApiManager = new Mock<IApiManager>();
            GivenTheTestServerIsConfigured();
        }

        [Fact]
        public void Should_Registed_Is_Success()
        {
            var request = new ClientRequest
            {
                ApiService = new ApiService("api 1")
                {
                    Url = "/v2/category",
                    HealthcheckPath = "/hc",
                    Host = "127.0.0.1",
                    
                    Port = "4444"
                },
                IsRegisterServer = true,
            };
            var response = new Response
            {

            };

            this.Given(s => s.GivenTheRequestRepositoryIs(request))
                .When(s => s.WhenTheApiManagerResponseIs(response))
                .When(s => s.WhenICallMiddleware(Url))
                .Then(s => ThenResponseShouldBe(response, false))
                .BDDfy();
        }

        [Fact]
        public void Should_Registed_Is_Faild()
        {
            var request = new ClientRequest
            {
                ApiService = new ApiService("api 1")
                {
                    Url = "/v2/category",
                    HealthcheckPath = "/hc",
                    Host = "127.0.0.1",
                    Port = "4444"
                },
                IsRegisterServer = true,
                
            };
            var response = new Response
            {
                IsError = true
            };

            this.Given(s => s.GivenTheRequestRepositoryIs(request))
                .When(s => s.WhenTheApiManagerResponseIs(response))
                .When(s => s.WhenICallMiddleware(Url))
                .Then(s => ThenResponseShouldBe(response, true))
                .Then(s => ThenResponseStatusCodeIs(HttpStatusCode.InternalServerError))
                .BDDfy();
        }

        private void ThenResponseStatusCodeIs(HttpStatusCode statusCode)
        {
            ResponseMessage.StatusCode.ShouldBe(statusCode);
        }

        private void ThenResponseShouldBe(Response response, bool iserror)
        {
            response.IsError.ShouldBe(iserror);
        }

        private void WhenTheApiManagerResponseIs(Response response)
        {
            _mockApiManager.Setup(m => m.TryAddOrUpdate(It.IsAny<ApiService>()))
                .Returns(response);
        }

        protected override void GivenTheTestServerServicesAreConfigured(IServiceCollection services)
        {
            base.GivenTheTestServerServicesAreConfigured(services);
            services.AddSingleton(Repository.Object);
            services.AddSingleton(_mockApiManager.Object);
            services.Configure<FXZOptions>(o =>
            {
                o.FxzPath = "fxz";
                o.IsLeader = false;
                o.SafeMode = SafeMode.None;
            });
        }

        protected override void GivenTheTestServerPipelineIsConfigured(IApplicationBuilder app)
        {
            app.UseServerRegisterMiddleware();
        }

        protected override void GivenTheRequestRepositoryIs(ClientRequest request)
        {
            Repository.Setup(r => r.GetRequest()).Returns(request);
        }
    }
}
