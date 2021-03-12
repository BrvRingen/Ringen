using System;
using System.Net;

namespace Http.Library.Services
{
    public interface IHttpService
    {
        string Get(Uri uri);

        void Post_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen);

        void Put_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen);

        void Delete_Json(Uri uri, string json, Action<HttpStatusCode, string, string> onAbgeschlossen);
    }
}
