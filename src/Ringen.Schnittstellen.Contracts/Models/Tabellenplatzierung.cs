namespace Ringen.Schnittstellen.Contracts.Models
{
    public class Tabellenplatzierung
    {
        public int Tabellenplatz { get; set; }

        public string TeamId { get; set; }

        public int AnzahlSiege { get; set; }

        public int AnzahlNiederlagen { get; set; }

        public int AnzahlUnentschieden { get; set; }

        public int GesamtErkaempfteGriffbewertungspunkte { get; set; }

        public int GesamtAbgegebeneGriffbewertungspunkte { get; set; }

        public int PlusPunkte { get; set; }

        public int MinusPunkte { get; set; }
    }
}
