using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class SaisonInformationen : ISaisonInformationen
    {
        private RdbService _rdbService;

        public SaisonInformationen(RdbService rdbService)
        {
            _rdbService = rdbService;
        }

        public List<Kampftag> GetKampftage(string saisonId)
        {
            KampftagMapper mapper = new KampftagMapper();

            JObject response = _rdbService.Get(
                "listOrgBoutday",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                });

            IEnumerable<BoutdayApiModel> apiModelListe = response["orgBoutdayList"].Select(elem => elem.FirstOrDefault().ToObject<BoutdayApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }

        public List<EinzelkampfSchema> GetMannschaftskampfSchema(string saisonId, string wettkampfId)
        {
            EinzelkampfMapper mapper = new EinzelkampfMapper();

            JObject response = _rdbService.Get(
                "getCompetitionScheme",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            IEnumerable<BoutSchemaApiModel> apiModelListe = response["boutList"].ToObject<IEnumerable<BoutSchemaApiModel>>();

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
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
