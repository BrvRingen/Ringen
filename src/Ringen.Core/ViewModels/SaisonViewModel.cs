using System.Collections.Generic;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.DependencyInjection;

namespace Ringen.Core.ViewModels
{
    public class SaisonViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public SaisonViewModel(string saisonId)
        {
            SaisonId = saisonId;
        }

        public string Value => SaisonId;

        public string SaisonId { get; }


        private List<LigaViewModel> _ligen;
        public List<LigaViewModel> Children
        {
            get
            {
                if (_ligen == null)
                {
                    Lade_ApiDaten_Async();
                }

                return _ligen;
            }
        }

        private async void Lade_ApiDaten_Async()
        {
            _ligen = await DependencyInjectionContainer.GetService<SaisonService>().Get_und_Map_Ligen_Async(SaisonId);
            OnPropertyChanged(nameof(Children));
        }
    }
}
