using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Services.Ergebnisdienst
{
    public class MannschaftskaempfeService
    {
        private List<MannschaftskampfViewModel> _mannschaftskaempfList;

        private IApiMannschaftskaempfe _apiMannschaftskaempfe;
        private MannschaftskaempfeViewModelMapper _mapper;

        public MannschaftskaempfeService(IApiMannschaftskaempfe apiMannschaftskaempfe, MannschaftskaempfeViewModelMapper mapper)
        {
            _apiMannschaftskaempfe = apiMannschaftskaempfe;
            _mapper = mapper;
        }

        public async Task<List<MannschaftskampfViewModel>> GetMannschaftskaempfeAsync(string saisonId, string ligaId, string tableId)
        {
            if (_mannschaftskaempfList == null)
            {
                List<Mannschaftskampf> mannschaftskaempfListe = await _apiMannschaftskaempfe.GetMannschaftskaempfeAsync(saisonId, ligaId, tableId);
                _mannschaftskaempfList = _mapper.Map(mannschaftskaempfListe);
            }

            return _mannschaftskaempfList;
        }

    }
}
