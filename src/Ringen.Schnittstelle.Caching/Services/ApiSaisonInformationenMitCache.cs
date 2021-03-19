using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Services
{
    public class ApiSaisonInformationenMitCache : IApiSaisonInformationen
    {
        private IApiSaisonInformationen _api;
        private readonly ApiCache _apiCache;
        private TimeSpan _classCacheTime;

        public ApiSaisonInformationenMitCache(IApiSaisonInformationen api)
        {
            _api = api;
            _apiCache = new ApiCache();
            _classCacheTime=TimeSpan.FromDays(30);
        }

        public async Task<List<Liga>> GetLigenAsync(string saisonId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetLigenAsync(saisonId); }, _classCacheTime);
        }

        public async Task<Tuple<Saison, List<Leistungsklasse>>> GetSaisonAsync(string saisonId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetSaisonAsync(saisonId); }, _classCacheTime);
        }

        public async Task<List<Saison>> GetSaisonsAsync()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetSaisonsAsync(); }, _classCacheTime);
        }

        public async Task<List<Mannschaft>> GetMannschaftenAsync(string saisonId, string ligaId, string tableId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftenAsync(saisonId, ligaId, tableId); }, _classCacheTime);
        }

        public async Task<List<EinzelkampfSchema>> GetMannschaftskampfSchemaAsync(string saisonId, string wettkampfId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftskampfSchemaAsync(saisonId, wettkampfId); }, _classCacheTime);
        }

        public async Task<List<Kampftag>> GetKampftageAsync(string saisonId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var cacheKey = m.ReflectedType.FullName;

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetKampftageAsync(saisonId); }, _classCacheTime);
        }
    }
}
