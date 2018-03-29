using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Store
{
    public interface IStoreProvider
    {
        T Get<T>(string key);

        T Save<T>(string key, T t, DateTimeOffset absoluteExpiration);
        void Remove(string key);
        Task<T> Retrieve<T>(string key, Func<T> func, DateTimeOffset absoluteExpiration);
    }
}
