using JCSoft.FXZ.Client.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JCSoft.FXZ.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFXZClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FXZClientOptions>(configuration);
        }
    }
}
