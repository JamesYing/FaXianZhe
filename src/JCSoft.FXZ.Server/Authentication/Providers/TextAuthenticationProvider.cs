using JCSoft.FXZ.Server.Authentication.Providers;
using JCSoft.FXZ.Server.Client.Repository;
using JCSoft.FXZ.Server.Configurations;
using JCSoft.FXZ.Server.Values;
using JCSoft.FXZ.Server.Values.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace JCSoft.FXZ.Server.Authentication
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
