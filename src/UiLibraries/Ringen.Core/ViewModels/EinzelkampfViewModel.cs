﻿using System.Collections.ObjectModel;
using Ringen.Core.CS;
using Ringen.Core.DependencyInjection;
using Ringen.Core.UI;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class EinzelkampfViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public ExplorerStates ExplorerStates { get; internal set; }

        public EinzelkampfViewModel()
        {
            ExplorerStates explorerStates = DependencyInjectionContainer.GetService<ExplorerStates>();
            ExplorerStates = explorerStates;

            wertungspunkte.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            {
                base.OnPropertyChanged();
            };
        }

        public int KampfNr { get; internal set; }

        public string Value
        {
            get
            {
                return $"{HeimRinger.Vorname} {HeimRinger.Nachname} - {GastRinger.Vorname} {GastRinger.Nachname}"; ;
            }
        }

        private BoutSettings settings;

        public BoutSettings Settings
        {
            get
            {
                if (settings == null) settings = new BoutSettings(Stilart);
                return settings;
            }
            set { settings = value; }
        }

        public int Order { get; internal set; }
        public string Gewichtsklasse { get; internal set; }
        public StilartViewModel Stilart { get; internal set; }
        
        public RingerViewModel HeimRinger { get; internal set; }
        public double HeimRingerGewicht { get; internal set; }

        public int HeimMannschaftswertung { get; internal set; }


        public bool IsNoOpponentWrestler()
        {
            return GastRinger == null;
        }

        public bool IsNoHomeWrestler()
        {
            return HeimRinger == null;
        }


        public RingerViewModel GastRinger { get; internal set; }
        public double GastRingerGewicht { get; internal set; }

        public int GastMannschaftswertung { get; internal set; }


        public SiegartViewModelEnum Siegart { get; internal set; }

        public string Round1 { get; internal set; }

        private ObservableCollection<Griffbewertungspunkt> wertungspunkte = new ObservableCollection<Griffbewertungspunkt>();

        public ObservableCollection<Griffbewertungspunkt> Wertungspunkte
        {
            get
            {
                return wertungspunkte;
            }
            set {
                wertungspunkte = value;
                base.OnPropertyChanged();
                wertungspunkte.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
                {
                    base.OnPropertyChanged();
                };
            }
        }
    }
}
