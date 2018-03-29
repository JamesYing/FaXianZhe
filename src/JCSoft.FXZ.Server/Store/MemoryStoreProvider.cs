using JCSoft.FXZ.Server.Cache.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Store
{
    public class MemoryStoreProvider : IStoreProvider
    {
        private ICacheProvider _cacheProvider;
        private readonly ILogger<MemoryStoreProvider> _logger;
        public MemoryStoreProvider(ICacheProvider cacheProvider,
            ILoggerFactory loggerFactory)
        {
            _cacheProvider = cacheProvider;
            _logger = loggerFactory.CreateLogger<MemoryStoreProvider>();
        }


        public void Remove(string key)
        {
            _cacheProvider.Remove(key);
        }

        public async Task<T> Retrieve<T>(string key, Func<T> func, DateTimeOffset absoluteExpiration)
        {
            return await _cacheProvider.RetrieveAsync(key, func, absoluteExpiration);
        }

        public T Get<T>(string key)
        {
            return _cacheProvider.Get<T>(key);
        }

        public T Save<T>(string key, T t, DateTimeOffset absoluteExpiration)
        {
            return _cacheProvider.Save(key, t, absoluteExpiration);
        }
    }
}
