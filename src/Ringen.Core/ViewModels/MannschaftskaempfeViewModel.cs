using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.DependencyInjection;

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
                    Task.Run(async () =>
                    {
                        _saisonViewModels = await DependencyInjectionContainer.GetService<SaisonService>().Get_und_Map_Saisons_Async();
                        OnPropertyChanged(nameof(Children));
                    });
                }
                    
                return _saisonViewModels;
            }
        }
    }
}
