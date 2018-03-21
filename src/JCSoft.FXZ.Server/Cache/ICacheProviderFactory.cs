using FXZServer.Cache.Providers;
using FXZServer.Configurations;

namespace FXZServer.Cache
{
    public interface ICacheProviderFactory
    {
        ICacheProvider CreateProvider(CacheMode cacheMode);
    }
}
