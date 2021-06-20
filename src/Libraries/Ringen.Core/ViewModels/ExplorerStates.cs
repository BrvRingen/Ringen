using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.ViewModels
{
    public class ExplorerStates
    {
        public MannschaftskampfViewModel Mannschaftskampf { get; set; } = null;

        public LigaViewModel Liga { get; set; } = null;

        public SaisonViewModel Saison { get; set; } = null;

        public EinzelkampfViewModel Einzelkampf { get; set; } = null;

    }
}
