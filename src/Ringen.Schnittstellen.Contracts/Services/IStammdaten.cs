using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    public interface IStammdaten
    {
        Task<Ringer> GetRingerAsync(string startausweisNr);

        Task<List<Mannschaft>> GetMannschaftenAsync();
    }
}
