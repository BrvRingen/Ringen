using System.Collections.Generic;
using System.Net;

namespace Http.Library.Models
{
    public class HttpServiceSettings
    {
        public NetworkCredential Credentials { get; set; }

        public RequestAuthorization Authorization { get; set; } = RequestAuthorization.NetworkCredentials;

        public string RequestContentType_Get { get; set; } = "application/x-www-form-urlencoded";

        public string RequestContentType_PutPostDelete { get; set; } = "application/json";

        public string RequestAccept { get; set; } = string.Empty;

        public string UserAgent { get; set; } = string.Empty;

        public IDictionary<string, string> ZusaetzlicheHeaders { get; set; } = new Dictionary<string, string>();

        public string ProxyUrl { get; set; } = string.Empty;

        public HttpServiceSettings()
        {
            Authorization = RequestAuthorization.Keine;
        }

        public HttpServiceSettings(NetworkCredential credentials)
        {
            Credentials = credentials;
            Authorization = RequestAuthorization.NetworkCredentials;
        }

        public HttpServiceSettings(string benutzername, string passwort) : this(new NetworkCredential(benutzername, passwort))
        {
        }
    }
}
