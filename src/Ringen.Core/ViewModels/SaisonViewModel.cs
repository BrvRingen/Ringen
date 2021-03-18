using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
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
                    Async.RunSync(async () =>
                    {
                        List<Liga> ligenListe = DependencyInjectionContainer.GetService<ISaisonInformationen>().GetLigenAsync(this.SaisonId).Result;
                        _ligen = new LigaViewModelMapper().Map(ligenListe);
                    });
                }

                return _ligen;
            }
        }
    }
}
