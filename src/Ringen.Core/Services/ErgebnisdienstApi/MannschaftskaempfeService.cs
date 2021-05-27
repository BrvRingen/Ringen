using System;
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
        private IApiErgebnisdienst _apiErgebnisdienst;
        private IApiMannschaftskaempfe _apiMannschaftskaempfe;
        private MannschaftskaempfeViewModelMapper _mapper;
        private EinzelkampfViewModelMapper _einzelkampfMapper;

        public MannschaftskaempfeService(IApiErgebnisdienst apiErgebnisdienst, IApiMannschaftskaempfe apiMannschaftskaempfe, MannschaftskaempfeViewModelMapper mapper, EinzelkampfViewModelMapper einzelkampfMapper)
        {
            _apiErgebnisdienst = apiErgebnisdienst;
            _apiMannschaftskaempfe = apiMannschaftskaempfe;
            _mapper = mapper;
            _einzelkampfMapper = einzelkampfMapper;
        }

        public async Task<List<EinzelkampfViewModel>> Get_und_Map_Einzelkaempfe_Async(string saisonId, string wettkampfId)
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> mannschaftskampf = await _apiMannschaftskaempfe.Get_Mannschaftskampf_Async(saisonId, wettkampfId);
            List<EinzelkampfViewModel> viewModelListe = _einzelkampfMapper.Map(mannschaftskampf);

            return viewModelListe;
        }

        public async Task<List<MannschaftskampfViewModel>> Get_und_Map_Mannschaftskaempfe_Async(string saisonId, string ligaId, string tableId)
        {
            List<Mannschaftskampf> mannschaftskaempfListe = await _apiMannschaftskaempfe.Get_Mannschaftskaempfe_Async(saisonId, ligaId, tableId);
            List<MannschaftskampfViewModel> viewModelListe = _mapper.Map(mannschaftskaempfListe);

            return viewModelListe;
        }

        public async Task Map_und_Sende_Ergebnis_Async(MannschaftskampfViewModel viewModel)
        {
            var ergebnis = _mapper.MapErgebnis(viewModel);
            await _apiErgebnisdienst.Uebermittle_Ergebnis_Async(ergebnis.Item1, ergebnis.Item2);
        }

    }
}
