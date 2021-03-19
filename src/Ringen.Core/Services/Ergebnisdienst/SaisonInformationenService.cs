using System.Collections.Generic;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Services.Ergebnisdienst
{
    public class SaisonInformationenService 
    {
        private List<SaisonViewModel> _seasons;
        private List<LigaViewModel> _ligen;
        private ISaisonInformationen _saisonInformationen;
        private SaisonInformationenViewModelMapper _mapper;

        public SaisonInformationenService(ISaisonInformationen saisonInformationen, SaisonInformationenViewModelMapper mapper)
        {
            _saisonInformationen = saisonInformationen;
            _mapper = mapper;
        }
        
        public async Task<List<SaisonViewModel>> GetSaisonsAsync() 
        {
            if (_seasons == null)
            {
                List<Saison> saisonListe = await _saisonInformationen.GetSaisonsAsync();
                _seasons = _mapper.Map(this, saisonListe);
            }

            return _seasons;
        }


        public async Task<List<LigaViewModel>> GetLigenAsync(string saisonId)
        {
            if (_ligen == null)
            {
                List<Liga> ligenListe = await _saisonInformationen.GetLigenAsync(saisonId);
                _ligen = _mapper.Map(ligenListe);
            }

            return _ligen;
        }


    }
}
