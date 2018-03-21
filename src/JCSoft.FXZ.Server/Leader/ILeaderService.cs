using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer.Leader
{
    public interface ILeaderService
    {
        bool IsLeader { get; }
    }
}
