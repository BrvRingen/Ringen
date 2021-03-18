using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    public interface IMannschaftskaempfe
    {
        /// <summary>
        /// Ein Kampf innerhalb eines Mannschaftskampfs
        /// z. B. 1ter Kampf von RCA vs. Hof - Landesliga Nord 2019 - 55 kg
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="wettkampfId"></param>
        /// <param name="kampfNr"></param>
        /// <returns></returns>
        Task<Einzelkampf> GetEinzelkampfAsync(string saisonId, string wettkampfId, int kampfNr);

        /// <summary>
        /// Ein konkreter Mannschaftskampf
        /// z. B. RCA vs. Hof
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="wettkampfId"></param>
        /// <returns></returns>
        Task<Tuple<Mannschaftskampf, List<Einzelkampf>>> GetMannschaftskampfAsync(string saisonId, string wettkampfId);

        /// <summary>
        /// Mehrere Mannschaftskämpfe für eine Saison, Liga und Tabelle
        /// z. B. alle Mannschaftskämpfe von Landesliga Nord 2019
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="ligaId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<List<Mannschaftskampf>> GetMannschaftskaempfeAsync(string saisonId, string ligaId, string tableId);

        /// <summary>
        /// Platzierungstabelle einer Saison, Liga und Tabelle
        /// z. B. Platzierungen von Landesliag Nord 2019
        /// </summary>
        /// <param name="saisonId"></param>
        /// <param name="ligaId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<Tuple<Liga, List<Tabellenplatzierung>>> GetLigaMitPlatzierungAsync(string saisonId, string ligaId, string tableId);
    }
}
