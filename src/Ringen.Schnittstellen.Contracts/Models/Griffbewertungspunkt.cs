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
        public HeimGast Fuer { get; set; }

        public GriffbewertungsTyp Typ { get; set; }

        public TimeSpan Zeit { get; set; }

        public int Punktzahl { get; set; }


        public Griffbewertungspunkt()
        {
            
        }

        public Griffbewertungspunkt(HeimGast fuer, GriffbewertungsTyp typ, TimeSpan zeit, int punktzahl=0)
        {
            Fuer = fuer;
            Typ = typ;
            Zeit = zeit;
            Punktzahl = punktzahl;
        }
    }
}