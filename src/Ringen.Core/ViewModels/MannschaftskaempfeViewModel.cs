using System.Collections.Generic;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class MannschaftskaempfeViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public string Value { get; } = "Mannschaftskämpfe";

        private List<SaisonViewModel> _saisonViewModels;
        public List<SaisonViewModel> Children
        {
            get
            {
                if (_saisonViewModels == null)
                {
                    LadeDaten();
                }
                    
                return _saisonViewModels;
            }
        }

        private async void LadeDaten()
        {
            _saisonViewModels = await DependencyInjectionContainer.GetService<SaisonService>().Get_und_Map_Saisons_Async();
            OnPropertyChanged(nameof(Children));
        }
    }
}
