using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstellen.Contracts.Models
{
    /// <summary>
    /// _system
    /// z. B. Männer, Schüler
    /// </summary>
    public class Leistungsklasse
    {
        public string SystemId { get; set; }

        public string Bezeichnung { get; set; }
    }
}
