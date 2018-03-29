using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.Leader
{
    public interface ILeaderService
    {
        bool IsLeader { get; }
    }
}
