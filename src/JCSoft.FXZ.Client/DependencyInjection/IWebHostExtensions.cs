using JCSoft.FXZ;
using JCSoft.FXZ.Client.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IWebHostExtensions
    {
        public static IWebHost RegisterService(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetService<ILogger<IWebHost>>();
                var options = services.GetService<IOptions<FXZClientOptions>>();
                if (options != null)
                {
                    var config = options.Value;
                    var api = config.ApiService;
                    api.ID = Guid.NewGuid();
                    
                    var request = new Request<ApiService>
                    {
                        Data = api
                    };
                    var header = new Dictionary<string, string>();

                    if (!String.IsNullOrEmpty(config.Token))
                    {
                        header.Add(String.IsNullOrEmpty(config.TokenKey) ? "token" : config.TokenKey, config.Token);
                    }

                    var json = JsonConvert.SerializeObject(request);
                    try
                    {
                        HttpClientHelper.PostAsync(config.FxzServerAddress, json, header)
                            .ContinueWith(r =>
                            {
                                logger.LogInformation($"register is complated, api id is {r.Result}");
                            });
                    }
                    catch(Exception ex)
                    {
                        logger.LogError($"register is failed, ex is {ex}");
                    }
                }
                else
                {
                    logger.LogWarning("IOptions<FXZClientOptions> is empty, please check it");
                }
            }

                

            return host;
        }
    }
}
