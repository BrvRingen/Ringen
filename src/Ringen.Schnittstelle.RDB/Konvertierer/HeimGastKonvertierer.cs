using Ringen.Schnittstellen.Contracts.Models.Enums;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    public class HeimGastKonvertierer : KonvertiererBase<HeimGast>
    {
        protected override Dictionary<string, HeimGast> MappingDictionary { get; } = new Dictionary<string, HeimGast>()
        {
            {"R", HeimGast.Heim },
            {"B", HeimGast.Gast },
        };
    }
}
