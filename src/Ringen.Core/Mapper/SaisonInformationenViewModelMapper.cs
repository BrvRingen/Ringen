using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Services;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.ViewModels;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Mapper
{
    public class SaisonInformationenViewModelMapper
    {
        public SaisonViewModel Map(Saison apiModel)
        {
            var viewModel = new SaisonViewModel(apiModel.SaisonId);
            return viewModel;
        }

        public List<SaisonViewModel> Map(IEnumerable<Saison> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }

        public LigaViewModel Map(Liga model)
        {
            var viewModel = new LigaViewModel( model.SaisonId, model.LigaId, model.TabellenId);
            return viewModel;
        }

        public List<LigaViewModel> Map(List<Liga> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
