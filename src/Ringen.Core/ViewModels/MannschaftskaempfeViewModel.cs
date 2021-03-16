using System.Collections.Generic;
using Ringen.Core.Mapper;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class MannschaftskaempfeViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public string Value { get; } = "Mannschaftskämpfe";

        private List<SaisonViewModel> _seasons;

        public List<SaisonViewModel> Children
        {
            get 
            {
                if (_seasons == null)
                {
                    Async.RunSync(async () =>
                    {
                        List<Saison> saisonListe = DependencyInjectionContainer.GetService<ISaisonInformationen>().GetSaisons();
                        _seasons = new SaisonViewModelMapper().Map(saisonListe);
                    });
                }
                    
                return _seasons;
            }
        }
    }
}
