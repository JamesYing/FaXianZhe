using FXZServer.Values.Responses;
using System;
using System.Threading.Tasks;

namespace FXZServer.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<Response<Boolean>> CheckAuthenicated();
    }
}
