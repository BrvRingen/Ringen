using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Services.CS
{
    public class SaisonInformationenService 
    {
        private List<SaisonViewModel> _seasons;
        private List<LigaViewModel> _ligen;
        private List<MannschaftskampfViewModel> _mannschaftskaempfe;
        private ISaisonInformationen _saisonInformationen;
        private SaisonViewModelMapper _mapper;

        public SaisonInformationenService(ISaisonInformationen saisonInformationen, SaisonViewModelMapper mapper)
        {
            _saisonInformationen = saisonInformationen;
            _mapper = mapper;
        }
        
        public async Task<List<SaisonViewModel>> GetSaisonsAsync() 
        {
            if (_seasons == null)
            {
                List<Saison> saisonListe = await _saisonInformationen.GetSaisonsAsync();
                _seasons = _mapper.Map(saisonListe);
            }

            return _seasons;
        }

        public async Task<List<LigaViewModel>> GetLigenAsync(ISaisonInformationen service, string SaisonId)
        {
            if (_ligen == null)
            {
                var ligenListe = _saisonInformationen.GetLigen(SaisonId);
                _ligen = _mapper.Map(ligenListe);
            }

            return _ligen;
        }


        public async Task<List<MannschaftskampfViewModel>> GetMannschaftskaempfeAsync(string SaisonId)
        {
            if (_mannschaftskaempfe == null)
            {
                var mannschaftskampfListe = _saisonInformationen.GetKampftage(SaisonId);
                _mannschaftskaempfe = _mapper.Map(mannschaftskampfListe);
            }

            return _mannschaftskaempfe;
        }





        //
    }
}
