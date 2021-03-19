using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;

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
                    LadeDaten();
                }

                return _ligen;
            }
        }

        private async void LadeDaten()
        {
            _ligen = await DependencyInjectionContainer.GetService<SaisonService>().Get_und_Map_Ligen_Async(SaisonId);
            OnPropertyChanged(nameof(Children));
        }
    }
}
