using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Http.Library.Models;

namespace Http.Library.Factories
{
    internal class HttpWebRequestGenerator
    {
        internal HttpServiceSettings _settings;

        public HttpWebRequestGenerator(HttpServiceSettings settings)
        {
            _settings = settings;
        }

        public HttpWebRequest Erstelle_Request(Uri uri, HttpMethod method)
        {
            var webRequest = Erstelle_WebRequest(uri, method);
            Ergaenze_Custom_Headers(webRequest);
            Ergaenze_Proxy(webRequest);
            Ergaenze_ContentType(webRequest, method);
            Ergaenze_UserAgent(webRequest);
            Ergaenze_Authorization(webRequest);

            var httpWebRequest = webRequest as HttpWebRequest;
            Ergaenze_Accept(httpWebRequest);

            return httpWebRequest;
        }

        private WebRequest Erstelle_WebRequest(Uri uri, HttpMethod method)
        {
            WebRequest request = WebRequest.Create(uri.AbsoluteUri);
            request.Method = method.Method;
            return request;
        }

        private void Ergaenze_Authorization(WebRequest request)
        {
            switch (_settings.Authorization)
            {
                case RequestAuthorization.NetworkCredentials:
                    request.Credentials = _settings.Credentials;
                    break;
                case RequestAuthorization.Keine:
                    break;
                case RequestAuthorization.Basic:
                    string encoded =
                        Convert.ToBase64String(
                            Encoding.UTF8.GetBytes($"{_settings.Credentials.UserName}:{_settings.Credentials.Password}"));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    break;
                default:
                    throw new ArgumentException("HttpService: Die Authorization konnte aus den Settings nicht ermittelt werden.");
            }
        }

        private void Ergaenze_Custom_Headers(WebRequest request)
        {
            if (_settings.ZusaetzlicheHeaders.Any())
            {
                foreach (KeyValuePair<string, string> header in _settings.ZusaetzlicheHeaders)
                {
                    request.Headers[header.Key] = header.Value;
                }
            }
        }

        private void Ergaenze_Proxy(WebRequest request)
        {
            if (!string.IsNullOrEmpty(_settings.ProxyUrl))
            {
                request.Proxy = new WebProxy { BypassProxyOnLocal = true, BypassArrayList = { _settings.ProxyUrl } };
            }
        }

        private void Ergaenze_UserAgent(WebRequest request)
        {
            if (!string.IsNullOrEmpty(_settings.UserAgent))
            {
                ((HttpWebRequest)request).UserAgent = _settings.UserAgent;
            }
        }

        private void Ergaenze_ContentType(WebRequest request, HttpMethod method)
        {
            if (method == HttpMethod.Get)
            {
                request.ContentType = _settings.RequestContentType_Get;
                return;
            }
            else if (method == HttpMethod.Put || method == HttpMethod.Post || method == HttpMethod.Delete)
            {
                request.ContentType = _settings.RequestContentType_PutPostDelete;
                return;
            }

            throw new ArgumentException($"HttpService - HttpMethod {method} nicht erkannt.");
        }

        private void Ergaenze_Accept(HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(_settings.RequestAccept))
            {
                request.Accept = _settings.RequestAccept;
            }
        }
    }
}
