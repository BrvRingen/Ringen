using System;
using System.Security.AccessControl;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary>
    /// Competition
    /// </summary>
    public class Mannschaftskampf
    {
        public string SaisonId { get; set; }

        public string WettkampfId { get; set; }

        public string HeimMannschaft { get; set; }

        public string GastMannschaft { get; set; }

        public int HeimPunkte { get; set; } = 0;

        public int GastPunkte { get; set; } = 0;

        public DateTime Kampfdatum { get; set; }

        public TimeSpan GeplanterKampfbeginn { get; set; }

        public TimeSpan EchterKampfbeginn { get; set; }

        public TimeSpan EchtesKampfende { get; set; }

        public int AnzahlZuschauer { get; set; } = 0;

        public string Wettkampfstaette { get; set; }

        public string Schiedsrichter_Vorname { get; set; }

        public string Schiedsrichter_Nachname { get; set; }

        public HeimGast Sieger { get; set; }

        public bool IstErgebnisGeprueft { get; set; } = false;

        public string Kommentar { get; set; }
    }
}