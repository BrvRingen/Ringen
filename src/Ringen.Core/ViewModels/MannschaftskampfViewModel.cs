using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.UI;
using Ringen.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.ViewModels
{
    public class MannschaftskampfViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        public MannschaftskampfViewModel(Mannschaftskampf model) //bool istDoppelRunde, string ligaId, string tableId, TimeSpan scaleTime, string saisonId, string competitionId, string homeTeamName, string opponentTeamName, string homePoints, string opponentPoints, DateTime boutDateDateTime, string location, string editorName, string referee)
        {
            Model = model;
            //IstDoppelRunde = istDoppelRunde;
            //LigaId = ligaId;
            //TableId = tableId;
            //ScaleTime = scaleTime;
            //SaisonId = saisonId;
            //CompetitionId = competitionId;
            //HomeTeamName = homeTeamName;
            //OpponentTeamName = opponentTeamName;
            //HomePoints = homePoints;
            //OpponentPoints = opponentPoints;
            //BoutDateDateTime = boutDateDateTime;
            //Location = location;
            //EditorName = editorName;
            //Referee = referee;

            Audience = model.AnzahlZuschauer;
            EditorComment = model.Kommentar;
            HomeTeamName = model.HeimMannschaft;
            OpponentTeamName = model.GastMannschaft;
        }

        public Mannschaftskampf Model { get; }

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

        /// <summary>
        /// z. B. RCA Bayreuth - ASV Hof II
        /// </summary>
        public string Value => $"{Model.HeimMannschaft} - {Model.GastMannschaft}";

        public bool IstDoppelRunde { get; }

        public string LigaId { get; }

        public string TableId { get; }

        /// <summary>
        /// z. B. 19:00:00
        /// </summary>
        public TimeSpan ScaleTime { get; }

        /// <summary>
        /// z. B. 2019
        /// </summary>
        public string SaisonId { get; }

        /// <summary>
        /// z. B. 092100g
        /// </summary>
        public string CompetitionId { get; }

        /// <summary>
        /// z. B. RCA Bayreuth
        /// </summary>
        public string HomeTeamName { get; }

        /// <summary>
        /// z. B. ASV Hof II
        /// </summary>
        public string OpponentTeamName { get; }

        /// <summary>
        /// z. B. 12
        /// </summary>
        public string HomePoints { get; }

        /// <summary>
        /// z. B. 44
        /// </summary>
        public string OpponentPoints { get; }

        /// <summary>
        /// Kampfdatum
        /// z. B. 2019-12-07
        /// </summary>
        public string BoutDate
        {
            get => BoutDateDateTime.ToShortDateString();
        }

        public DateTime BoutDateDateTime { get; }

        /// <summary>
        /// Zuschaueranzahl
        /// z. B. 16
        /// </summary>
        private int _audience;
        public int Audience
        {
            get => _audience;
            set => Set(ref _audience, value);
        }

        /// <summary>
        /// Kampfstätte
        /// z. B. Altstadtschule Bayreuth, Fantasierstr. 11, 95445 Bayreuth
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Benutzername von BRV-Webseite, welcher das Protokoll eingereicht hat.
        /// bayreuth
        /// </summary>
        public string EditorName { get; }

        private string _editorComment;

        /// <summary>
        /// Protokoll-Kommentar (von Schiedsrichter)
        /// z. B. Bayreuth 57 u. 130 g+f unbesetzt<br>Hof 66 g+f unbesetzt<br>keine Pause<br>
        /// </summary>
        public string EditorComment
        {
            get
            {
                return _editorComment.ToString().Replace("<br>", Environment.NewLine);
            }
            set
            {
                Set(ref _editorComment, value.Replace(Environment.NewLine, "<br>"));
            }
        }

        /// <summary>
        /// Schiedsrichter
        /// z. B. Jürgen Fischer
        /// </summary>
        public string Referee { get; }

        private List<Bout> bouts;

        public List<Bout> Children
        {
            get
            {
                return null;

                if (bouts == null)
                {
                    Async.RunSync(async () =>
                    {
                        var mannschaftskampf = DependencyInjectionContainer.GetService<IApiMannschaftskaempfe>().GetMannschaftskampfAsync(this.SaisonId, this.CompetitionId).Result;
                        foreach (var einzelkampf in mannschaftskampf.Item2)
                        {
                            //TODO EinzelkampfViewModelMapper
                            //bouts.Add(new Bout(einzelkampf));
                        }
                    });

                    var BoutsForCompetition = new List<(string WeightClass, string Style)>();
                    var kampfSchema = DependencyInjectionContainer.GetService<IApiSaisonInformationen>().GetMannschaftskampfSchemaAsync(this.SaisonId, this.CompetitionId);

                    if (bouts.Count() == 0)
                    {
                        var i = 1;
                        //TODO Map Schema
                        foreach (var BoutForCompetition in BoutsForCompetition)
                        {
                            bouts.Add(new Bout(i++, BoutForCompetition.WeightClass, BoutForCompetition.Style, this));
                        }
                    }

                }

                return bouts;
            }
            set { bouts = value; }
        }

        public async Task SendAsync()
        {
            throw new NotImplementedException();
            var ergebnisdienst = DependencyInjectionContainer.GetService<IApiErgebnisdienst>();

            //TODO: Ergebnis Objekt erstellen
            var ergebnis = new Tuple<Mannschaftskampf, List<Einzelkampf>>(new Mannschaftskampf(), new List<Einzelkampf>());
            //ergebnisdienst.Uebermittle_Ergebnis(this.SaisonId, this.CompetitionId, ergebnis);
        }
    }
}
