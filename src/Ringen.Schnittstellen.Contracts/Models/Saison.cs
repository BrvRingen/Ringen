namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary>
    /// CS
    /// z. B. 2020, 2019, 2018
    /// </summary>
    public class Saison
    {
        public string SaisonId { get; set; }

        public string Bezeichnung { get; set; }

        public string Ligenleiter { get; set; }
    }
}
