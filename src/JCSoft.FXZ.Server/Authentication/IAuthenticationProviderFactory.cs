using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FXZServer.Authentication
{
    public interface IAuthenticationProviderFactory
    {
        IAuthenticationProvider CreateAuthencationProvider();
    }
}
