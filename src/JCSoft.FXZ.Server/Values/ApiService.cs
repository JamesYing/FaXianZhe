using System;

namespace FXZServer
{
    public class ApiService
    {
        public ApiService(ClientRequest request)
        {
            Name = request.ApiName;
            Url = request.ApiUrl;
            Host = request.Host;
            Port = request.Port;
            HealthcheckPath = request.HealthcheckPath;
            ID = Guid.NewGuid();
            Protocol = request.Protocol;
            AllowedHttpMethod = request.AllowedHttpMethod;
        }

        public ApiService(string apiName) => Name = apiName;

        public string Name { get; set; }

        public string Description { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Url { get; set; }

        public string Alias { get; set; }

        public string HealthcheckPath { get; set; } = "hc";

        public Guid ID { get; set; }

        public string Protocol { get; set; } = "http";

        public string AllowedHttpMethod { get; set; }
    }
}
