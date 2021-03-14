using System;
using System.Collections.Generic;
using Flurl;
using Http.Library.Services;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class RdbService
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private HttpService _httpService;
        private RdbSystemSettings _settings;

        public RdbService(HttpService httpService, RdbSystemSettings settings)
        {
            _httpService = httpService;
            _settings = settings;
        }

        public JObject GetOrganisationsmanager(string operation,
            List<KeyValuePair<string, string>> queryParameter = null)
        {
            string url = _settings.BaseUrl
                .SetQueryParam(_settings.JsonReaderService.Key, _settings.JsonReaderService.Value)
                .SetQueryParam(_settings.TaskOrganisationsmanager.Key, _settings.TaskOrganisationsmanager.Value) //tk = task | jr:cs = Json-Reader Service | OM = Organisationsmanager 
                .SetQueryParam("op", operation); //op ~ operation 

            return Get(queryParameter, url);
        }

        public JObject GetCompetitionSystem(string operation, List<KeyValuePair<string, string>> queryParameter = null)
        {
            string url = _settings.BaseUrl
                .SetQueryParam(_settings.JsonReaderService.Key, _settings.JsonReaderService.Value)
                .SetQueryParam(_settings.TaskCompetitionSystem.Key, _settings.TaskCompetitionSystem.Value) //tk = task | jr:cs = Json-Reader Service | CS = Competition System
                .SetQueryParam("op", operation); //op ~ operation 

            return Get(queryParameter, url);
        }

        private JObject Get(List<KeyValuePair<string, string>> queryParameter, string url)
        {
            if (queryParameter != null)
            {
                foreach (var param in queryParameter)
                {
                    url = url.SetQueryParam(param.Key, param.Value);
                }
            }

            _logger.Debug($"RdbService: GET {url}");
            string jsonString = _httpService.Get(new Uri(url));
            _logger.Debug($"RdbService: Response = {jsonString}");

            JObject parsedJson = JObject.Parse(jsonString);
            return parsedJson;
        }
    }
}
