using JCSoft.FXZ;
using JCSoft.FXZ.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server
{
    public static class ApiServiceExtensions
    {
        public static ApiService ToApiService(this ClientRequest request)
        {
            return request.ApiService;
        }
    }
}
