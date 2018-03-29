using JCSoft.FXZ.Server.Store;
using JCSoft.FXZ;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCSoft.FXZ.Server.Managers
{
    public class ApiManager : IApiManager
    {
        private readonly IStoreProvider _storeProvider;
        private readonly ILogger<ApiManager> _logger;
        private static object _lockobj = new object();
        private static bool _isupdated;

        public ApiManager(IStoreProviderFactory storeProviderFactory,
            ILoggerFactory loggerFactory)
        {
            _storeProvider = storeProviderFactory.CreateStoreProvider();
            _logger = loggerFactory.CreateLogger<ApiManager>();
            
            AllServices = new List<ApiService>();
            _isupdated = true;
            InitDatas();
        }

        private void InitDatas()
        {
            if (_isupdated)
            {
                lock (_lockobj)
                {
                    Sets = _storeProvider.Get<ApiCollections>(ApiManagerCacheKey) ?? new ApiCollections();
                    InitAllServices();                   
                }

                _isupdated = false;
            }
            
        }

        private void InitAllServices()
        {
            if (Sets != null && Sets.Count > 0)
            {
                foreach (var key in Sets.Keys)
                {
                    foreach (var api in Sets[key])
                    {
                        if (!AllServices.Any(a => a.ID == api.ID))
                        {
                            AllServices.Add(api);
                        }
                    }
                }
            }
        }

        private List<ApiService> AllServices { get; set; }

        private const string ApiManagerCacheKey = "FXZ_API_MANAGER";
        public Response<ApiCollections> GetAll()
        {
            return this.TryGetResponse(() => Sets);
        }

        private ApiCollections Sets { get; set; }

        public Response<ApiService> GetApiservice(Guid guid)
        {
            return this.TryGetResponse(() => AllServices.Find(a => a.ID == guid));
        }

        public Response<IEnumerable<ApiService>> GetApiServices(string apiname)
        {
            return this.TryGetResponse(() =>
            {
                IEnumerable<ApiService> result = null;
                if (Sets != null && Sets.ContainsKey(apiname))
                {
                    result = Sets[apiname];
                }

                return result;
            });
        }

        public void RemoveApi(ApiService apiService)
        {
            if (Sets.ContainsKey(apiService.Name))
            {
                var services = Sets[apiService.Name];
                var find = services.Find(a => a.ID == apiService.ID);
                if (find != null)
                {
                    services.Remove(find);
                    _isupdated = true;
                }

            }
        }

        public Response TryAddOrUpdate(ApiService apiService)
        {
            return this.TryGetResponse(() =>
            {
                if (Sets.ContainsKey(apiService.Name))
                {
                    var services = Sets[apiService.Name];
                    var find = services.Find(a => a.ID == apiService.ID);
                    if (find != null)
                    {
                        find.Host = apiService.Host;
                        find.HealthcheckPath = apiService.HealthcheckPath;
                        find.Alias = apiService.Alias;
                        find.Description = apiService.Description;
                        find.Url = apiService.Url;
                        find.Port = apiService.Port;
                    }
                    else
                    {
                        services.Add(apiService);
                    }

                    _isupdated = true;
                }
                else
                {
                    Sets.Add(apiService.Name, new List<ApiService> { apiService });
                    _isupdated = true;
                }
            });
           
        }

    }
}
