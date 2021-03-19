using Ringen.Schnittstellen.Contracts.Models.Enums;
using System.Collections.Generic;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class HeimGastKonvertierer : KonvertiererBase<HeimGast>
    {
        protected override Dictionary<string, HeimGast> MappingDictionary { get; } = new Dictionary<string, HeimGast>()
        {
            {"R", HeimGast.Heim },
            {"B", HeimGast.Gast },
        };
    }
}
