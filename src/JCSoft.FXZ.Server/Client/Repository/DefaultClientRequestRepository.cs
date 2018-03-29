using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JCSoft.FXZ.Server.Values;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JCSoft.FXZ.Server.Client.Repository
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
        private const string _apiserviceKey = "fxzApiService";


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
                    return request;
                }
                if (_httpContextAccessor.HttpContext.Request != null)
                {
                    try
                    {
                        request.Token = GetToken();
                        request.ApiService = GetApiService();
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError($"GeToken has been error, the error info:{ex.ToString()}");
                    }
                    
                }
                
                request.RemoteIp = GetRemoteIp();
                UpdateRequest(request);
            }

            return request;
        }

        private ApiService GetApiService()
        {
            using (var bodyStream = _httpContextAccessor.HttpContext.Request.Body)
            using (var bodyReader = new StreamReader(bodyStream))
            {
                var body = bodyReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Request<ApiService>>(body);

                return result?.Data;
            }
        }

        private string GetRemoteIp() => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        private string GetToken() => FromHeaderMessage(_fxzTokenKey);

        private string GetValueFromHttpRequest(string key, string defaultValue = "")
        {
            var result = defaultValue;
            try
            {
                result = FromQueryMessage(key);
                if (String.IsNullOrEmpty(result))
                {
                    result = FromFormMessage(key);
                    if (String.IsNullOrEmpty(result))
                    {
                        result = defaultValue;
                    }
                }

            }
            catch(Exception ex)
            {
                result = defaultValue;
                _logger.LogError($"Get Value has been error, key is {key}, the error info:{ex.ToString()}");
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
