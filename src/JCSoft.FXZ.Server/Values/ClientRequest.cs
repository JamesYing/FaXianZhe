using System;

namespace FXZServer
{
    public class ClientRequest
    {
        public const string DefaultAllowedHttpMethod = "GET,POST,PUT,DELETE";

        public string Token { get; set; }

        public string ApiName { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Protocol { get; set; } = "http";

        public string RemoteIp { get; set; }

        public string ApiUrl { get; set; }

        public string AllowedHttpMethod { get; set; }

        public override string ToString()
        {
            return $"apiname:{ApiName}, host:{Host}, port:{Port}, remoteip:{RemoteIp}, token:{Token}, url:{ApiUrl}";
        }

        public bool IsRegisterServer { get; set; }

        public string HealthcheckPath { get; set; } = "/hc";
    }
    
}
