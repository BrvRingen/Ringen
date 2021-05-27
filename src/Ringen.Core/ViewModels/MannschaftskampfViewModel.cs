using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ringen.Core.DependencyInjection;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.ViewModels
{
    public class MannschaftskampfViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        /// <summary>
        /// z. B. RCA Bayreuth - ASV Hof II
        /// </summary>
        public string Value => $"{HeimMannschaft} - {GastMannschaft}";

        public MannschaftskampfViewModel(string saisonId, string wettkampfId)
        {
            SaisonId = saisonId;
            WettkampfId = wettkampfId;
        }

        /// <summary>
        /// z. B. 2019
        /// </summary>
        public string SaisonId { get;  }
        
        /// <summary>
        /// z. B. 092100g
        /// </summary>
        public string WettkampfId { get; }
        
        /// <summary>
        /// z. B. RCA Bayreuth
        /// </summary>
        public string HeimMannschaft { get; internal set; }

        /// <summary>
        /// z. B. ASV Hof II
        /// </summary>
        public string GastMannschaft { get; internal set; }

        /// <summary>
        /// z. B. 12
        /// </summary>
        public int HeimPunkte { get; internal set; }

        /// <summary>
        /// z. B. 44
        /// </summary>
        public int GastPunkte { get; internal set; }

        public DateTime Kampfdatum { get; internal set; }
        
        /// <summary>
        /// z. B. 19:00:00
        /// </summary>
        public TimeSpan GeplanterKampfbeginn { get; internal set; }

        public TimeSpan EchterKampfbeginn { get; internal set; }

        public TimeSpan EchtesKampfende { get; internal set; }

        /// <summary>
        /// Zuschaueranzahl
        /// z. B. 16
        /// </summary>
        private int _anzahlZuschauer;
        public int AnzahlZuschauer
        {
            get => _anzahlZuschauer;
            set => Set(ref _anzahlZuschauer, value);
        }

        /// <summary>
        /// Kampfstätte
        /// z. B. Altstadtschule Bayreuth, Fantasierstr. 11, 95445 Bayreuth
        /// </summary>
        public string Wettkampfstaette { get; internal set; }
        
        /// <summary>
        /// Schiedsrichter
        /// z. B. Jürgen Fischer
        /// </summary>
        public string Schiedsrichter_Vorname { get; internal set; }

        public string Schiedsrichter_Nachname { get; internal set; }

        public HeimGast Sieger { get; internal set; }

        public bool IstErgebnisGeprueft { get; internal set; } = false;


        private string _kommentar;

        /// <summary>
        /// Protokoll-Kommentar (von Schiedsrichter)
        /// z. B. Bayreuth 57 u. 130 g+f unbesetzt<br>Hof 66 g+f unbesetzt<br>keine Pause<br>
        /// </summary>
        public string Kommentar
        {
            get
            {
                return _kommentar.ToString().Replace("<br>", Environment.NewLine);
            }
            set
            {
                Set(ref _kommentar, value.Replace(Environment.NewLine, "<br>"));
            }
        }




        //TODO: ggf. löschen
        public int[] GetKampffolge()
        {
            List<int> temp = new List<int>();
            foreach (var bout in Children)
            {
                temp.Add(bout.Order);
            }

            List<int> result = new List<int>();
            bool first = true;
            while (temp.Any())
            {
                if (first)
                {
                    var elem = temp.First();
                    result.Add(elem);
                    temp.Remove(elem);
                    first = false;
                }
                else
                {
                    var elem = temp.Last();
                    result.Add(elem);
                    temp.Remove(elem);
                    first = true;
                }
            }

            return result.ToArray();
        }


        public string LigaId { get; internal set; } //TODO: ggf. aus abhängigkeiten entfernen

        public string TableId { get; internal set; } //TODO: ggf. aus abhängigkeiten entfernen


        private List<EinzelkampfViewModel> _einzelkaempfe;

        public List<EinzelkampfViewModel> Children
        {
            get
            {
                if (_einzelkaempfe == null)
                {
                    Task.Run(async () =>
                    {
                        _einzelkaempfe = await DependencyInjectionContainer.GetService<MannschaftskaempfeService>().Get_und_Map_Einzelkaempfe_Async(this.SaisonId, this.WettkampfId);
                        OnPropertyChanged(nameof(Children));
                    });
                }

                return _einzelkaempfe;
            }
            set { _einzelkaempfe = value; }
        }


        public async Task Sende_Ergebnis_Async()
        {
            await DependencyInjectionContainer.GetService<MannschaftskaempfeService>().Map_und_Sende_Ergebnis_Async(this);
        }
    }
}
