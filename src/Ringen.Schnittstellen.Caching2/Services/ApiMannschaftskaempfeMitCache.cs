using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Caching.Models;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstellen.Caching.Services
{
    internal class ApiMannschaftskaempfeMitCache : IApiMannschaftskaempfe
    {
        private IApiMannschaftskaempfe _api;
        private CacheZeiten _cacheZeiten;

        private readonly ApiCache _apiCache = new ApiCache();

        public ApiMannschaftskaempfeMitCache(IApiMannschaftskaempfe api, CacheZeiten cacheZeiten)
        {
            _api = api;
            _cacheZeiten = cacheZeiten;
        }

        public async Task<Einzelkampf> Get_Einzelkampf_Async(string saisonId, string wettkampfId, int kampfNr)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Einzelkampf_Async)}_{saisonId}_{wettkampfId}_{kampfNr}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.EinzelkampfInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Einzelkampf_Async(saisonId, wettkampfId, kampfNr); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Mannschaftskampf, List<Einzelkampf>>> Get_Mannschaftskampf_Async(string saisonId, string wettkampfId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Mannschaftskampf_Async)}_{saisonId}_{wettkampfId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskampfInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Mannschaftskampf_Async(saisonId, wettkampfId); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaftskampf>> Get_Mannschaftskaempfe_Async(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Mannschaftskaempfe_Async)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskaempfeInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Mannschaftskaempfe_Async(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Liga, List<Tabellenplatzierung>>> Get_Liga_mit_Tabellenplatzierungen_Async(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(Get_Liga_mit_Tabellenplatzierungen_Async)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.LigaMitPlatzierungInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.Get_Liga_mit_Tabellenplatzierungen_Async(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }
    }
}
