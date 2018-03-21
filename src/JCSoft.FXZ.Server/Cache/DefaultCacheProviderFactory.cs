using System;
using FXZServer.Cache.Providers;
using FXZServer.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FXZServer.Cache
{
    public class DefaultCacheProviderFactory : ICacheProviderFactory
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DefaultCacheProviderFactory> _logger;
        private readonly ILoggerFactory _loggerFactory;
        public DefaultCacheProviderFactory(IMemoryCache memoryCache, ILoggerFactory loggerFactory)
        {
            _memoryCache = memoryCache;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<DefaultCacheProviderFactory>();
        }
        ICacheProvider ICacheProviderFactory.CreateProvider(CacheMode cacheMode)
        {
            switch (cacheMode)
            {
                case CacheMode.WebCache:
                    return new MemoryCacheProvider(_memoryCache, _loggerFactory);
            }

            _logger.LogError($"don't support this cache mode, please change, now is {cacheMode}");

            throw new ArgumentOutOfRangeException("cacheMode");
        }
    }
}
