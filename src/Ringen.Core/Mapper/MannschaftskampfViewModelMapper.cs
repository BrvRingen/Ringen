using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    internal class MannschaftskampfViewModelMapper
    {
        public MannschaftskampfViewModel Map(Mannschaftskampf model)
        {
            //TODO: Mapping
            //var viewModel = new MannschaftskampfViewModel(model.ar);
            return null;
        }

        public List<MannschaftskampfViewModel> Map(IEnumerable<Mannschaftskampf> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
