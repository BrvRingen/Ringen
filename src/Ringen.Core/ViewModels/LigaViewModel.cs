using System.Collections.Generic;
using System.Text;
using Nancy.Helpers;
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


        private List<MannschaftskampfViewModel> _mannschaftskaempfe;

        public List<MannschaftskampfViewModel> Children
        {
            get
            {
                if (_mannschaftskaempfe == null)
                {
                    Async.RunSync(async () =>
                    {
                        List<Mannschaftskampf> mannschaftskaempfe = DependencyInjectionContainer.GetService<IMannschaftskaempfe>().GetMannschaftskaempfe(this.SaisonId, this.LigaId, this.TableId);
                        _mannschaftskaempfe = new MannschaftskampfViewModelMapper().Map(mannschaftskaempfe);
                    });
                }

                return _mannschaftskaempfe;
            }
        }

    }
}
