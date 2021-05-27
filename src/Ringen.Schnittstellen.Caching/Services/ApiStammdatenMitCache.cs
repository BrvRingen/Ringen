using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Caching.Models;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstellen.Caching.Services
{
    internal class ApiStammdatenMitCache : IApiStammdaten
    {
        private IApiStammdaten _api;
        private CacheZeiten _cacheZeiten;
        private readonly ApiCache _apiCache = new ApiCache();

        public ApiStammdatenMitCache(IApiStammdaten api, CacheZeiten cacheZeiten)
        {
            _api = api;
            _cacheZeiten = cacheZeiten;
        }

        public async Task<Ringer> Get_Ringer_Async(string startausweisNr)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Ringer_Async)}_{startausweisNr}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.RingerInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Ringer_Async(startausweisNr); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaft>> Get_Mannschaften_Async()
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Mannschaften_Async)}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftenInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Mannschaften_Async(); }, cacheDauerInTagen);
        }
    }
}
