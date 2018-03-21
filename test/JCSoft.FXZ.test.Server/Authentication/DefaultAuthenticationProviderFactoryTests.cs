using FXZServer.Authentication;
using FXZServer.Client.Repository;
using FXZServer.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestStack.BDDfy;
using Shouldly;
using Xunit;

namespace JCSoft.FXZ.test.Server.Authentication
{
    public class DefaultAuthenticationProviderFactoryTests
    {
        private Mock<ILoggerFactory> _loggerFactory;
        private Mock<IClientRequestRepository> _repository;
        private Mock<IOptions<FXZOptions>> _options;
        private Mock<ILogger<DefaultAuthenticationProviderFactory>> _logger;
        private IAuthenticationProvider provider;

        public DefaultAuthenticationProviderFactoryTests()
        {
            _loggerFactory = new Mock<ILoggerFactory>();
            _repository = new Mock<IClientRequestRepository>();
            _options = new Mock<IOptions<FXZOptions>>();
            _logger = new Mock<ILogger<DefaultAuthenticationProviderFactory>>();
            _loggerFactory.Setup(l => l.CreateLogger(It.IsAny<String>()))
                .Returns(_logger.Object);
        }

        [Fact]
        public void Should_Is_NoneAuthenticationProvider()
        {
            this.Given(d => d.GivenSetNoneOptions(SafeMode.None))
                .When(d => d.WhenCreateAuthenctionProvider())
                .Then(d => d.ThenCheckProvider(typeof(NoneAuthenticationProvider)))
                .BDDfy();
        }

        [Fact]
        public void Should_Is_TextAuthenticationProvider()
        {
            this.Given(d => d.GivenSetNoneOptions(SafeMode.Text))
                .When(d => d.WhenCreateAuthenctionProvider())
                .Then(d => d.ThenCheckProvider(typeof(TextAuthenticationProvider)))
                .BDDfy();
        }

        [Fact]
        public void Should_Is_TokenAuthenticationProvider()
        {
            this.Given(d => d.GivenSetNoneOptions(SafeMode.Token))
                .When(d => d.WhenCreateAuthenctionProvider())
                .Then(d => d.ThenCheckProvider(typeof(TokenAuthenticationProvider)))
                .BDDfy();
        }

        private void WhenCreateAuthenctionProvider()
        {
            IAuthenticationProviderFactory factory = new DefaultAuthenticationProviderFactory(_options.Object, _loggerFactory.Object, _repository.Object);
            provider = factory.CreateAuthencationProvider();
        }

        private void ThenCheckProvider(Type type)
        {
            provider.ShouldBeOfType(type);
        }

        private void GivenSetNoneOptions(SafeMode mode)
        {
            _options.SetupGet(o => o.Value).Returns(new FXZOptions
            {
                SafeMode = mode
            });
        }
    }
}
