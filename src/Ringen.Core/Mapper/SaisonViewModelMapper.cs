using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    internal class SaisonViewModelMapper
    {
        public SaisonViewModel Map(Saison model)
        {
            var viewModel = new SaisonViewModel(model.SaisonId);
            return viewModel;
        }

        public List<SaisonViewModel> Map(IEnumerable<Saison> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
