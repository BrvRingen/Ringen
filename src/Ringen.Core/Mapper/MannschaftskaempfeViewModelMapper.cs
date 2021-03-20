using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nelibur.ObjectMapper;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.Mapper
{
    public class MannschaftskaempfeViewModelMapper
    {
        public MannschaftskaempfeViewModelMapper()
        {
            //TinyMapper.Bind<Mannschaftskampf, MannschaftskampfViewModel>();
            //TinyMapper.Bind<Einzelkampf, EinzelkampfViewModel>();
        }

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
                KampfNr = 0,
                Settings = null,
                Order = 0,
                WeightClass = null,
                WrestleStyle = BoutSettings.WrestleStyles.LL,
                HomeWrestlerId = 0,
                HomeWrestlerWeight = 0,
                OpponentWrestlerWeight = 0,
                HomeWrestlerLicId = 0,
                HomeWrestlerName = model.HeimRinger?.Nachname,
                HomeWrestlerGivenname = model.HeimRinger?.Vorname,
                HomeWrestlerStatus = null,
                HomeWrestlerPoints = 0,
                HomeWrestlerFlags = 0,
                OpponentWrestlerId = 0,
                OpponentWrestlerLicId = 0,
                OpponentWrestlerName = model.GastRinger?.Nachname,
                OpponentWrestlerGivenname = model.GastRinger?.Vorname,
                OpponentWrestlerStatus = null,
                OpponentWrestlerPoints = 0,
                OpponentWrestlerFlags = 0,
                Result = BoutSettings.Results.TÜ,
                Round1 = null,
                Points = null,
                Children = null
            };

            return viewModel;
        }
        
        public List<EinzelkampfViewModel> Map(IEnumerable<Einzelkampf> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
        
        public MannschaftskampfViewModel Map(Mannschaftskampf model)
        {
            var viewModel = new MannschaftskampfViewModel(model.SaisonId, model.WettkampfId)
            {
                HeimMannschaft = model.HeimMannschaft,
                GastMannschaft = model.GastMannschaft,
                HeimPunkte = model.HeimPunkte,
                GastPunkte = model.GastPunkte,
                Kampfdatum = model.Kampfdatum,
                GeplanterKampfbeginn = model.GeplanterKampfbeginn,
                EchterKampfbeginn = model.EchterKampfbeginn,
                EchtesKampfende = model.EchtesKampfende,
                AnzahlZuschauer = model.AnzahlZuschauer,
                Wettkampfstaette = model.Wettkampfstaette,
                Schiedsrichter_Vorname = model.Schiedsrichter_Vorname,
                Schiedsrichter_Nachname = model.Schiedsrichter_Nachname,
                Sieger = model.Sieger,
                IstErgebnisGeprueft = model.IstErgebnisGeprueft,
                Kommentar = model.Kommentar,
            };

            return viewModel;
        }

        public List<MannschaftskampfViewModel> Map(IEnumerable<Mannschaftskampf> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }

        public Tuple<Mannschaftskampf, List<Einzelkampf>> MapErgebnis(MannschaftskampfViewModel viewModel)
        {
            var ergebnis = new Tuple<Mannschaftskampf, List<Einzelkampf>>(new Mannschaftskampf(), new List<Einzelkampf>());

            //TODO Map Daten für Senden an BRV

            return ergebnis;
        }
    }
}
