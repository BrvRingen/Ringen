using System;
using System.Net;
using System.Threading.Tasks;
using Http.Library.Models;

namespace Http.Library.Services
{
    public interface IHttpService
    {
        Task<HttpResponse> Get_Async(Uri uri);

        Task<HttpResponse> Post_Json_Async(Uri uri, string json);

        Task<HttpResponse> Put_Json_Async(Uri uri, string json);

        Task<HttpResponse> Delete_Json_Async(Uri uri, string json);
    }
}
