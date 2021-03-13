using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class Ergebnisdienst : IErgebnisdienst
    {
        private RdbService _rdbService;

        public Ergebnisdienst(RdbService rdbService)
        {
            _rdbService = rdbService;
        }

        public Ringer GetRinger(string startausweisNr, string saisonId)
        {
            RingerMapper mapper = new RingerMapper();

            JObject response = _rdbService.Get(
                "getSaisonWrestler",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("passCode", startausweisNr),
                });

            //TODO: Response noch unbekannt
            WrestlerApiModel apiModel = response["wrestler"].ToObject<WrestlerApiModel>();
            
            return mapper.Map(apiModel);
        }

        public Einzelkampf GetEinzelkampf(string saisonId, string wettkampfId, int kampfNr)
        {
            EinzelkampfMapper mapper = new EinzelkampfMapper();

            JObject response = _rdbService.Get(
                "getCompetition",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            var kaempfeJArray = response["competition"]["_boutList"].ToArray();
            var kampfJToken = kaempfeJArray.FirstOrDefault(li => li["order"].Equals(kampfNr.ToString()));

            return mapper.Map(kampfJToken);
        }


        public Tuple<Mannschaftskampf, List<Einzelkampf>> GetMannschaftskampf(string saisonId, string wettkampfId)
        {
            MannschaftskampfMapper wettkampfMapper = new MannschaftskampfMapper();
            EinzelkampfMapper einzelkampfMapper = new EinzelkampfMapper();
            
            JObject response = _rdbService.Get(
                "getCompetition",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            CompetitionApiModel apiModel = response["competition"].ToObject<CompetitionApiModel>();
            JToken[] kaempfeJArray = response["competition"]["_boutList"].ToArray();

            Mannschaftskampf mannschaftskampf = wettkampfMapper.Map(apiModel);
            List<Einzelkampf> einzelKaempfe = einzelkampfMapper.Map(kaempfeJArray);

            return new Tuple<Mannschaftskampf, List<Einzelkampf>>(mannschaftskampf, einzelKaempfe);
        }

        public List<Mannschaftskampf> GetMannschaftskaempfe(string saisonId, string ligaId, string tableId)
        {
            MannschaftskampfMapper mapper = new MannschaftskampfMapper();

            JObject response = _rdbService.Get(
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

        public Tuple<Liga, List<Tabellenplatzierung>> GetLigaMitPlatzierung(string saisonId, string ligaId, string tableId)
        {
            LigaMapper ligaMapper = new LigaMapper();
            TabellenplatzierungMapper tabellenplatzierungMapper = new TabellenplatzierungMapper();

            JObject response = _rdbService.Get(
                "getTable",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("ligaId", ligaId),
                    new KeyValuePair<string, string>("rid", tableId),
                });
            LigaApiModel ligaApiModel = response["table"].ToObject<LigaApiModel>();
            IEnumerable<PlaceApiModel> platzierungApiModelListe = response["table"]["_place"].Select(elem => elem.FirstOrDefault().ToObject<PlaceApiModel>());

            return new Tuple<Liga, List<Tabellenplatzierung>>(ligaMapper.Map(ligaApiModel), tabellenplatzierungMapper.Map(platzierungApiModelListe));
        }
    }
}
