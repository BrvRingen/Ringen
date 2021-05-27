using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.DependencyInjection;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;

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
                    Task.Run(async () =>
                    {
                        _ligen = await DependencyInjectionContainer.GetService<SaisonService>().Get_und_Map_Ligen_Async(SaisonId);
                        OnPropertyChanged(nameof(Children));
                    });
                }

                return _ligen;
            }
        }
    }
}
