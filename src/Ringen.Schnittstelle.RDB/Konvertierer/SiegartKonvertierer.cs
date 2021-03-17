using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class SiegartKonvertierer
    {
        private Dictionary<string, Siegart> _dictionary = new Dictionary<string, Siegart>()
        {
            {"TÜ", Siegart.TechnischUeberlegen },
            {"SS", Siegart.Schultersieg },
            {"PS", Siegart.Punktsieg },
            {"KL", Siegart.Kampflos },
            {"ÜG", Siegart.Uebergewicht },
        };

        public string ToSiegartString(Siegart siegart)
        {
            KeyValuePair<string, Siegart> elem = _dictionary.FirstOrDefault(li => li.Value.Equals(siegart));

            return elem.Key;
        }

        public Siegart ToSiegartEnum(string apiModelResult)
        {
            KeyValuePair<string, Siegart> elem = _dictionary.FirstOrDefault(li => li.Key.Equals(apiModelResult, StringComparison.OrdinalIgnoreCase));

            return elem.Value;
        }
    }
}
