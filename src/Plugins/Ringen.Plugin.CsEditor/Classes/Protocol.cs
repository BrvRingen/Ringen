using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ringen.Core.Messaging;
using System.Threading.Tasks;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Reporting;
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor
{
    public class Protocol
    {
        public static async Task OnCreateProtocolAsync(Core.CS.Competition competition, CompetitionInfos zusatzInfos = null) //TODO: ZusatzInfos von Oberfläche übergeben
        {
            //TODO: Aktuell nur zu Testzwecken, später von Oberfläche übergeben
            zusatzInfos = new CompetitionInfos()
            {
                ErgebnislistenSchreiber = "Tahar Alemann",
                Kampfart = "Verbandskampf",
                MannschaftsfuehrerHeim = "Halil Aygün",
                MannschaftsfuehrerGast = "Maximilian Fleischer",
                Ordner = new List<string>() { "Jürgen Pfaffe", "Christoph Zapf" },
                Protokollfuehrer = "Anna Badewitz",
                VorKampfRueckKampf = "Rückkampf"
            };

            //TODO Löschen, sobald wirklich Punkte hinterlegt sind
            Random rnd = new Random();
            int cnt = 0;
            foreach (Bout bout in competition.Children)
            {
                for (int i = 0; i < rnd.Next(1, 10); i++)
                {
                    var ringer = (rnd.Next(1, 3) == 1 ? Core.CS.BoutPoint.Wrestler.Home : Core.CS.BoutPoint.Wrestler.Opponent);

                    var zeit = new DateTime(
                        competition.BoutDateDateTime.Year,
                        competition.BoutDateDateTime.Month,
                        competition.BoutDateDateTime.Day,
                        competition.ScaleTime.Hours,
                        competition.ScaleTime.Minutes + cnt + i + 4,
                        rnd.Next(1, 59)
                    );

                    bout.Points.Add(new Core.CS.BoutPoint(rnd.Next(1, 5).ToString(), null, ringer, zeit));
                }

                cnt++;
            }

            var myTask = Task.Run(() =>
            {
                string filename = $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}.pdf";

                LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Erstelle Protokoll für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Bitte warten..."));

                IReport bericht = new ReportFarbigPdf();
                bericht.Export(filename, competition, zusatzInfos);

                LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Protokoll erfolgreich erstellt für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Öffne nun PDF-Datei."));
                Process.Start(filename);//Öffne PDF

                return;
            });
            await myTask;

            return;
        }

        public static async Task OnCreateBoutResultListAsync(Core.CS.Competition competition)
        {
            var myTask = Task.Run(() =>
            {
                string filename = $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}_Ergebnisliste.pdf";

                LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Erstelle Ergebnisliste (Punktzettel) für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Bitte warten..."));

                IReport bericht = new ReportErgebnislisteKampfrichtertischPdf();
                bericht.Export(filename, competition, null);

                LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Ergebnisliste (Punktzettel) erfolgreich erstellt für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Öffne nun PDF-Datei."));
                Process.Start(filename);//Öffne PDF

                return;
            });
            await myTask;

            return;
        }
    }
}
