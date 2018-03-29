using JCSoft.FXZ.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCSoft.FXZ.Server
{
    public static class ResponseExtensions
    {
        public static Response TryGetResponse(this object obj, Action action)
        {
            var result = new Response();
            try
            {
                action();
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.ErrorMessage = $"{nameof(obj)} has error, msg:{ex}";
            }

            return result;
        }

        public static Response<T> TryGetResponse<T>(this object obj, Func<T> func)
        {
            var result = new Response<T>();
            try
            {
                result.Data = func();
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.ErrorMessage = $"{nameof(obj)} has error, msg:{ex}";
            }

            return result;
        }
    }
}
