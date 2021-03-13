namespace Ringen.Schnittstellen.Contracts.Models
{
    public class Ringer
    {
        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Status { get; set; }

        public string Startausweisnummer { get; set; }

        public string Lizenznummer { get; set; }

        public Ringer()
        {
            
        }

        public Ringer(string vorname, string nachname, string status, string startausweisnummer, string lizenznummer)
        {
            Vorname = vorname;
            Nachname = nachname;
            Status = status;
            Startausweisnummer = startausweisnummer;
            Lizenznummer = lizenznummer;
        }
    }
}
