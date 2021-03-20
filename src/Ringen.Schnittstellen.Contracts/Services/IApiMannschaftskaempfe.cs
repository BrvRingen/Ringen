using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiMannschaftskaempfe
    {
        /// <summary>
        /// Ein Kampf innerhalb eines Mannschaftskampfs
        /// z. B. 1ter Kampf von RCA vs. Hof - Landesliga Nord 2019 - 55 kg
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="wettkampfId"></param>
        /// <param name="kampfNr"></param>
        /// <returns></returns>
        Task<Einzelkampf> Get_Einzelkampf_Async(string saisonId, string wettkampfId, int kampfNr);

        /// <summary>
        /// Ein konkreter Mannschaftskampf
        /// z. B. RCA vs. Hof
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="wettkampfId"></param>
        /// <returns></returns>
        Task<Tuple<Mannschaftskampf, List<Einzelkampf>>> Get_Mannschaftskampf_Async(string saisonId, string wettkampfId);

        /// <summary>
        /// Mehrere Mannschaftskämpfe für eine Saison, Liga und Tabelle
        /// z. B. alle Mannschaftskämpfe von Landesliga Nord 2019
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="ligaId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<List<Mannschaftskampf>> Get_Mannschaftskaempfe_Async(string saisonId, string ligaId, string tableId);

        /// <summary>
        /// Platzierungstabelle einer Saison, Liga und Tabelle
        /// z. B. Platzierungen von Landesliag Nord 2019
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="ligaId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<Tuple<Liga, List<Tabellenplatzierung>>> Get_Liga_mit_Tabellenplatzierungen_Async(string saisonId, string ligaId, string tableId);
    }
}
