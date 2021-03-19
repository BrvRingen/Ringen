using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiSaisonInformationen
    {
        Task<List<Liga>> GetLigenAsync(string saisonId);

        Task<Tuple<Saison, List<Leistungsklasse>>> GetSaisonAsync(string saisonId);

        Task<List<Saison>> GetSaisonsAsync();

        Task<List<Mannschaft>> GetMannschaftenAsync(string saisonId, string ligaId, string tableId);

        Task<List<EinzelkampfSchema>> GetMannschaftskampfSchemaAsync(string saisonId, string wettkampfId);

        Task<List<Kampftag>> GetKampftageAsync(string saisonId);
    }
}
