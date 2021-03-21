using System.Collections.Generic;
using System.Threading.Tasks;
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
                    Task.Run(async () =>
                    {
                        _mannschaftskaempfList = await DependencyInjectionContainer.GetService<MannschaftskaempfeService>().Get_und_Map_Mannschaftskaempfe_Async(SaisonId, LigaId, TableId);
                        OnPropertyChanged(nameof(Children));
                    });
                }

                return _mannschaftskaempfList;
            }
        }
    }
}
