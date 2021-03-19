using System;
using System.Collections.Generic;
using System.Linq;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal abstract class KonvertiererBase<T>
    {
        protected abstract Dictionary<string, T> MappingDictionary { get; }

        public string ToApiString(T enumValue)
        {
            KeyValuePair<string, T> elem = MappingDictionary.FirstOrDefault(li => li.Value.Equals(enumValue));

            return elem.Key;
        }

        public T ToEnum(string apiString)
        {
            KeyValuePair<string, T> elem = MappingDictionary.FirstOrDefault(li => li.Key.Equals(apiString, StringComparison.OrdinalIgnoreCase));

            return elem.Value;
        }
    }
}