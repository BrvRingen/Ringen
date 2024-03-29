﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.Mapper
{
    public class EinzelkampfViewModelMapper
    {
        private RingerViewModelMapper _ringerMapper = new RingerViewModelMapper();

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
                Gewichtsklasse = model.Gewichtsklasse,
                Stilart = MapWrestleStyle(model.Stilart),

                HeimRinger = _ringerMapper.Map(model.HeimRinger),
                HeimMannschaftswertung = model.GastMannschaftswertung,

                GastRinger = _ringerMapper.Map(model.GastRinger),
                GastMannschaftswertung = model.GastMannschaftswertung,

                Wertungspunkte = new ObservableCollection<Griffbewertungspunkt>(model.Wertungspunkte),

                Siegart = MapSiegart(model.Siegart),
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
