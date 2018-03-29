using JCSoft.FXZ.Server.Authentication;
using JCSoft.FXZ.Server.Cache;
using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Leader;
using JCSoft.FXZ.Server.Managers;
using JCSoft.FXZ.Server.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JCSoft.FXZ.Server.DependencyInjection
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
            _services.TryAddSingleton<IApiManager, ApiManager>();
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
