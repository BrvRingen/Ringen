using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    internal class LigaViewModelMapper
    {
        public LigaViewModel Map(Liga model)
        {
            var viewModel = new LigaViewModel(model.SaisonId, model.LigaId, model.TabellenId);
            return viewModel;
        }

        public List<LigaViewModel> Map(IEnumerable<Liga> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
