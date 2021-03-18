using System.Collections.Generic;
using System.Text;
using Nancy.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.Services.CS;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class KampftagViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private SaisonInformationenService _service;

        public KampftagViewModel(SaisonInformationenService service, string saisonId, string ligaId, string tablenId)
        {
            _service = service;
            SaisonId = saisonId;
            LigaId = ligaId;
            TablenId = tablenId;
        }

        public KampftagViewModel(SaisonInformationenService service, Kampftag kampftag)
        {
            _service = service;
            LigaId = "test";
            TablenId = "test";
        }

        public string Value => $"{LigaId} {TablenId}".Trim();

        public string SaisonId { get; }

        public string LigaId { get; }

        public string TablenId { get; }



        //public List<MannschaftskampfViewModel> Children => Async.RunSync(() => _service.GetMannschaftskaempfeAsync(this.SaisonId, this.LigaId, this.TablenId));



    }
}
