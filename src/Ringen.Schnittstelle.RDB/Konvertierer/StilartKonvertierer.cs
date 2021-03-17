using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class StilartKonvertierer
    {
        private Dictionary<string, Stilart> _dictionary = new Dictionary<string, Stilart>()
        {
            {"LL", Stilart.Freistil },
            {"GR", Stilart.GriechischRoemisch },
        };

        public string ToStilartString(Stilart stilart)
        {
            KeyValuePair<string, Stilart> elem = _dictionary.FirstOrDefault(li => li.Value.Equals(stilart));

            return elem.Key;
        }

        public Stilart ToStilartEnum(string apiModelResult)
        {
            KeyValuePair<string, Stilart> elem = _dictionary.FirstOrDefault(li => li.Key.Equals(apiModelResult, StringComparison.OrdinalIgnoreCase));

            return elem.Value;
        }
    }
}
