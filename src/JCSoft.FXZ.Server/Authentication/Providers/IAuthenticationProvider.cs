using JCSoft.FXZ.Server.Values.Responses;
using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<Response<Boolean>> CheckAuthenicated();
    }
}
