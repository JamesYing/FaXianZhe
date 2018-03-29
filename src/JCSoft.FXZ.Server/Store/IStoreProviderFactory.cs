using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Store
{
    public interface IStoreProviderFactory
    {
        IStoreProvider CreateStoreProvider();
    }
}
