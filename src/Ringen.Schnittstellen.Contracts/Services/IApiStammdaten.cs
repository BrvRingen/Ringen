using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Services
{
    public interface IApiStammdaten
    {
        Task<Ringer> Get_Ringer_Async(string startausweisNr);

        Task<List<Mannschaft>> Get_Mannschaften_Async();
    }
}
