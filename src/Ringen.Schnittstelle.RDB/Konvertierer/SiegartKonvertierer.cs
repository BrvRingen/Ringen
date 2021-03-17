using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    public class SiegartKonvertierer : KonvertiererBase<Siegart>
    {
        protected override Dictionary<string, Siegart> MappingDictionary { get; } = new Dictionary<string, Siegart>()
        {
            {"TÜ", Siegart.TechnischUeberlegen },
            {"SS", Siegart.Schultersieg },
            {"PS", Siegart.Punktsieg },
            {"KL", Siegart.Kampflos },
            {"ÜG", Siegart.Uebergewicht },
        };
    }
}
