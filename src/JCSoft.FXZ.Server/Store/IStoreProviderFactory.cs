using System.Threading.Tasks;

namespace FXZServer.Store
{
    public interface IStoreProviderFactory
    {
        IStoreProvider CreateStoreProvider();
    }
}
