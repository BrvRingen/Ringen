using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiErgebnisdienst
    {
        /// <summary>
        /// Übermittelt das Ergebnis eines Mannschaftskampf an den Ergebnisdienst z. B. RDB-Schnittstelle des BRVs
        /// </summary>
        /// <param name="mannschaftskampf"></param>
        /// <param name="einzelkaempfe"></param>
        Task Uebermittle_Ergebnis_Async(Mannschaftskampf mannschaftskampf, List<Einzelkampf> einzelkaempfe);
    }
}
