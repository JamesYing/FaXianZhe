using JCSoft.FXZ.Server.Cache;
using JCSoft.FXZ.Server.Cache.Providers;
using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Store
{
    public class DefaultStoreProviderFactory : IStoreProviderFactory
    {
        private readonly IClientRequestRepository _reposity;
        private ICacheProvider _cacheProvider;
        private ILoggerFactory _loggerFactory;
        private FXZOptions _options;
        private ILogger<DefaultStoreProviderFactory> _logger;

        public DefaultStoreProviderFactory(IClientRequestRepository repository,
            ICacheProviderFactory cacheProviderFactory,
            ILoggerFactory loggerFactory,
            IOptions<FXZOptions> options)
        {
            _reposity = repository;
            _options = options?.Value;
            _cacheProvider = cacheProviderFactory.CreateProvider(_options.CacheMode);
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<DefaultStoreProviderFactory>();
        }
        public IStoreProvider CreateStoreProvider()
        {
            IStoreProvider provider = new MemoryStoreProvider(_cacheProvider, _loggerFactory);


            return provider;
        }
    }
}
