using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Services.ErgebnisdienstApi
{
    public class MannschaftskaempfeService
    {

        private IApiMannschaftskaempfe _apiMannschaftskaempfe;
        private MannschaftskaempfeViewModelMapper _mapper;

        public MannschaftskaempfeService(IApiMannschaftskaempfe apiMannschaftskaempfe, MannschaftskaempfeViewModelMapper mapper)
        {
            _apiMannschaftskaempfe = apiMannschaftskaempfe;
            _mapper = mapper;
        }

        public async Task<List<MannschaftskampfViewModel>> Get_und_Map_Mannschaftskaempfe_Async(string saisonId, string ligaId, string tableId)
        {
            List<Mannschaftskampf> mannschaftskaempfListe = await _apiMannschaftskaempfe.GetMannschaftskaempfeAsync(saisonId, ligaId, tableId);
            List<MannschaftskampfViewModel> viewModelListe = _mapper.Map(mannschaftskaempfListe);

            return viewModelListe;
        }

    }
}
