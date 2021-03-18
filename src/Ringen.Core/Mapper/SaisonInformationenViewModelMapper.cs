using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Services;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    public class SaisonInformationenViewModelMapper
    {
        public SaisonViewModel Map(SaisonInformationenService service, Saison model)
        {
            var viewModel = new SaisonViewModel(service, model);
            return viewModel;
        }

        public List<SaisonViewModel> Map(SaisonInformationenService service, IEnumerable<Saison> modelListe)
        {
            return modelListe.Select(model => Map(service, model)).ToList();
        }

        public LigaViewModel Map(Liga model)
        {
            var viewModel = new LigaViewModel(model);
            return viewModel;
        }

        public List<LigaViewModel> Map(List<Liga> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
