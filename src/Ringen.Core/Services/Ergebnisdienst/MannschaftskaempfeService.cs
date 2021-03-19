using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Services.Ergebnisdienst
{
    public class MannschaftskaempfeService
    {
        private List<MannschaftskampfViewModel> _mannschaftskaempfList;

        private IMannschaftskaempfe _mannschaftskaempfe;
        private MannschaftskaempfeViewModelMapper _mapper;

        public MannschaftskaempfeService(IMannschaftskaempfe mannschaftskaempfe, MannschaftskaempfeViewModelMapper mapper)
        {
            _mannschaftskaempfe = mannschaftskaempfe;
            _mapper = mapper;
        }

        public async Task<List<MannschaftskampfViewModel>> GetMannschaftskaempfeAsync(string saisonId, string ligaId, string tableId)
        {
            if (_mannschaftskaempfList == null)
            {
                List<Mannschaftskampf> mannschaftskaempfListe = await _mannschaftskaempfe.GetMannschaftskaempfeAsync(saisonId, ligaId, tableId);
                _mannschaftskaempfList = _mapper.Map(mannschaftskaempfListe);
            }

            return _mannschaftskaempfList;
        }

    }
}
