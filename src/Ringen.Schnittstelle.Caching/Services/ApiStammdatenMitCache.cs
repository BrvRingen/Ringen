using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstelle.Caching.Models;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Services
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

        public async Task<Ringer> GetRingerAsync(string startausweisNr)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetRingerAsync)}_{startausweisNr}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.RingerInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetRingerAsync(startausweisNr); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaft>> GetMannschaftenAsync()
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetMannschaftenAsync)}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftenInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftenAsync(); }, cacheDauerInTagen);
        }
    }
}
