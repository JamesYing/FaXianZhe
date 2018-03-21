using FXZServer.Authentication;
using FXZServer.Cache;
using FXZServer.Client.Repository;
using FXZServer.Configurations;
using FXZServer.Leader;
using FXZServer.Managers;
using FXZServer.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FXZServer.DependencyInjection
{
    public class ServerBuilder : IServerBuilder
    {
        private IServiceCollection _services;
        private IConfiguration _configurationRoot;

        public ServerBuilder(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _services = serviceCollection;
            _configurationRoot = configuration;
            _services.Configure<FXZOptions>(configuration);
            _services.AddMemoryCache();
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.TryAddSingleton<IClientRequestRepository, DefaultClientRequestRepository>();
            _services.TryAddSingleton<IAuthenticationProviderFactory, DefaultAuthenticationProviderFactory>();
            _services.TryAddSingleton<ILeaderService>(new LeaderService(false));
            _services.TryAddSingleton<ICacheProviderFactory, DefaultCacheProviderFactory>();
            _services.TryAddSingleton<IStoreProviderFactory, DefaultStoreProviderFactory>();
            _services.AddSingleton<IApiManager, ApiManager>();
            //AddAuthecticationProvider();
        }

        public IServerBuilder SetLeader()
        {
            _services.Configure<FXZOptions>(options =>
            {
                options.IsLeader = true;
            });
            var descriptor = new ServiceDescriptor(typeof(ILeaderService), new LeaderService(true));
            _services.Replace(descriptor);
            return this;
        }
    }
}
