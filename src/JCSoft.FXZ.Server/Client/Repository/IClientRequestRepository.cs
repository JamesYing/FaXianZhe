using JCSoft.FXZ.Server.Values;
using Microsoft.Extensions.Caching.Memory;

namespace JCSoft.FXZ.Server.Client.Repository
{
    public interface IClientRequestRepository
    {
        ClientRequest GetRequest();

        void AddRequest(ClientRequest request);

        void UpdateRequest(ClientRequest request);
    }
}
