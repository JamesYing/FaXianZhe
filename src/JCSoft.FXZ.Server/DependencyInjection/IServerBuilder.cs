using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer.DependencyInjection
{
    public interface IServerBuilder
    {
        IServerBuilder SetLeader();
    }
}
