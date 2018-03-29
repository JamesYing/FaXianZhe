using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCSoft.FXZ.Server.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServerBuilder AddFaXianZhe(this IServiceCollection services)
        {
            var service = services.FirstOrDefault(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)service?.ImplementationInstance;
            return services.AddFaXianZhe(configuration);
        }

        public static IServerBuilder AddFaXianZhe(this IServiceCollection services, IConfiguration configuration)
        {
            return new ServerBuilder(services, configuration);
        }
    }
}
