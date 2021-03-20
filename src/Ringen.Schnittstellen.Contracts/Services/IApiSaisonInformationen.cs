using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiSaisonInformationen
    {
        Task<List<Liga>> Get_Ligen_Async(string saisonId);

        Task<Tuple<Saison, List<Leistungsklasse>>> Get_Saison_Async(string saisonId);

        Task<List<Saison>> Get_Saisons_Async();

        Task<List<Mannschaft>> Get_Mannschaften_Async(string saisonId, string ligaId, string tableId);

        Task<List<EinzelkampfSchema>> Get_MannschaftskampfSchema_Async(string saisonId, string wettkampfId);

        Task<List<Kampftag>> Get_Kampftage_Async(string saisonId);
    }
}
