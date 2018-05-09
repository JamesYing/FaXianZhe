using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JCSoft.FXZ
{
    public class HttpClientHelper
    {
        private static HttpClient _client = new HttpClient();
        static HttpClientHelper()
        {
           
        }

        public static async Task<string> PostAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            
            if (headers != null && headers.Count > 0)
            {
                request.Headers.Clear();
                foreach(var key in headers.Keys)
                {
                    request.Headers.Add(key, headers[key]);
                }
            }

            request.Content = content;

            var response = await _client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
