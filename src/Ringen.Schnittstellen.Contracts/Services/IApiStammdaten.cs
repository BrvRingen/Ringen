using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiStammdaten
    {
        Task<Ringer> GetRingerAsync(string startausweisNr);

        Task<List<Mannschaft>> GetMannschaftenAsync();
    }
}
