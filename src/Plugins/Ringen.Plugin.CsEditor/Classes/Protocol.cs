using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var myTask = Task.Run(() =>
            {
                CreateProtocol(competition, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        private static void CreateProtocol(Competition competition, CompetitionInfos zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(competition);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename =
                $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Protokoll für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Bitte warten..."));

            IReport bericht = new ReportFarbigPdf();
            bericht.Export(pfad, competition, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Protokoll erfolgreich erstellt für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
            
        }

        private static CompetitionInfos TempTestdaten(Competition competition)
        {
            CompetitionInfos zusatzInfos;
            //TODO: Aktuell nur zu Testzwecken, später von Oberfläche übergeben
            zusatzInfos = new CompetitionInfos()
            {
                ErgebnislistenSchreiber = "Tahar Alemann",
                Kampfart = "Verbandskampf",
                MannschaftsfuehrerHeim = "Halil Aygün",
                MannschaftsfuehrerGast = "Maximilian Fleischer",
                Ordner = new List<string>() {"Jürgen Pfaffe", "Christoph Zapf"},
                Protokollfuehrer = "Anna Badewitz",
                VorKampfRueckKampf = "Rückkampf"
            };

            return zusatzInfos;
        }

        public static async Task OnCreateCreateAllListAsync(Competition competition, CompetitionInfos zusatzInfos=null)
        {
            var myTask = Task.Run(() =>
            {
                CreateBoutResultList(competition, zusatzInfos);
                CreateProtocol(competition, zusatzInfos);
                CreateSprecherList(competition, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        public static async Task OnCreateBoutResultListAsync(Core.CS.Competition competition, CompetitionInfos zusatzInfos=null)
        {
            var myTask = Task.Run(() =>
            {
                CreateBoutResultList(competition, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        private static void CreateBoutResultList(Competition competition, CompetitionInfos zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(competition);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}_Ergebnisliste.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Ergebnisliste (Punktzettel) für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Bitte warten..."));

            IReport bericht = new ReportErgebnislisteKampfrichtertischPdf();
            bericht.Export(pfad, competition, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Ergebnisliste (Punktzettel) erfolgreich erstellt für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
        }

        private static void CreateSprecherList(Competition competition, CompetitionInfos zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(competition);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}_Sprecher-Info.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Sprecher-Info für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Bitte warten..."));

            IReport bericht = new ReportSprecher();
            bericht.Export(pfad, competition, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Ergebnisliste Sprecher-Info erfolgreich erstellt für Wettkampf {competition.BoutDateDateTime.ToShortDateString()} {competition.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
        }
    }
}
