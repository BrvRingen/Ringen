﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Reporting;

namespace Ringen.Plugin.CsEditor.Tests
{
    [TestFixture]
    public class ProtocolPdfGenerierungTests
    {
        [Test]
        public void TesteErgebnislisteExport()
        {
            string daten = File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestDaten\\2019_Landesliga_RCA-Bayreuth_vs_ASV-Hof.json");
            MannschaftskampfViewModel testMannschaftskampfViewModel = new MannschaftskampfViewModel(JObject.Parse(daten), null);
            var zusatzInfos = GetDemoZusatzInfos();

            Random random = new Random();
            string filename = $"Test_{random.Next()}.pdf";

            IReport bericht = new ReportErgebnislisteKampfrichtertischPdf();
            bericht.Export(filename, testMannschaftskampfViewModel, zusatzInfos);
            Process.Start(filename);//Öffne PDF
        }

        [Test]
        public void TestePdfExport()
        {
            string daten = File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestDaten\\2019_Landesliga_RCA-Bayreuth_vs_ASV-Hof.json");
            MannschaftskampfViewModel testMannschaftskampfViewModel = new MannschaftskampfViewModel(JObject.Parse(daten), null);

            var zusatzInfos = GetDemoZusatzInfos();

            //TODO Löschen, sobald wirklich Punkte hinterlegt sind
            Random rnd = new Random();
            int cnt = 0;
            foreach (Bout bout in testMannschaftskampfViewModel.Children)
            {
                for (int i = 0; i < rnd.Next(1, 10); i++)
                {
                    var ringer = (rnd.Next(1, 3) == 1 ? Core.CS.BoutPoint.Wrestler.Home : Core.CS.BoutPoint.Wrestler.Opponent);

                    var zeit = new DateTime(
                        testMannschaftskampfViewModel.BoutDateDateTime.Year,
                        testMannschaftskampfViewModel.BoutDateDateTime.Month,
                        testMannschaftskampfViewModel.BoutDateDateTime.Day,
                        testMannschaftskampfViewModel.ScaleTime.Hours,
                        testMannschaftskampfViewModel.ScaleTime.Minutes + cnt + i + 4,
                        rnd.Next(1, 59)
                    );

                    bout.Points.Add(new Core.CS.BoutPoint(rnd.Next(1, 5).ToString(), null, ringer, zeit));
                }

                cnt++;
            }

            Random random = new Random();
            string filename = $"Test_{random.Next()}.pdf";

            IReport bericht = new ReportPdf();
            bericht.Export(filename, testMannschaftskampfViewModel, zusatzInfos);
            Process.Start(filename);//Öffne PDF

        }

        private static CompetitionInfos GetDemoZusatzInfos()
        {
            //TODO: Aktuell nur zu Testzwecken, später von Oberfläche übergeben
            CompetitionInfos zusatzInfos = new CompetitionInfos()
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

        [Test]
        public void TestePdfFarbigExport()
        {
            string daten = File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestDaten\\2019_Landesliga_RCA-Bayreuth_vs_ASV-Hof.json");
            MannschaftskampfViewModel testMannschaftskampfViewModel = new MannschaftskampfViewModel(JObject.Parse(daten), null);

            var zusatzInfos = GetDemoZusatzInfos();

            //TODO Löschen, sobald wirklich Punkte hinterlegt sind
            Random rnd = new Random();
            int cnt = 0;
            foreach (Bout bout in testMannschaftskampfViewModel.Children)
            {
                for (int i = 0; i < rnd.Next(1, 10); i++)
                {
                    var ringer = (rnd.Next(1, 3) == 1 ? Core.CS.BoutPoint.Wrestler.Home : Core.CS.BoutPoint.Wrestler.Opponent);

                    var zeit = new DateTime(
                        testMannschaftskampfViewModel.BoutDateDateTime.Year,
                        testMannschaftskampfViewModel.BoutDateDateTime.Month, 
                        testMannschaftskampfViewModel.BoutDateDateTime.Day,
                        testMannschaftskampfViewModel.ScaleTime.Hours,
                        testMannschaftskampfViewModel.ScaleTime.Minutes + cnt + i + 4,
                        rnd.Next(1, 59)
                        );

                    bout.Points.Add(new Core.CS.BoutPoint(rnd.Next(1, 5).ToString(), null, ringer, zeit));
                }

                cnt++;
            }

            Random random = new Random();
            string filename = $"Test_{random.Next()}.pdf";

            IReport bericht = new ReportFarbigPdf();
            bericht.Export(filename, testMannschaftskampfViewModel, zusatzInfos);
            Process.Start(filename);//Öffne PDF

        }

    }
}
