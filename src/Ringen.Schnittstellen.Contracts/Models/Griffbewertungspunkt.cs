using System;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary> 
    /// Bewertungen für Aktionen und Griffe
    /// 1, 2, 4 und 5 Punkte
    /// BoutPoint
    /// </summary>
    public class Griffbewertungspunkt
    {
        public HeimGast? Fuer { get; set; }

        public GriffbewertungsTyp Typ { get; set; }

        public string Value { get; set; }

        public TimeSpan Zeit { get; set; }
    }
}