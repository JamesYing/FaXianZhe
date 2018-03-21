using FXZServer.Authentication.Providers;
using FXZServer.Client.Repository;
using FXZServer.Configurations;
using FXZServer.Values;
using FXZServer.Values.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FXZServer.Authentication
{
    public class TextAuthenticationProvider : BaseAuthenticationProvider
    {
        private ClientRequest Request { get; set; }

        private string Text { get; set; }

        public TextAuthenticationProvider(IClientRequestRepository requestRepository,
            IOptions<FXZOptions> options) : base(requestRepository)
        {
            Text = options?.Value?.Token;
        }

        public override Task<Response<Boolean>> CheckAuthenicated()
        {
            var result = new Response<Boolean>();

            Request = _requestRepository?.GetRequest();
            
            if (Request != null && !String.IsNullOrEmpty(Request.Token))
            {
                result.Data = Request.Token.Equals(Text);
            }

            return Task.FromResult(result);
        }
    }
}
