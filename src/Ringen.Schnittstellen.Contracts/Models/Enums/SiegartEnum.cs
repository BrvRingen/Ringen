using System.ComponentModel;

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
