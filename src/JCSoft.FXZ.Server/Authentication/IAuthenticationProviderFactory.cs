using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication
{
    public interface IAuthenticationProviderFactory
    {
        IAuthenticationProvider CreateAuthencationProvider();
    }
}
