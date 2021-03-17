using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    public class StilartKonvertierer : KonvertiererBase<Stilart>
    {
        protected override Dictionary<string, Stilart> MappingDictionary { get; } = new Dictionary<string, Stilart>()
        {
            {"LL", Stilart.Freistil },
            {"GR", Stilart.GriechischRoemisch },
        };
    }
}
