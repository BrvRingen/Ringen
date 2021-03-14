using System;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstellen.Contracts.Models
{
    public class Ringer
    {
        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Status { get; set; }

        public string Startausweisnummer { get; set; }

        public string Lizenznummer { get; set; }

        public Geschlecht Geschlecht { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public string Vereinsnummer { get; set; }
    }
}
