using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstelle.Caching.Models;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Services
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

        public async Task<Einzelkampf> GetEinzelkampfAsync(string saisonId, string wettkampfId, int kampfNr)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetEinzelkampfAsync)}_{saisonId}_{wettkampfId}_{kampfNr}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.EinzelkampfInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetEinzelkampfAsync(saisonId, wettkampfId, kampfNr); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Mannschaftskampf, List<Einzelkampf>>> GetMannschaftskampfAsync(string saisonId, string wettkampfId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetMannschaftskampfAsync)}_{saisonId}_{wettkampfId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskampfInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftskampfAsync(saisonId, wettkampfId); }, cacheDauerInTagen);
        }

        public async Task<List<Mannschaftskampf>> GetMannschaftskaempfeAsync(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetMannschaftskaempfeAsync)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.MannschaftskaempfeInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetMannschaftskaempfeAsync(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }

        public async Task<Tuple<Liga, List<Tabellenplatzierung>>> GetLigaMitPlatzierungAsync(string saisonId, string ligaId, string tableId)
        {
            var cacheKey = $"{this.GetType().Name}_{nameof(GetLigaMitPlatzierungAsync)}_{saisonId}_{ligaId}_{tableId}";
            var cacheDauerInTagen = TimeSpan.FromDays(_cacheZeiten.LigaMitPlatzierungInTagen);

            return await _apiCache.Get_und_Cache_Daten(cacheKey, async () => { return await _api.GetLigaMitPlatzierungAsync(saisonId, ligaId, tableId); }, cacheDauerInTagen);
        }
    }
}
