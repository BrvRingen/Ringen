using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstelle.Caching.Models;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Services
{
    internal class ApiSaisonInformationenMitCache : IApiSaisonInformationen
    {
        private IApiSaisonInformationen _api;
        private CacheZeiten _cacheZeiten;

        private readonly ApiCache _apiCache = new ApiCache();

        public ApiSaisonInformationenMitCache(IApiSaisonInformationen api, CacheZeiten cacheZeiten)
        {
            _api = api;
            _cacheZeiten = cacheZeiten;
        }

        public async Task<List<Liga>> GetLigenAsync(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetLigenAsync)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.LigenInTagen);
            
            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetLigenAsync(saisonId); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Saison, List<Leistungsklasse>>> GetSaisonAsync(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetSaisonAsync)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.SaisonInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetSaisonAsync(saisonId); }, cacheDauerInTagen);
        }

        public async Task<List<Saison>> GetSaisonsAsync()
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetSaisonsAsync)}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.SaisonsInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetSaisonsAsync(); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaft>> GetMannschaftenAsync(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetMannschaftenAsync)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftenInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftenAsync(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }

        public async Task<List<EinzelkampfSchema>> GetMannschaftskampfSchemaAsync(string saisonId, string wettkampfId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetMannschaftskampfSchemaAsync)}_{saisonId}_{wettkampfId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskampfSchemaInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftskampfSchemaAsync(saisonId, wettkampfId); }, cacheDauerInTagen);
        }

        public async Task<List<Kampftag>> GetKampftageAsync(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetKampftageAsync)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.KampftageInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetKampftageAsync(saisonId); }, cacheDauerInTagen);
        }
    }
}
