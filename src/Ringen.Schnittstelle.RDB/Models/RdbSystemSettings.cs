using System.Net;

namespace Ringen.Schnittstelle.RDB.Models
{
    public class RdbSystemSettings
    {
        public string BaseUrl { get; }

        public NetworkCredential Credentials { get; }

        public RdbSystemSettings(string baseUrl, NetworkCredential credentials)
        {
            BaseUrl = baseUrl;
            Credentials = credentials;
        }
    }
}
