using System;
using System.Collections.Generic;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary>
    /// Bout
    /// </summary>
    public class Einzelkampf
    {
        public int KampfNr { get; set; }

        public string Gewichtsklasse { get; set; }

        public Stilart Stilart { get; set; }

        public Ringer HeimRinger { get; set; }

        public Ringer GastRinger { get; set; }

        public int HeimMannschaftswertung { get; set; }

        public int GastMannschaftswertung { get; set; }
        
        public List<KeyValuePair<int, string>> RundenErgebnisse { get; set; } = new List<KeyValuePair<int, string>>();

        public Siegart Siegart { get; set; }

        public TimeSpan Kampfdauer { get; set; }

        public List<Griffbewertungspunkt> Wertungspunkte { get; set; }

        public string Kommentar { get; set; }
    }
}