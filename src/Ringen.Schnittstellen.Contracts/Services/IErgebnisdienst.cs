using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    public interface IErgebnisdienst
    {
        /// <summary>
        /// Übermittelt das Ergebnis eines Mannschaftskampf an den Ergebnisdienst z. B. RDB-Schnittstelle des BRVs
        /// </summary>
        /// <param name="mannschaftskampf"></param>
        /// <param name="einzelkaempfe"></param>
        Task UebermittleErgebnisAsync(Mannschaftskampf mannschaftskampf, List<Einzelkampf> einzelkaempfe);
    }
}
