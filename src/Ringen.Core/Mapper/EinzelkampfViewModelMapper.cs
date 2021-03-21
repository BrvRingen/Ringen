using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.Mapper
{
    public class EinzelkampfViewModelMapper
    {

        public List<EinzelkampfViewModel> Map(Tuple<Mannschaftskampf, List<Einzelkampf>> model)
        {
            var viewModelListe = Map(model.Item2);

            return viewModelListe;
        }

        public EinzelkampfViewModel Map(Einzelkampf model)
        {
            //var viewModel = TinyMapper.Map<EinzelkampfViewModel>(model);

            //TODO Einzelkampf Mapping machen
            var viewModel = new EinzelkampfViewModel
            {
                KampfNr = model.KampfNr,
                Settings = new BoutSettings(MapWrestleStyle(model.Stilart)),
                //Order = 0,
                WeightClass = model.Gewichtsklasse,
                StilartViewModel = MapWrestleStyle(model.Stilart),
                
                HomeWrestlerId = model.HeimRinger?.Startausweisnummer,
                HomeWrestlerLicId = model.HeimRinger?.Lizenznummer,
                HomeWrestlerName = model.HeimRinger?.Nachname,
                HomeWrestlerGivenname = model.HeimRinger?.Vorname,
                HomeWrestlerStatus = model.HeimRinger?.Status,
                HomeWrestlerPoints = model.HeimMannschaftswertung,
                
                OpponentWrestlerId = model.GastRinger?.Startausweisnummer,
                OpponentWrestlerLicId = model.GastRinger?.Lizenznummer,
                OpponentWrestlerName = model.GastRinger?.Nachname,
                OpponentWrestlerGivenname = model.GastRinger?.Vorname,
                OpponentWrestlerStatus = model.GastRinger?.Status,
                OpponentWrestlerPoints = model.GastMannschaftswertung,

                SiegartViewModel = MapSiegart(model.Siegart),
                //Round1 = null,
                //Points = null,
            };

            return viewModel;
        }

        private SiegartViewModelEnum MapSiegart(Siegart modelSiegart)
        {
            switch (modelSiegart)
            {
                case Siegart.TechnischUeberlegen:
                    return SiegartViewModelEnum.TÜ;
                case Siegart.Schultersieg:
                    return SiegartViewModelEnum.SS;
                case Siegart.Punktsieg:
                    return SiegartViewModelEnum.PS;
                case Siegart.Kampflos:
                    return SiegartViewModelEnum.KL;
                case Siegart.Uebergewicht:
                    return SiegartViewModelEnum.ÜG;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelSiegart), modelSiegart, null);
            }
        }

        private StilartViewModel MapWrestleStyle(Stilart modelStilart)
        {
            switch (modelStilart)
            {
                case Stilart.Freistil:
                    return StilartViewModel.LL;
                case Stilart.GriechischRoemisch:
                    return StilartViewModel.GR;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelStilart), modelStilart, null);
            }
        }

        public List<EinzelkampfViewModel> Map(IEnumerable<Einzelkampf> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}
