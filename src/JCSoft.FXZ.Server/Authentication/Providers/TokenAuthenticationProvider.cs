using JCSoft.FXZ.Server.Client;
using JCSoft.FXZ.Server.Values.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication
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
