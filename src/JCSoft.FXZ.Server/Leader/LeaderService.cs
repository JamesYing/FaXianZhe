using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.Leader
{
    public class LeaderService : ILeaderService
    {
        public LeaderService(bool isLeader) => IsLeader = isLeader;
        public bool IsLeader { get; private set; }
    }
}
