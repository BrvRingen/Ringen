using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
