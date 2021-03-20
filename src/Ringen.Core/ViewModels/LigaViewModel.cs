using System.Collections.Generic;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.DependencyInjection;

namespace Ringen.Core.ViewModels
{
    public class LigaViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public LigaViewModel(string saisonId, string ligaId, string tableId)
        {
            SaisonId = saisonId;
            LigaId = ligaId;
            TableId = tableId;
        }

        public string Value => $"{LigaId} {TableId}".Trim();

        public string SaisonId { get; }

        public string LigaId { get; }

        public string TableId { get; }


        private List<MannschaftskampfViewModel> _mannschaftskaempfList;
        public List<MannschaftskampfViewModel> Children
        {
            get
            {
                if (_mannschaftskaempfList == null)
                {
                    Lade_ApiDaten_Async();
                }

                return _mannschaftskaempfList;
            }
        }

        private async void Lade_ApiDaten_Async()
        {
            _mannschaftskaempfList = await DependencyInjectionContainer.GetService<MannschaftskaempfeService>().Get_und_Map_Mannschaftskaempfe_Async(SaisonId, LigaId, TableId);
            OnPropertyChanged(nameof(Children));
        }
    }
}
