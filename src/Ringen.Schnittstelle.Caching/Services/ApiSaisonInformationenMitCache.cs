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

        public async Task<List<Liga>> Get_Ligen_Async(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Ligen_Async)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.LigenInTagen);
            
            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Ligen_Async(saisonId); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Saison, List<Leistungsklasse>>> Get_Saison_Async(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Saison_Async)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.SaisonInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Saison_Async(saisonId); }, cacheDauerInTagen);
        }

        public async Task<List<Saison>> Get_Saisons_Async()
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Saisons_Async)}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.SaisonsInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Saisons_Async(); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaft>> Get_Mannschaften_Async(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Mannschaften_Async)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftenInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Mannschaften_Async(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }

        public async Task<List<EinzelkampfSchema>> Get_MannschaftskampfSchema_Async(string saisonId, string wettkampfId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_MannschaftskampfSchema_Async)}_{saisonId}_{wettkampfId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskampfSchemaInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_MannschaftskampfSchema_Async(saisonId, wettkampfId); }, cacheDauerInTagen);
        }

        public async Task<List<Kampftag>> Get_Kampftage_Async(string saisonId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Kampftage_Async)}_{saisonId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.KampftageInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Kampftage_Async(saisonId); }, cacheDauerInTagen);
        }
    }
}
