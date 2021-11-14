using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Mapper;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.Services.ErgebnisdienstApi
{
    public class StammdatenService
    {
        private IApiStammdaten _apiStammdaten;

        private RingerViewModelMapper _viewModelMapper;

        public StammdatenService(IApiStammdaten apiStammdaten, RingerViewModelMapper viewModelMapper)
        {
            _apiStammdaten = apiStammdaten;
            _viewModelMapper = viewModelMapper;
        }
        
        public async Task<RingerViewModel> Get_und_Map_Ringer_Async(string startausweisNr)
        {
            Ringer ringer = await _apiStammdaten.Get_Ringer_Async(startausweisNr);
            var ringerViewModel = _viewModelMapper.Map(ringer);

            return ringerViewModel;
        }
    }
}
