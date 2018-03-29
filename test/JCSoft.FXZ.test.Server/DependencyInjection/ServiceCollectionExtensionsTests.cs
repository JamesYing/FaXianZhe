using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using TestStack.BDDfy;
using JCSoft.FXZ.Server.DependencyInjection;
using JCSoft.FXZ.Server.Configurations;
using Microsoft.Extensions.Options;
using System.Linq;
using JCSoft.FXZ.Server.Leader;

namespace JCSoft.FXZ.test.Server.DependencyInjection
{
    public class ServiceCollectionExtensionsTests
    {
        private IServiceCollection _services;
        private IServiceProvider _serviceProvider;
        private IConfiguration _configRoot;
        private IServerBuilder _serverBuilder;
        private Exception _ex;

        public ServiceCollectionExtensionsTests()
        {
            IWebHostBuilder builder = new WebHostBuilder();
            _configRoot = new ConfigurationRoot(new List<IConfigurationProvider>());
            _services = new ServiceCollection();
            _services.AddSingleton(builder);
            _services.AddSingleton<IHostingEnvironment, HostingEnvironment>();
            _services.AddSingleton<IConfiguration>(_configRoot);
        }

        [Fact]
        public void Should_Setup_Without_Config()
        {
            this.When(s => s.WhenWithoutConfigSetup())
                .Then(s => s.ThenAnExceptionIsntThrown())
                .Then(s => s.ThenIsLeaderServiceCheck(false))
                .BDDfy();

        }

        [Fact]
        public void Should_Setup_Config()
        {
            this.When(s => s.WhenConfigSetup())
                .Then(s => s.ThenAnExceptionIsntThrown())
                .Then(s => s.ThenIsLeaderServiceCheck(false))
                .BDDfy();
        }

        [Fact]
        public void Should_Setup_SetLeader() => this.When(s => s.WhenSetLeaderSetup())
                .Then(s => s.ThenAnExceptionIsntThrown())
                .Then(s => s.ThenIsLeaderServiceCheck(true))
                .BDDfy();


        private void WhenWithoutConfigSetup()
        {
            try
            {
                _serverBuilder = _services.AddFaXianZhe();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }

        private void WhenConfigSetup()
        {
            try
            {
                _serverBuilder = _services.AddFaXianZhe(_configRoot);
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }

        private void WhenSetLeaderSetup()
        {
            try
            {
                _serverBuilder = _services.AddFaXianZhe(_configRoot).SetLeader();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }

        private void ThenAnExceptionIsntThrown()
        {
            _ex.ShouldBeNull();
        }

        private void ThenIsLeaderServiceCheck(bool isLeader)
        {
            _serviceProvider = _services.BuildServiceProvider();
            var leaderService = _serviceProvider.GetService<ILeaderService>();

            leaderService.ShouldNotBeNull();
            leaderService.IsLeader.ShouldBe(isLeader);
        }
    }
}
