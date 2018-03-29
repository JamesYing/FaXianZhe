using JCSoft.FXZ.Server.Client.Repository;
using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication.Providers
{
    public abstract class BaseAuthenticationProvider : IAuthenticationProvider
    {
        protected readonly IClientRequestRepository _requestRepository;

        public BaseAuthenticationProvider(IClientRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public abstract Task<Response<Boolean>> CheckAuthenicated();
    }
}
