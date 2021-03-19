using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Http.Library.Models;
using Http.Library.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    internal class RdbService
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private IHttpService _httpService;
        private RdbSystemSettings _settings;

        public RdbService(IHttpService httpService, RdbSystemSettings settings)
        {
            _httpService = httpService;
            _settings = settings;
        }

        public async Task<JObject> Sende_Ergebnis_Async(CompetitionPostApiModel apiModel)
        {
            string jsonString = JsonConvert.SerializeObject(apiModel);

            //TODO: Url angeben, sobald bekannt
            string url = _settings.BaseUrl;
                //.SetQueryParam(_settings.JsonReaderService.Key, _settings.JsonReaderService.Value)
                //.SetQueryParam(_settings.TaskOrganisationsmanager.Key, _settings.TaskOrganisationsmanager.Value) //tk = task | jr:cs = Json-Reader Service | OM = Organisationsmanager 
                //.SetQueryParam("op", operation); //op ~ operation 

            _logger.Debug($"RdbService: GET {url}");
            HttpResponse httpResponse = await _httpService.Post_Json_Async(new Uri(url), jsonString);
            _logger.Debug($"RdbService: Response = {httpResponse.Result}");

            JObject parsedJson = JObject.Parse(httpResponse.Result);
            return parsedJson;
        }

        public async Task<JObject> Get_Organisationsmanager_Async(string operation,
            List<KeyValuePair<string, string>> queryParameter = null)
        {
            string url = _settings.BaseUrl
                .SetQueryParam(_settings.JsonReaderService.Key, _settings.JsonReaderService.Value)
                .SetQueryParam(_settings.TaskOrganisationsmanager.Key, _settings.TaskOrganisationsmanager.Value) //tk = task | jr:cs = Json-Reader Service | OM = Organisationsmanager 
                .SetQueryParam("op", operation); //op ~ operation 

            return await Get_Async(queryParameter, url);
        }

        public async Task<JObject> Get_CompetitionSystem_Async(string operation, List<KeyValuePair<string, string>> queryParameter = null)
        {
            string url = _settings.BaseUrl
                .SetQueryParam(_settings.JsonReaderService.Key, _settings.JsonReaderService.Value)
                .SetQueryParam(_settings.TaskCompetitionSystem.Key, _settings.TaskCompetitionSystem.Value) //tk = task | jr:cs = Json-Reader Service | CS = Competition System
                .SetQueryParam("op", operation); //op ~ operation 

            return await Get_Async(queryParameter, url);
        }

        private async Task<JObject> Get_Async(List<KeyValuePair<string, string>> queryParameter, string url)
        {
            if (queryParameter != null)
            {
                foreach (var param in queryParameter)
                {
                    url = url.SetQueryParam(param.Key, param.Value);
                }
            }

            _logger.Debug($"RdbService: GET {url}");
            HttpResponse httpResponse = await _httpService.Get_Async(new Uri(url));
            _logger.Debug($"RdbService: Response = {httpResponse.Result}");

            JObject parsedJson = JObject.Parse(httpResponse.Result);
            return parsedJson;
        }
    }
}
