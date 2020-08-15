using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Shared.Models
{
    public class CompetitionInfos
    {
        /// <summary>
        /// Verbandskampf | Freunschaftskampf
        /// </summary>
        public string Kampfart { get; set; } = "Verbandskampf";

        /// <summary>
        /// Vorkampf | Rückkampf
        /// </summary>
        public string VorKampfRueckKampf { get; set; } = string.Empty;

        public string Protokollfuehrer { get; set; } = string.Empty;

        public string ErgebnislistenSchreiber { get; set; } = string.Empty;

        public string MannschaftsfuehrerHeim { get; set; } = string.Empty;

        public string MannschaftsfuehrerGast { get; set; } = string.Empty;

        public List<string> Ordner { get; set; } = new List<string>();
    }
}
