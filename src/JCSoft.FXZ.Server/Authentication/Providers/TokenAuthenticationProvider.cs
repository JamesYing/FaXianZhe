using FXZServer.Client;
using FXZServer.Values.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FXZServer.Authentication
{
    public class TokenAuthenticationProvider : IAuthenticationProvider
    {
        public TokenAuthenticationProvider()
        {

        }

        Task<Response<Boolean>> IAuthenticationProvider.CheckAuthenicated()
        {
            var response = new Response<Boolean>()
            {
                IsError = true,
                ErrorCode = 10001,
                ErrorMessage = "token is error"
            };

            return Task.FromResult(response);
        }
    }
}
