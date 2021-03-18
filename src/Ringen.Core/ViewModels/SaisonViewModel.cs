using System.Collections.Generic;
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
    public class SaisonViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private SaisonInformationenService _service;

        public SaisonViewModel(SaisonInformationenService service, Saison model)
        {
            _service = service;
            Model = model;
        }

        public string Value => Model.SaisonId;

        public Saison Model { get; }
        

        public List<LigaViewModel> Children => Async.RunSync(() => _service.GetLigenAsync(Model.SaisonId));

    }
}
