using System;
using System.Threading.Tasks;

namespace FXZServer.Authentication
{
    public class NoneAuthenticationProvider : IAuthenticationProvider
    {


        Task<Response<Boolean>> IAuthenticationProvider.CheckAuthenicated() =>
            new Task<Response<Boolean>>(() => new Response<bool>()
            {
                Data = true
            });
    }
}
