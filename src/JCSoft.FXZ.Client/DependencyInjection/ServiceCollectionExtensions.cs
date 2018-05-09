using JCSoft.FXZ.Client.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JCSoft.FXZ.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IClientBuilder AddFXZClient(this IServiceCollection services, IConfiguration configuration)
        {
            return new ClientBuilder(services, configuration);
        }
    }
}
