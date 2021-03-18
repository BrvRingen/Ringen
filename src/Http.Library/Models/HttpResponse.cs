using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http.Library.Models
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; }

        public string ContentType { get; }

        public string Result { get; }

        public HttpResponse(HttpStatusCode statusCode, string contentType, string result)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            Result = result;
        }
    }
}
