using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Services
{
    public class ApiSaisonInformationenMitCache : IApiSaisonInformationen
    {
        private IApiSaisonInformationen _api;

        public ApiSaisonInformationenMitCache(IApiSaisonInformationen api)
        {
            _api = api;
        }

        public Task<List<Liga>> GetLigenAsync(string saisonId)
        {
            return _api.GetLigenAsync(saisonId);
        }

        public Task<Tuple<Saison, List<Leistungsklasse>>> GetSaisonAsync(string saisonId)
        {
            return _api.GetSaisonAsync(saisonId);
        }

        public Task<List<Saison>> GetSaisonsAsync()
        {
            return _api.GetSaisonsAsync();
        }

        public Task<List<Mannschaft>> GetMannschaftenAsync(string saisonId, string ligaId, string tableId)
        {
            return _api.GetMannschaftenAsync(saisonId, ligaId, tableId);
        }

        public Task<List<EinzelkampfSchema>> GetMannschaftskampfSchemaAsync(string saisonId, string wettkampfId)
        {
            return _api.GetMannschaftskampfSchemaAsync(saisonId, wettkampfId);
        }

        public Task<List<Kampftag>> GetKampftageAsync(string saisonId)
        {
            return _api.GetKampftageAsync(saisonId);
        }
    }
}
