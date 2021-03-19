using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Services;
using Ringen.Core.Services.Ergebnisdienst;
using Ringen.Core.ViewModels;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    public class SaisonInformationenViewModelMapper
    {
        public SaisonViewModel Map(SaisonService service, string saisonId)
        {
            var viewModel = new SaisonViewModel(service, saisonId);
            return viewModel;
        }

        public List<SaisonViewModel> Map(SaisonService service, IEnumerable<Saison> modelListe)
        {
            return modelListe.Select(model => Map(service, model.SaisonId)).ToList();
        }

        public LigaViewModel Map(Liga model)
        {
            var mannschaftskaempfeService = DependencyInjectionContainer.GetService<MannschaftskaempfeService>();
            var viewModel = new LigaViewModel(mannschaftskaempfeService, model.SaisonId, model.LigaId, model.TabellenId);
            return viewModel;
        }

        public List<LigaViewModel> Map(List<Liga> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
