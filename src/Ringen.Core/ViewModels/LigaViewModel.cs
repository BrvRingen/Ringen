using System.Collections.Generic;
using System.Text;
using Nancy.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class LigaViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private MannschaftskaempfeService _service;

        public LigaViewModel(Liga model)
        {
            _service = DependencyInjectionContainer.GetService<MannschaftskaempfeService>();
            Model = model;
        }

        public string Value => $"{Model.LigaId} {Model.TabellenId}".Trim();

        public Liga Model { get; }

        public List<MannschaftskampfViewModel> Children => Async.RunSync(() => _service.GetMannschaftskaempfeAsync(Model.SaisonId, Model.LigaId, Model.TabellenId));
    }
}
