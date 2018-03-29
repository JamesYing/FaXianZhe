using Shouldly;
using Xunit;
using TestStack.BDDfy;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using JCSoft.FXZ.Server.Cache;
using Microsoft.Extensions.Logging;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Cache.Providers;
using System;

namespace JCSoft.FXZ.test.Server.Cache
{
    public class DefaultCacheProviderFactoryTests
    {
        private Mock<IMemoryCache> _mockMemoryCache;
        private Mock<ILoggerFactory> _mockLoggerFactory;
        private ICacheProviderFactory _cacheProviderFactory;
        private ICacheProvider _cacheProvider;
        private Exception _ex;

        public DefaultCacheProviderFactoryTests()
        {
            _mockLoggerFactory = new Mock<ILoggerFactory>();
            _mockMemoryCache = new Mock<IMemoryCache>();
        }

        [Fact]
        public void Should_Create_WebCache_Success()
        {
            this.Given(s => WhenCreateCacheProvider())
                .When(s => WhenCacheModeIs(CacheMode.WebCache))
                .Then(s => ThenCacheProvoiderTypeIs<MemoryCacheProvider>())
                .BDDfy();
        }

        [Fact]
        public void Should_Create_CacheProvider_Failed()
        {
            this.Given(s => WhenCreateCacheProvider())
                .When(s => WhenCacheModeIs(CacheMode.Redis))
                .Then(s => ThenThrowAnException())
                .BDDfy();
        }

        private void ThenThrowAnException()
        {
            _ex.ShouldNotBeNull();
        }

        private void ThenCacheProvoiderTypeIs<T>()
        {
            _cacheProvider.ShouldBeOfType<T>();
        }

        private void WhenCacheModeIs(CacheMode cacheMode)
        {
            try
            {
                _cacheProvider = _cacheProviderFactory.CreateProvider(cacheMode);
            }
            catch(Exception ex)
            {
                _ex = ex;
            }
        }

        private void WhenCreateCacheProvider()
        {
            _cacheProviderFactory = new DefaultCacheProviderFactory(_mockMemoryCache.Object, _mockLoggerFactory.Object);
        }
    }
}
