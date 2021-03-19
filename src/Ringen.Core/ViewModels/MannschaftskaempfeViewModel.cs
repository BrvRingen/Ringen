using System.Collections.Generic;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.Ergebnisdienst;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class MannschaftskaempfeViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private SaisonInformationenService _service;

        public MannschaftskaempfeViewModel(SaisonInformationenService service)
        {
            _service = service;
        }

        public string Value { get; } = "Mannschaftskämpfe";
        
        public List<SaisonViewModel> Children => Async.RunSync( () => _service.GetSaisonsAsync());
    }
}
