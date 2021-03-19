using System.Collections.Generic;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class StilartKonvertierer : KonvertiererBase<Stilart>
    {
        protected override Dictionary<string, Stilart> MappingDictionary { get; } = new Dictionary<string, Stilart>()
        {
            {"LL", Stilart.Freistil },
            {"GR", Stilart.GriechischRoemisch },
        };
    }
}
