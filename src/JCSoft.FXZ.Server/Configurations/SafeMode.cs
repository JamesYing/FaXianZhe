using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server.Configurations
{
    public enum SafeMode
    {
        /// <summary>
        /// don't need anything
        /// </summary>
        None = 0,
        /// <summary>
        /// need a text for register
        /// </summary>
        Text = 1,
        /// <summary>
        /// need a token for register
        /// </summary>
        Token = 2
    }
}
