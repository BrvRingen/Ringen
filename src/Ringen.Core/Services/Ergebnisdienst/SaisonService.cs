using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Services.Ergebnisdienst
{
    public class SaisonService 
    {
        private IApiSaisonInformationen _apiApiSaisonInformationen;

        private List<SaisonViewModel> _saisonViewModels;
        private List<LigaViewModel> _ligen;
        
        private SaisonInformationenViewModelMapper _viewModelMapper;

        public SaisonService(IApiSaisonInformationen apiApiSaisonInformationen, SaisonInformationenViewModelMapper viewModelMapper)
        {
            _apiApiSaisonInformationen = apiApiSaisonInformationen;
            _viewModelMapper = viewModelMapper;
        }
        
        public async Task<List<SaisonViewModel>> Get_und_Map_Saisons_Async() 
        {
            if (_saisonViewModels == null)
            {
                List<Saison> saisonListe = await _apiApiSaisonInformationen.GetSaisonsAsync();
                _saisonViewModels = _viewModelMapper.Map(this, saisonListe);
            }

            return _saisonViewModels;
        }


        public async Task<List<LigaViewModel>> Get_und_Map_Ligen_Async(string saisonId)
        {
            if (_ligen == null)
            {
                List<Liga> ligenListe = await _apiApiSaisonInformationen.GetLigenAsync(saisonId);
                _ligen = _viewModelMapper.Map(ligenListe);
            }

            return _ligen;
        }


    }
}
