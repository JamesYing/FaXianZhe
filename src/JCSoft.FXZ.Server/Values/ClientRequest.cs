using System;

namespace JCSoft.FXZ.Server
{
    public class ClientRequest
    {
        public ApiService ApiService { get; set; }

        public string Token { get; set; }

        public string RemoteIp { get; set; }

        public bool IsRegisterServer { get; set; }
       
    }
    
}
