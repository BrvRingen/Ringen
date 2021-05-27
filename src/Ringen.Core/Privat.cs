using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core
{
    public static class Privat
    {
        private static HttpClient client;
        private static string tmpUsername;
        private static string tmpPassword;

        public static HttpClient Client(string Username = "", string Password = "")
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                if (tmpUsername != Username || tmpPassword != Password)
                {
                    client = null;
                    tmpUsername = Username;
                    tmpPassword = Password;
                }
            }

            if (client == null)
            {
                Username = "test";
                Password = "test";

                client = new HttpClient();
                client.BaseAddress = new Uri(Properties.Settings.Default.PrivateRestServer);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", Username, Password))));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return client;
        }

        public static async Task<PrivateInfos> GetPrivateInfos()
        {
            var Response = await Client().GetAsync($"/Api/v1/cs/");
            if (Response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<PrivateInfos>(Response.Content.ReadAsStringAsync().Result);
            else
                return new PrivateInfos();

        }
    }

    public class PrivateInfos
    {
        public int audience { get; set; }
    }
}
