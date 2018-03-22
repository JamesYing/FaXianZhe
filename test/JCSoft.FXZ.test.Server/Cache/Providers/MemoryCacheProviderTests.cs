using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;
using TestStack.BDDfy;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using FXZServer.Cache.Providers;

namespace JCSoft.FXZ.test.Server.Cache.Providers
{
    public class MemoryCacheProviderTests
    {
        private Mock<IMemoryCache> _memoryCache;
        private Mock<ILoggerFactory> _loggerFactory;
        private Dictionary<string, object> _dict;
        private ICacheProvider _cacheProvider;
        private Mock<ICacheEntry> _cacheEntry;

        public MemoryCacheProviderTests()
        {
            _memoryCache = new Mock<IMemoryCache>();
            _loggerFactory = new Mock<ILoggerFactory>();
            _dict = new Dictionary<string, object>();
            _cacheProvider = new MemoryCacheProvider(_memoryCache.Object, _loggerFactory.Object);
            _cacheEntry = new Mock<ICacheEntry>();
        }

        [Fact]
        public void Should_SaveAndGet_To_Cache()
        {
            var key = "key";
            var value = "value";
            this.Given(s => s.GivenCacheProviderInit(key, value))
                .When(s => s.WhenCacheProviderSaveKeyValue(key, value))
                .Then(s => s.ThenGetValueFromCacheIs(key, value))
                .BDDfy();
        }

        private void ThenGetValueFromCacheIs(string key, object obj)
        {
            var value = _cacheProvider.Get<string>(key);
            value.ShouldBe(obj.ToString());
        }

        private void WhenCacheProviderSaveKeyValue(string key, object obj)
        {
            _cacheProvider.Save(key, obj, DateTimeOffset.MaxValue);
        }

        private void GivenCacheProviderInit(string key, object obj)
        {
            _memoryCache.Setup(s => s.CreateEntry(key)).Returns(() =>
            {
                _dict.Add(key, obj);
                return _cacheEntry.Object;
            });

            //object outobj = null;
            _memoryCache.Setup(s => s.TryGetValue(key, out obj))
                .Returns(() =>
                {
                    return _dict.TryGetValue(key, out obj);
                });
        }
    }
}
