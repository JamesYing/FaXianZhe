using System;
using System.Collections.Generic;
using System.Text;

namespace FXZServer
{
    public class ServerException : Exception
    {
        public ServerException(string message, Exception exception):this(9999, message, exception)
        {
            
        }

        public ServerException(int code, string message, Exception exception) : base(message, exception)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"error code is {ErrorCode}, Message is {ErrorMessage}";
        }
    }
}
