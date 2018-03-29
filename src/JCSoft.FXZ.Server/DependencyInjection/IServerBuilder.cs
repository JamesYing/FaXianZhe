using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.DependencyInjection
{
    public interface IServerBuilder
    {
        IServerBuilder SetLeader();
    }
}
