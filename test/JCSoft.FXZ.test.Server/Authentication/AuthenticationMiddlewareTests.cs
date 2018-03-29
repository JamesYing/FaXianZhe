using FXZServer;
using JCSoft.FXZ.Server;
using JCSoft.FXZ.Server.Authentication;
using JCSoft.FXZ.Server.Authentication.Middleware;
using JCSoft.FXZ.Server.Client.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace JCSoft.FXZ.test.Server.Authentication
{
    public class AuthenticationMiddlewareTests : BaseMiddlewareTests
    {
        private Mock<IAuthenticationProviderFactory> _providerFactory;
        private Mock<IAuthenticationProvider> _provider;
        public AuthenticationMiddlewareTests()
        {
            _providerFactory = new Mock<IAuthenticationProviderFactory>();
            _provider = new Mock<IAuthenticationProvider>();
            _providerFactory.Setup(p => p.CreateAuthencationProvider()).Returns(_provider.Object);
            GivenTheTestServerIsConfigured();
        }

        [Fact]
        public void Should_Is_Authenticated()
        {
            var response = new Response<Boolean>()
            {
                Data = true
            };
            
            this.Given(a => a.GivenTheAuthenicatedIs(response))
                .When(a => a.WhenICallMiddleware(Url))
                .Then(a => ThenTheUserIsAuthenticated())
                .BDDfy();
        }

        [Fact]
        public void Should_Not_IsAuthenticated()
        {
            var response = new Response<Boolean>()
            {
                Data = false
            };

            this.Given(a => a.GivenTheAuthenicatedIs(response))
                .When(a => a.WhenICallMiddleware(Url))
                .Then(a => a.ThenTheUserNotIsAuthenticated())
                .BDDfy();
        }

        [Fact]
        public void Should_When_Authentication_Has_Exception()
        {
            var response = new Response<Boolean>
            {
                IsError = true,
                ErrorCode = 10001,
                ErrorMessage = "error"
            };

            this.Given(a => a.GivenTheAuthenicatedIs(response))
                .When(a => a.WhenICallMiddleware(Url))
                .Then(a => a.ThenTheAuthenticationHasException(response))
                .BDDfy();
        }

        private void ThenTheAuthenticationHasException(Response<Boolean> response)
        {
            var content = ResponseMessage.Content.ReadAsStringAsync().Result;
            content.ShouldBe($"you have a error, the info is {response}");
        }
        private void ThenTheUserIsAuthenticated()
        {
            var content = ResponseMessage.Content.ReadAsStringAsync().Result;
            content.ShouldBe("User is Authenticated");
        }

        private void ThenTheUserNotIsAuthenticated()
        {
            var content = ResponseMessage.Content.ReadAsStringAsync().Result;
            content.ShouldBe("you don't has been authentication, please check");
        }

        private void GivenTheAuthenicatedIs(Response<Boolean> response)
        {
            Repository.Setup(r => r.GetRequest()).Returns(IsRegisterRequest);
            _provider.Setup(p => p.CheckAuthenicated()).Returns(Task.FromResult(response));
        }

        protected override void GivenTheTestServerPipelineIsConfigured(IApplicationBuilder app)
        {
            app.UseAuthenticationMiddleware();
            SetResponseContext(app, "User is Authenticated");
        }

        protected override void GivenTheTestServerServicesAreConfigured(IServiceCollection services)
        {
            base.GivenTheTestServerServicesAreConfigured(services);
            services.AddSingleton(Repository.Object);
            services.AddSingleton(_providerFactory.Object);
        }
    }
}
