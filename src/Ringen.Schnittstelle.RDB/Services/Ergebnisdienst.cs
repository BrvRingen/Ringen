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
            var apiModel = GeneriereBoutApiModel(kampfJToken);

            return mapper.Map(apiModel);
        }

        private BoutApiModel GeneriereBoutApiModel(JToken kampfJToken)
        {
            var apiModel = kampfJToken.ToObject<BoutApiModel>();
            var annotationApiModelListe = kampfJToken["annotation"]["1"].Select(li => li.FirstOrDefault().ToObject<AnnotationApiModel>()).ToList();
            apiModel.Annotations = annotationApiModelListe.ToList();

            return apiModel;
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

            List<BoutApiModel> einzelKaempfeApiModelListe = new List<BoutApiModel>();
            var kaempfeJArray = response["competition"]["_boutList"].ToArray();
            foreach (JToken kampfJToken in kaempfeJArray)
            {
                var kampfApiModel = GeneriereBoutApiModel(kampfJToken);
                einzelKaempfeApiModelListe.Add(kampfApiModel);
            }


            Mannschaftskampf mannschaftskampf = wettkampfMapper.Map(apiModel);
            List<Einzelkampf> einzelKaempfe = einzelkampfMapper.Map(einzelKaempfeApiModelListe);

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

        public List<Liga> GetLigen(string saisonId)
        {
            LigaMapper mapper = new LigaMapper();

            JObject response = _rdbService.Get(
                "listLiga",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId)
                });
            IEnumerable<LigaApiModel> apiModelListe = response["ligaList"].Select(elem => elem.FirstOrDefault().ToObject<LigaApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }
        
        public Tuple<Saison, Leistungsklasse> GetSaison(string saisonId)
        {
            SaisonMapper saisonMapper = new SaisonMapper();
            LeistungsklasseMapper leistungsklasseMapper = new LeistungsklasseMapper();

            JObject response = _rdbService.Get("getSaison",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId)
                });

            SaisonApiModel saisonApiModel = response["saison"].ToObject<SaisonApiModel>();
            SystemApiModel systemApiModel = response["saison"]["_system"].ToObject<SystemApiModel>();

            return new Tuple<Saison, Leistungsklasse>(saisonMapper.Map(saisonApiModel), leistungsklasseMapper.Map(systemApiModel));
        }

        public List<Saison> GetSaisons()
        {
            SaisonMapper mapper = new SaisonMapper();

            JObject response = _rdbService.Get("listSaison");
            IEnumerable<SaisonApiModel> apiModelListe = response["saisonList"].Select(elem => elem.FirstOrDefault().ToObject<SaisonApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }

        public List<Mannschaft> GetMannschaften(string saisonId, string ligaId, string tableId)
        {
            MannschaftMapper mapper = new MannschaftMapper();

            JObject response = _rdbService.Get(
                "getTable",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("ligaId", ligaId),
                    new KeyValuePair<string, string>("rid", tableId),
                });

            IEnumerable<TeamApiModel> apiModelListe = response["table"]["_teamNameMap"].Select(elem => elem.FirstOrDefault().ToObject<TeamApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }
    }
}
