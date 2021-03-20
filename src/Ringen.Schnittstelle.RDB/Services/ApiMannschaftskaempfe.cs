using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Services
{
    internal class ApiMannschaftskaempfe : IApiMannschaftskaempfe
    {
        private RdbService _rdbService;
        private EinzelkampfMapper _einzelkampfMapper;
        
        public ApiMannschaftskaempfe(RdbService rdbService, EinzelkampfMapper einzelkampfMapper)
        {
            _rdbService = rdbService;
            _einzelkampfMapper = einzelkampfMapper;
        }

        public async Task<Einzelkampf> Get_Einzelkampf_Async(string saisonId, string wettkampfId, int kampfNr)
        {
            JObject response = await _rdbService.Get_CompetitionSystem_Async(
                "getCompetition",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            JToken[] kaempfeJArray = response["competition"]["_boutList"].ToArray();
            if (kaempfeJArray == null || kaempfeJArray.Length <= 0)
            {
                throw new ApiNichtGefundenException($"Es sind keine Kämpfe für Saison {saisonId} und Wettkampf {wettkampfId} ({response["competition"]["homeTeamName"]} vs. {response["competition"]["opponentTeamName"]} am {response["competition"]["boutDate"]}) vorhanden.");
            }

            JToken kampfJToken = kaempfeJArray.FirstOrDefault(li => li["order"].Value<string>().Equals(kampfNr.ToString()));

            return _einzelkampfMapper.Map(kampfJToken);
        }
        
        public async Task<Tuple<Mannschaftskampf, List<Einzelkampf>>> Get_Mannschaftskampf_Async(string saisonId, string wettkampfId)
        {
            MannschaftskampfMapper wettkampfMapper = new MannschaftskampfMapper();
            
            JObject response = await _rdbService.Get_CompetitionSystem_Async(
                "getCompetition",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            CompetitionApiModel apiModel = response["competition"].ToObject<CompetitionApiModel>();
            JToken[] kaempfeJArray = response["competition"]["_boutList"].ToArray();

            Mannschaftskampf mannschaftskampf = wettkampfMapper.Map(apiModel);
            List<Einzelkampf> einzelKaempfe = _einzelkampfMapper.Map(kaempfeJArray);

            return new Tuple<Mannschaftskampf, List<Einzelkampf>>(mannschaftskampf, einzelKaempfe);
        }

        public async Task<List<Mannschaftskampf>> Get_Mannschaftskaempfe_Async(string saisonId, string ligaId, string tableId)
        {
            MannschaftskampfMapper mapper = new MannschaftskampfMapper();

            JObject response = await _rdbService.Get_CompetitionSystem_Async(
                "listCompetition", 
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("ligaId", ligaId),
                    new KeyValuePair<string, string>("rid", tableId),
                });

            IEnumerable<CompetitionApiModel> apiModelListe = response["competitionList"]
                .Select(elem => elem.FirstOrDefault().ToObject<CompetitionApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }

        public async Task<Tuple<Liga, List<Tabellenplatzierung>>> Get_Liga_mit_Tabellenplatzierungen_Async(string saisonId, string ligaId, string tableId)
        {
            LigaMapper ligaMapper = new LigaMapper();
            TabellenplatzierungMapper tabellenplatzierungMapper = new TabellenplatzierungMapper();

            JObject response = await _rdbService.Get_CompetitionSystem_Async(
                "getTable",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("ligaId", ligaId),
                    new KeyValuePair<string, string>("rid", tableId),
                });
            LigaApiModel ligaApiModel = response["table"].ToObject<LigaApiModel>();
            IEnumerable<PlaceApiModel> platzierungApiModelListe = response["table"]["_place"].ToObject<IEnumerable<PlaceApiModel>>();

            return new Tuple<Liga, List<Tabellenplatzierung>>(ligaMapper.Map(ligaApiModel), tabellenplatzierungMapper.Map(platzierungApiModelListe));
        }
    }
}
