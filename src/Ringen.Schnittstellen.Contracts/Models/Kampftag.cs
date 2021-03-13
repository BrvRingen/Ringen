using System;

namespace Ringen.Schnittstellen.Contracts.Models
{
    public class Kampftag
    {
        public string SaisonId { get; set; }

        public int KampftagNummer { get; set; }

        public DateTime Datum { get; set; }
    }
}
