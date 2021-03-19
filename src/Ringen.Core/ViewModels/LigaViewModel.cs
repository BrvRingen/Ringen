using System;
using System.Collections.Generic;
using System.Text;
using Nancy.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.Ergebnisdienst;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class LigaViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private MannschaftskaempfeService _service;

        public LigaViewModel(MannschaftskaempfeService service, string saisonId, string ligaId, string tableId)
        {
            _service = service;
            SaisonId = saisonId;
            LigaId = ligaId;
            TableId = tableId;
        }

        public string Value => $"{LigaId} {TableId}".Trim();

        public string SaisonId { get; }

        public string LigaId { get; }

        public string TableId { get; }

        public List<MannschaftskampfViewModel> Children => Async.RunSync(() => _service.GetMannschaftskaempfeAsync(SaisonId, LigaId, TableId));
    }
}
