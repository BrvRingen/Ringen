using System;
using System.Collections.Generic;
using System.Linq;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    public class MannschaftskaempfeViewModelMapper
    {
        public MannschaftskaempfeViewModelMapper()
        {
            //TinyMapper.Bind<Mannschaftskampf, MannschaftskampfViewModel>();
            //TinyMapper.Bind<Einzelkampf, EinzelkampfViewModel>();
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
