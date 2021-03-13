using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    public class EinzelkampfSchema
    {
        public int KampfNr { get; set; }

        public string Gewichtsklasse { get; set; }

        public Stilart Stilart { get; set; }
    }
}
