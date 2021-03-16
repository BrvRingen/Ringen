using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary>
    /// Table
    /// </summary>
    public class Liga
    {
        public string SaisonId { get; set; }

        public string LigaId { get; set; }

        public string TabellenId { get; set; }

        public string Bezeichnung { get; set; }

        public Austragungsmodus Austragungsmodus { get; set; }
    }
}