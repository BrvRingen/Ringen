using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.Ergebnisdienst;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class SaisonViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private SaisonService _service;

        public SaisonViewModel(SaisonService service, string saisonId)
        {
            _service = service;
            SaisonId = saisonId;
        }

        public string Value => SaisonId;

        public string SaisonId { get; }

        public List<LigaViewModel> Children => Async.RunSync(() => _service.Get_und_Map_Ligen_Async(SaisonId));

    }
}
