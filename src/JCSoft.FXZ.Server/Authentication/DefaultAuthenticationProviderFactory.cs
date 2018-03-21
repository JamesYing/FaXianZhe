using FXZServer.Client;
using FXZServer.Client.Repository;
using FXZServer.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
namespace FXZServer.Authentication
{
    public class DefaultAuthenticationProviderFactory : IAuthenticationProviderFactory
    {
        private readonly IOptions<FXZOptions> _options;
        private readonly ILogger<DefaultAuthenticationProviderFactory> _logger;
        private readonly IClientRequestRepository _requestRespository;
   
        public DefaultAuthenticationProviderFactory(
            IOptions<FXZOptions> options,
            ILoggerFactory loggerFactory,
            IClientRequestRepository requestRespository)
        {
            _options = options;
            _logger = loggerFactory.CreateLogger<DefaultAuthenticationProviderFactory>();
            _requestRespository = requestRespository;
        }
        IAuthenticationProvider IAuthenticationProviderFactory.CreateAuthencationProvider()
        {
            switch (_options.Value.SafeMode)
            {
                case SafeMode.None:
                    return new NoneAuthenticationProvider();
                case SafeMode.Text:
                    return new TextAuthenticationProvider(_requestRespository, _options);
                case SafeMode.Token:
                    return new TokenAuthenticationProvider();
            }

            throw new ArgumentOutOfRangeException("don't support safemode");
        }
    }
}
