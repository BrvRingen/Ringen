using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstellen.Contracts.Models
{
    public class Mannschaft
    {
        public string TeamId { get; set; }

        public string Vereinsnummer { get; set; }

        public string Kurzname { get; set; }
        
        public string Langname { get; set; }

    }
}
