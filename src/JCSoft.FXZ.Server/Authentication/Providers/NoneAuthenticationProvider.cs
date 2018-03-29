using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication
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
