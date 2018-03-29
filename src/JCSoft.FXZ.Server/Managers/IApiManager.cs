using JCSoft.FXZ;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.Managers
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
