using System;
using System.Net;

namespace Http.Library.Exceptions
{
    public class HttpServiceCallException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Unused;

        public string Response { get; } = string.Empty;

        public HttpServiceCallException(string fehlermeldung) : base(fehlermeldung)
        {

        }

        public HttpServiceCallException(string fehlermeldung, Exception innerException) : base(fehlermeldung, innerException)
        {

        }

        public HttpServiceCallException(string fehlermeldung, Exception innerException, string response, HttpStatusCode statusCode) : base(fehlermeldung, innerException)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public HttpServiceCallException(string fehlermeldung, string response, HttpStatusCode statusCode) : base(fehlermeldung)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
