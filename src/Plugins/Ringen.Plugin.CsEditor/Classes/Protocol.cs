using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Ringen.Core.Messaging;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Reporting;

namespace Ringen.Plugin.CsEditor
{
    public class Protocol
    {
        public static async Task OnCreateProtocolAsync(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos = null) //TODO: ZusatzInfos von Oberfläche übergeben
        {
            var myTask = Task.Run(() =>
            {
                CreateProtocol(mannschaftskampfViewModel, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        private static void CreateProtocol(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(mannschaftskampfViewModel);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename =
                $"{mannschaftskampfViewModel.Kampfdatum.ToShortDateString()}_{mannschaftskampfViewModel.HeimMannschaft.Replace(' ', '-')}_vs_{mannschaftskampfViewModel.GastMannschaft.Replace(' ', '-')}.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Protokoll für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Bitte warten..."));

            IReport bericht = new ReportFarbigPdf();
            bericht.Export(pfad, mannschaftskampfViewModel, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Protokoll erfolgreich erstellt für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
            
        }

        private static CompetitionInfosViewModel TempTestdaten(MannschaftskampfViewModel mannschaftskampfViewModel)
        {
            CompetitionInfosViewModel zusatzInfos;
            //TODO: Aktuell nur zu Testzwecken, später von Oberfläche übergeben
            zusatzInfos = new CompetitionInfosViewModel()
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

        public static async Task OnCreateCreateAllListAsync(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos =null)
        {
            var myTask = Task.Run(() =>
            {
                CreateBoutResultList(mannschaftskampfViewModel, zusatzInfos);
                CreateProtocol(mannschaftskampfViewModel, zusatzInfos);
                CreateSprecherList(mannschaftskampfViewModel, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        public static async Task OnCreateBoutResultListAsync(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos =null)
        {
            var myTask = Task.Run(() =>
            {
                CreateBoutResultList(mannschaftskampfViewModel, zusatzInfos);

                return;
            });
            await myTask;

            return;
        }

        private static void CreateBoutResultList(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(mannschaftskampfViewModel);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"{mannschaftskampfViewModel.Kampfdatum.ToShortDateString()}_{mannschaftskampfViewModel.HeimMannschaft.Replace(' ', '-')}_vs_{mannschaftskampfViewModel.GastMannschaft.Replace(' ', '-')}_Ergebnisliste.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Ergebnisliste (Punktzettel) für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Bitte warten..."));

            IReport bericht = new ReportErgebnislisteKampfrichtertischPdf();
            bericht.Export(pfad, mannschaftskampfViewModel, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Ergebnisliste (Punktzettel) erfolgreich erstellt für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
        }

        private static void CreateSprecherList(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos)
        {
            //TODO Entfernen sobald in API vorhanden oder über UI
            zusatzInfos = TempTestdaten(mannschaftskampfViewModel);

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"{mannschaftskampfViewModel.Kampfdatum.ToShortDateString()}_{mannschaftskampfViewModel.HeimMannschaft.Replace(' ', '-')}_vs_{mannschaftskampfViewModel.GastMannschaft.Replace(' ', '-')}_Sprecher-Info.pdf";
            string pfad = Path.Combine(desktop, filename);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Erstelle Sprecher-Info für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Bitte warten..."));

            IReport bericht = new ReportSprecher();
            bericht.Export(pfad, mannschaftskampfViewModel, zusatzInfos);

            LoggerMessage.Send(new LogEntry(LogEntryType.Message,
                $"Ergebnisliste Sprecher-Info erfolgreich erstellt für Wettkampf {mannschaftskampfViewModel.Kampfdatum.ToShortDateString()} {mannschaftskampfViewModel.Value}. Öffne nun PDF-Datei."));
            Process.Start(pfad); //Öffne PDF
        }
    }
}
