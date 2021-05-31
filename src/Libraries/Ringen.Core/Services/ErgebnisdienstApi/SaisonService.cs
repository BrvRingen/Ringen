using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Services.ErgebnisdienstApi
{
    public class SaisonService 
    {
        private IApiSaisonInformationen _apiSaisonInformationen;

        private SaisonInformationenViewModelMapper _viewModelMapper;

        public SaisonService(IApiSaisonInformationen apiSaisonInformationen, SaisonInformationenViewModelMapper viewModelMapper)
        {
            _apiSaisonInformationen = apiSaisonInformationen;
            _viewModelMapper = viewModelMapper;
        }
        
        public async Task<List<SaisonViewModel>> Get_und_Map_Saisons_Async() 
        {
            List<Saison> saisonListe = await _apiSaisonInformationen.Get_Saisons_Async();
            var saisonViewModels = _viewModelMapper.Map( saisonListe);

            return saisonViewModels;
        }


        public async Task<List<LigaViewModel>> Get_und_Map_Ligen_Async(string saisonId)
        {
            List<Liga> ligenListe = await _apiSaisonInformationen.Get_Ligen_Async(saisonId);
            var ligen = _viewModelMapper.Map(ligenListe);
            

            return ligen;
        }


    }
}
