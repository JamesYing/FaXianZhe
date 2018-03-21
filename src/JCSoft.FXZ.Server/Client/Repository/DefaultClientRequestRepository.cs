using System;
using System.Collections.Generic;
using System.Text;
using FXZServer.Values;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FXZServer.Client.Repository
{
    public class DefaultClientRequestRepository : IClientRequestRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DefaultClientRequestRepository> _logger;
        private const string _fxzTokenKey = "fxzToken";
        private const string _requestKey = "fxzRequest";
        private const string _apiNameKey = "apiname";
        private const string _hostKey = "host";
        private const string _portKey = "port";
        private const string _apiurl = "apiurl";
        private const string _protocolKey = "protocol";
        private const string _allowedMethod = "allowedMethod";


        public DefaultClientRequestRepository(IHttpContextAccessor httpContextAccessor,
            ILoggerFactory factory)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = factory.CreateLogger<DefaultClientRequestRepository>();
        }

        public ClientRequest GetRequest()
        {
            ClientRequest request = new ClientRequest();
            if (_httpContextAccessor.HttpContext != null)
            {
                if (_httpContextAccessor.HttpContext.Items != null && _httpContextAccessor.HttpContext.Items.ContainsKey(_requestKey))
                {
                    request = _httpContextAccessor.HttpContext.Items[_requestKey] as ClientRequest;
                }
                if (_httpContextAccessor.HttpContext.Request != null)
                {
                    try
                    {
                        request.Token = GetToken();
                        request.ApiName = GetValueFromHttpRequest(_apiNameKey);
                        request.Host = GetValueFromHttpRequest(_hostKey);
                        request.Port = GetValueFromHttpRequest(_portKey);
                        request.Protocol = GetValueFromHttpRequest(_protocolKey, "http");
                        request.ApiUrl = GetValueFromHttpRequest(_apiurl);
                        request.AllowedHttpMethod = GetValueFromHttpRequest(_allowedMethod, ClientRequest.DefaultAllowedHttpMethod);
                    }
                    catch(Exception ex)
                    {
                        request.Token = String.Empty;
                        _logger.LogError($"GeToken has been error, the error info:{ex.ToString()}");
                    }
                    
                }
                
                request.RemoteIp = GetRemoteIp();
            }
            return request;
        }

        private string GetRemoteIp() => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        private string GetToken() => FromHeaderMessage(_fxzTokenKey) ?? GetValueFromHttpRequest(_fxzTokenKey);

        private string GetValueFromHttpRequest(string key, string defaultValue = "")
        {
            var result = FromQueryMessage(key);
            if (String.IsNullOrEmpty(result))
            {
                result = FromFormMessage(key);
                if (String.IsNullOrEmpty(result))
                {
                    return defaultValue;
                }
            }

            return result;
        }

        private string FromHeaderMessage(string key) => _httpContextAccessor?.HttpContext?.Request?.Headers?[key];

        private string FromQueryMessage(string key) => _httpContextAccessor?.HttpContext?.Request?.Query?[key];

        private string FromFormMessage(string key) => _httpContextAccessor?.HttpContext?.Request?.Form?[key];

        public void AddRequest(ClientRequest request)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Items != null)
            {
                _httpContextAccessor.HttpContext.Items.Add(_requestKey, request);
            }
            else
            {
                _logger.LogError($"the http context is null, user request:{request}");
            }
        }

        public void UpdateRequest(ClientRequest request)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Items != null)
            {
                if (_httpContextAccessor.HttpContext.Items.ContainsKey(_requestKey))
                {
                    _httpContextAccessor.HttpContext.Items[_requestKey] = request;
                }
                else
                {
                    AddRequest(request);
                }
            }
            else
            {
                _logger.LogError($"the http context is null, user request:{request}");
            }

        }
    }
}
