using FXZServer;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestStack.BDDfy;
using Xunit;
using Shouldly;
using JCSoft.FXZ.Server;

namespace JCSoft.FXZ.test.Server.Middleware
{
    public class PathMiddlewareTests : BaseMiddlewareTests
    {
        

        public PathMiddlewareTests()
        {
            GivenTheTestServerIsConfigured();
        }

        [Fact]
        public void Should_Is_Service_Register_Path()
        {
            this.Given(p => p.GivenTheRequestRepositoryIs(NotRegisterRequest))
                .When(p => p.WhenICallMiddleware(Url + "/fxz"))
                .Then(p => p.ThenRegisterPathIs(true))
                .BDDfy();
        }

        [Fact]
        public void Should_Not_Is_Service_Register_Path()
        {
            this.Given(p => p.GivenTheRequestRepositoryIs(NotRegisterRequest))
                .When(p => p.WhenICallMiddleware(Url + "/fxz1"))
                .Then(p => p.ThenRegisterPathIs(false))
                .BDDfy();
        }

        protected override void GivenTheTestServerPipelineIsConfigured(IApplicationBuilder app)
        {
            app.UsePathMiddleware();
            SetResponseContext(app, "path checked");
        }

        protected override void GivenTheTestServerServicesAreConfigured(IServiceCollection services)
        {
            base.GivenTheTestServerServicesAreConfigured(services);
            services.AddSingleton(Repository.Object);
            services.Configure<FXZOptions>(o =>
            {
                o.FxzPath = "fxz";
                o.IsLeader = false;
                o.SafeMode = SafeMode.None;
            });
        }

        protected override void GivenTheRequestRepositoryIs(ClientRequest request)
        {
            Repository.Setup(r => r.GetRequest()).Returns(request);
            Repository.Setup(r => r.UpdateRequest(It.IsAny<ClientRequest>())).Verifiable();
        }

        private void ThenRegisterPathIs(bool isRegistered)
        {
            var request = Repository.Object.GetRequest();
            request.IsRegisterServer.ShouldBe(isRegistered);
        }

        protected override string GetRequestUrl()
        {
            return base.GetRequestUrl() + "/fxz";
        }
    }
}
