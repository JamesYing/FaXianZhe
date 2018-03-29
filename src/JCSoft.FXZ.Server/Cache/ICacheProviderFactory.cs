using JCSoft.FXZ.Server.Cache.Providers;
using JCSoft.FXZ.Server.Configurations;

namespace JCSoft.FXZ.Server.Cache
{
    public interface ICacheProviderFactory
    {
        ICacheProvider CreateProvider(CacheMode cacheMode);
    }
}
