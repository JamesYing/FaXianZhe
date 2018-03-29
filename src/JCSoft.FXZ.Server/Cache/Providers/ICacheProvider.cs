using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Cache.Providers
{
    public interface ICacheProvider
    {
        T Save<T>(string key, T value, DateTimeOffset absoluteExpiration);
        void Remove(string key);
        T Get<T>(string key);
        Task<T> RetrieveAsync<T>(string key, Func<T> func, DateTimeOffset absoluteExpiration);
    }
}
