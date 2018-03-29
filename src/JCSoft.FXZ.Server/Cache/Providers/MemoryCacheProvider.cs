using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Cache.Providers
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private IMemoryCache _cache;
        private readonly ILogger<MemoryCacheProvider> _logger;
        public MemoryCacheProvider(IMemoryCache memoryCache,
            ILoggerFactory loggerFactory)
        {
            _cache = memoryCache;
            _logger = loggerFactory.CreateLogger<MemoryCacheProvider>();
        }
        public T Get<T>(string key)
        {
            T t;
            
            if (!_cache.TryGetValue<T>(key, out t))
            {
                _logger.LogTrace($"don't get this value in memory cache, key is {key}");
            }

            return t;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public T Save<T>(string key, T value, DateTimeOffset absoluteExpiration)
        {
            return _cache.Set(key, value, absoluteExpiration);
        }

        public async Task<T> RetrieveAsync<T>(string key, Func<T> func, DateTimeOffset absoluteExpiration)
        {
            return await _cache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(DateTimeOffset.MaxValue);
                var result = func();

                return Task.FromResult(result);
            });
        }
    }
}
