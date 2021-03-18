using System;
using System.Collections.Generic;
using System.Linq;
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
        private EinzelkampfMapper _einzelkampfMapper;

        public SaisonInformationen(RdbService rdbService, EinzelkampfMapper einzelkampfMapper)
        {
            _rdbService = rdbService;
            _einzelkampfMapper = einzelkampfMapper;
        }

        public List<Kampftag> GetKampftage(string saisonId)
        {
            KampftagMapper mapper = new KampftagMapper();

            JObject response = _rdbService.GetCompetitionSystem(
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
            JObject response = _rdbService.GetCompetitionSystem(
                "getCompetitionScheme",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId),
                    new KeyValuePair<string, string>("cid", wettkampfId),
                });

            IEnumerable<BoutSchemaApiModel> apiModelListe = response["boutList"].ToObject<IEnumerable<BoutSchemaApiModel>>();

            return apiModelListe.Select(apiModel => _einzelkampfMapper.Map(apiModel)).ToList();
        }

        public List<Liga> GetLigen(string saisonId)
        {
            LigaMapper mapper = new LigaMapper();

            JObject response = _rdbService.GetCompetitionSystem(
                "listLiga",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId)
                });

            List<LigaApiModel> apiModelListe = new List<LigaApiModel>();
            foreach (var liga in response["ligaList"].ToArray())
            {
                foreach (var tabelle in liga.ToArray())
                {
                    var temp = tabelle.Select(elem => elem.FirstOrDefault().ToObject<LigaApiModel>()).ToList();
                    apiModelListe.AddRange(temp);
                }
            }

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }

        public Tuple<Saison, List<Leistungsklasse>> GetSaison(string saisonId)
        {
            SaisonMapper saisonMapper = new SaisonMapper();
            LeistungsklasseMapper leistungsklasseMapper = new LeistungsklasseMapper();

            JObject response = _rdbService.GetCompetitionSystem("getSaison",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("sid", saisonId)
                });

            SaisonApiModel saisonApiModel = response["saison"].ToObject<SaisonApiModel>();
            IEnumerable<SystemApiModel> systemApiModelListe = response["saison"]["_system"].Select(elem => elem.FirstOrDefault().ToObject<SystemApiModel>());

            return new Tuple<Saison, List<Leistungsklasse>>(saisonMapper.Map(saisonApiModel), leistungsklasseMapper.Map(systemApiModelListe));
        }

        public async Task<List<Saison>> GetSaisonsAsync()
        {
            return await Task.Run(() =>
            {
                SaisonMapper mapper = new SaisonMapper();

                JObject response = _rdbService.GetCompetitionSystem("listSaison");
                IEnumerable<SaisonApiModel> apiModelListe = response["saisonList"].Select(elem => elem.FirstOrDefault().ToObject<SaisonApiModel>());

                return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
            });
        }





        public List<Mannschaft> GetMannschaften(string saisonId, string ligaId, string tableId)
        {
            MannschaftMapper mapper = new MannschaftMapper();

            JObject response = _rdbService.GetCompetitionSystem(
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
