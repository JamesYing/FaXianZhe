using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer.Managers
{
    public interface IApiManager
    {
        Response<ApiCollections> GetAll();
        Response TryAddOrUpdate(ApiService apiService);
        void RemoveApi(ApiService apiService);
        Response<IEnumerable<ApiService>> GetApiServices(string apiname);
        Response<ApiService> GetApiservice(Guid guid);
    }
}
