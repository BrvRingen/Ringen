using System;
using System.Collections.Generic;
using Flurl;
using Http.Library.Services;
using Newtonsoft.Json.Linq;
using NLog.Fluent;
using Ringen.Schnittstelle.RDB.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class RdbService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HttpService _httpService;
        private RdbSystemSettings _settings;

        public RdbService(HttpService httpService, RdbSystemSettings settings)
        {
            _httpService = httpService;
            _settings = settings;
        }
        
        public JObject Get(string operation, List<KeyValuePair<string, string>> queryParameter = null)
        {
            string url = _settings.BaseUrl
                .SetQueryParam("sv", "json") //Joomla "view" parameter mit "com_rdb". Bei test System ist das der Service (sv) Parameter.  
                .SetQueryParam("tk", "jr:cs") //tk ~ task
                .SetQueryParam("op", operation); //op ~ operation 

            if (queryParameter != null)
            {
                foreach (var param in queryParameter)
                {
                    url = url.SetQueryParam(param.Key, param.Value);
                }
            }

            Logger.Debug($"RdbService: GET {url}");

            string jsonString = _httpService.Get(new Uri(url));

            Logger.Debug($"RdbService: Response = {jsonString}");
            JObject parsedJson = JObject.Parse(jsonString);

            return parsedJson;
        }
    }
}
