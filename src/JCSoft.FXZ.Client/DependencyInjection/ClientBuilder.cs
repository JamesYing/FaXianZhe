using JCSoft.FXZ.Client.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Client.DependencyInjection
{
    public class ClientBuilder : IClientBuilder
    {
        private IServiceCollection _services;
        private IConfiguration _configurationRoot;

        public ClientBuilder(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _services = serviceCollection;
            _configurationRoot = configuration;
            _services.Configure<FXZClientOptions>(configuration);
        }
    }
}
