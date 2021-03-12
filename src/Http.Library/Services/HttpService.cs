using System;
using System.IO;
using System.Net;
using System.Net.Http;
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

        public string Get(Uri uri)
        {
            _logger.Trace($"{_schnittstellenName}: HTTP-Request (GET): {uri.AbsoluteUri}");

            HttpWebRequest request = _requestGenerator.Erstelle_Request(uri, HttpMethod.Get);
            string httpResult = string.Empty;
            Send_Request(request, onAbgeschlossen: (statusCode, contentType, content) =>
            {
                httpResult = content;
            }
            );

            return httpResult;
        }

        public void Put_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen)
        {
            Erstelle_und_Sende_StandardRequest(HttpMethod.Post, uri, json, onAbgeschlossen);
        }

        public void Post_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen)
        {
            Erstelle_und_Sende_StandardRequest(HttpMethod.Post, uri, json, onAbgeschlossen);
        }

        public void Delete_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen)
        {
            Erstelle_und_Sende_StandardRequest(HttpMethod.Delete, uri, json, onAbgeschlossen);
        }

        internal void Erstelle_und_Sende_StandardRequest(HttpMethod httpMethod, Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen)
        {
            _logger.Trace(
                $"{_schnittstellenName}: HTTP-Request ({httpMethod.ToString().ToUpper()}) an {Uri.UnescapeDataString(uri.AbsolutePath)} mit Daten: {Uri.UnescapeDataString(json)}");

            HttpWebRequest request = _requestGenerator.Erstelle_Request(uri, httpMethod);
            Ergaenze_HttpContent(json, request);
            Send_Request(request, onAbgeschlossen);
        }

        internal void Ergaenze_HttpContent(byte[] daten, HttpWebRequest request)
        {
            if (daten == null || daten.Length <= 0)
            {
                throw new HttpServiceCallException($"{_schnittstellenName}: Angefragte URL: {request.Address.AbsoluteUri}, HTTP-Content ist leer.");
            }

            request.ContentLength = daten.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(daten, 0, daten.Length);
            }
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

        internal void Send_Request(HttpWebRequest request, Action<HttpStatusCode, string, string> onAbgeschlossen)
        {
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode statusCode = response.StatusCode;
                string contentType = response.ContentType;

                string httpResult = Lese_Response(response);

                onAbgeschlossen(statusCode, contentType, httpResult);
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
            finally
            {
                response?.Dispose();
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
