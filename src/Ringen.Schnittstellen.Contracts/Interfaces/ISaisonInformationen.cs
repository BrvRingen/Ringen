using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    public interface ISaisonInformationen
    {
        List<Liga> GetLigen(string saisonId);

        Tuple<Saison, Leistungsklasse> GetSaison(string saisonId);

        List<Saison> GetSaisons();

        List<Mannschaft> GetMannschaften(string saisonId, string ligaId, string tableId);

        List<EinzelkampfSchema> GetMannschaftskampfSchema(string saisonId, string wettkampfId);

        List<Kampftag> GetKampftage(string saisonId);
    }
}
