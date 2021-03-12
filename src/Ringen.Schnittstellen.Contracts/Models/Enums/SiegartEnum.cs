using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstellen.Contracts.Models.Enums
{
    public enum Siegart
    {
        [Description("TÜ")]
        TechnischUeberlegen,

        [Description("SS")]
        Schultersieg,

        [Description("PS")]
        Punktsieg,

        [Description("KL")]
        Kampflos, 

        [Description("ÜG")]
        Uebergewicht
    }
}
