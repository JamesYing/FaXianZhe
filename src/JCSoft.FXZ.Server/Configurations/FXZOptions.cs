using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer.Configurations
{
    public class FXZOptions
    {
        public bool IsLeader { get; set; }

        public string FxzPath { get; set; } = "fxz";

        public SafeMode SafeMode { get; set; } = SafeMode.None;

        public string Token { get; set; } = "fxz";

        public StoreMode StoreMode { get; set; } = StoreMode.Memory;

        public CacheMode CacheMode { get; set; } = CacheMode.WebCache;
    }
}
