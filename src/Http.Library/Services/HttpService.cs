using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Http.Library.Exceptions;
using Http.Library.Factories;
using Http.Library.Models;
using NLog;

namespace Http.Library.Services
{
    public class HttpService : IHttpService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private string _schnittstellenName;
        private HttpWebRequestGenerator _requestGenerator;

        public HttpService(string schnittstellenName, HttpServiceSettings settings)
        {
            _requestGenerator = new HttpWebRequestGenerator(settings);
            _schnittstellenName = schnittstellenName;
        }

        public Task<HttpResponse> Get_Async(Uri uri)
        {
            _logger.Trace($"{_schnittstellenName}: HTTP-Request (GET): {uri.AbsoluteUri}");

            HttpWebRequest request = _requestGenerator.Erstelle_Request(uri, HttpMethod.Get);

            Task<HttpResponse> httpResponse = Send_Request_Async(request);

            return httpResponse;
        }

        public Task<HttpResponse> Put_Json_Async(Uri uri, string json)
        {
            return Erstelle_und_Sende_StandardRequest(HttpMethod.Post, uri, json);
        }

        public Task<HttpResponse> Post_Json_Async(Uri uri, string json)
        {
            return Erstelle_und_Sende_StandardRequest(HttpMethod.Post, uri, json);
        }

        public Task<HttpResponse> Delete_Json_Async(Uri uri, string json)
        {
            return Erstelle_und_Sende_StandardRequest(HttpMethod.Delete, uri, json);
        }
        
        internal Task<HttpResponse> Erstelle_und_Sende_StandardRequest(HttpMethod httpMethod, Uri uri, string json)
        {
            _logger.Trace(
                $"{_schnittstellenName}: HTTP-Request ({httpMethod.ToString().ToUpper()}) an {Uri.UnescapeDataString(uri.AbsolutePath)} mit Daten: {Uri.UnescapeDataString(json)}");

            HttpWebRequest request = _requestGenerator.Erstelle_Request(uri, httpMethod);
            Ergaenze_HttpContent(json, request);

            Task<HttpResponse> httpResponse = Send_Request_Async(request);
            return httpResponse;
        }

        internal void Ergaenze_HttpContent(string datenString, HttpWebRequest request)
        {
            if (string.IsNullOrEmpty(datenString))
            {
                throw new HttpServiceCallException($"{_schnittstellenName}: Angefragte URL: {request.Address.AbsoluteUri}, HTTP-Content ist leer.");
            }

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(datenString);
            }
        }

        internal async Task<HttpResponse> Send_Request_Async(HttpWebRequest request)
        {
            try
            {
                var response =
                    await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
                string httpResult = Lese_Response(response);

                return new HttpResponse(((HttpWebResponse) response).StatusCode, response.ContentType, httpResult);
            }
            catch (WebException ex)
            {
                string fehlermeldung = $"{_schnittstellenName}: Angefragte URL: {request.Address.AbsoluteUri}, Rückmeldung: ";
                if (ex.Response != null)
                {
                    string responseString = Lese_Response(ex.Response);
                    var responseStatusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    fehlermeldung += responseString;

                    if (responseStatusCode != HttpStatusCode.NotFound)
                    {
                        _logger.Fatal(ex, fehlermeldung);
                    }

                    throw new HttpServiceCallException(fehlermeldung, ex, responseString, responseStatusCode);
                }

                throw new HttpServiceCallException(fehlermeldung, ex);
            }
        }

        internal string Lese_Response(WebResponse response)
        {
            if (response == null)
            {
                throw new HttpServiceCallException($"{_schnittstellenName}: Übergebener WebResponse ist leer.");
            }

            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
