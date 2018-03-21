using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer.Leader
{
    public class LeaderService : ILeaderService
    {
        public LeaderService(bool isLeader) => IsLeader = isLeader;
        public bool IsLeader { get; private set; }
    }
}
