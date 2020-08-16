﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EnumsNET;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using Ringen.Shared.Models;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportPdf : IReport
    {
        private const double _randLinksRechts = 1.2;

        public void Export(string pfad, Competition daten, CompetitionInfos zusatzInfos)
        {
            ExportPdf(pfad, ErstelleBericht(daten, zusatzInfos));
        }

        private void ExportPdf(string path, Document report)
        {
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = report;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(path);
        }

        public Document ErstelleBericht(Competition competition, CompetitionInfos zusatzInfos)
        {
            Document document = new Document
            {
                Info =
                {
                    Title = string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolTitle, competition.LigaId, competition.TableId, competition.HomeTeamName, competition.OpponentTeamName, DateTime.Parse(competition.BoutDate).ToShortDateString())
                }
            };

            CustomStyles.Definiere(document);

            document.Add(ErstelleHauptSektion(competition, zusatzInfos, document.DefaultPageSetup));

            return document;
        }

        private Section ErstelleHauptSektion(Competition competition, CompetitionInfos zusatzInfos, PageSetup defaultPageSetup)
        {
            var section = new Section();
            SetUpPage(section, defaultPageSetup);
            
            ErgaenzeHeader(section, competition, zusatzInfos);
            ErgaenzeInhalt(section, competition);
            ErgaenzeFooter(section, competition, zusatzInfos.MannschaftsfuehrerHeim, zusatzInfos.MannschaftsfuehrerGast);

            return section;
        }

        private void ErgaenzeInhalt(Section section, Competition competition)
        {
            section.Add(AddAbstandNachOben("0.8cm"));
            section.Add(ErgaenzeKampftabelle(competition));
            section.Add(Bemerkungen(competition.HomeTeamName, Convert.ToInt32(competition.HomePoints), 
                competition.OpponentTeamName, Convert.ToInt32(competition.OpponentPoints), competition.EditorComment));
        }

        private void ErgaenzeHeader(Section section, Competition competition, CompetitionInfos zusatzInfos)
        {
            section.Headers.Primary.Add(Ueberschrift());
            section.Add(KampfInfos(competition, zusatzInfos));
        }

        private void ErgaenzeFooter(Section section, Competition competition, string trainerHeim, string trainerGast)
        {
            section.Footers.Primary.Add(FooterUnterschriften( competition.Referee, trainerHeim, trainerGast));
            section.Footers.Primary.Add(FooterHinweisZeile());
        }

        private void SetUpPage(Section section, PageSetup defaultPageSetup)
        {
            section.PageSetup = defaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.LeftMargin = $"{_randLinksRechts}cm";
            section.PageSetup.RightMargin = $"{_randLinksRechts}cm";
            section.PageSetup.HeaderDistance = "1.2cm";
            section.PageSetup.FooterDistance = "0.5cm";
            //section.PageSetup.TopMargin = 0;
        }

        private Paragraph FooterHinweisZeile()
        {
            Paragraph footer = new Paragraph();
            footer.AddText(string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolFooterHint, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            footer.Format.Font.Size = CustomStyles.fontSizeExtremKlein;
            footer.Format.Alignment = ParagraphAlignment.Center;
            footer.Format.SpaceBefore = "0.5cm";

            return footer;
        }

        private Table FooterUnterschriften(string schiedsrichter, string trainerHeim, string trainerGast)
        {
            Table table = new Table();
            table.Style = CustomStyles.TABLE;
            table.Borders.Visible = false;


            double abstand = 1;
            double a4LandscapeBreite = (29.7 - 2*_randLinksRechts - 2* abstand);
            double spaltenBreite = (a4LandscapeBreite / 3);



            Column column = table.AddColumn($"{spaltenBreite}cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column.Borders.Top.Visible = true;

            column = table.AddColumn($"{abstand}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{spaltenBreite}cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column.Borders.Top.Visible = true;

            column = table.AddColumn($"{abstand}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{spaltenBreite}cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column.Borders.Top.Visible = true;


            Row zeile = table.AddRow();

            var spalte = zeile.Cells[0].AddParagraph();
            spalte.AddFormattedText(trainerHeim, TextFormat.Bold);
            spalte.AddLineBreak();
            spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Trainer} {Resources.LanguageFiles.DictPluginMain.HomeTeamNameShort}", TextFormat.Italic);

            spalte = zeile.Cells[2].AddParagraph();
            spalte.AddFormattedText(schiedsrichter, TextFormat.Bold);
            spalte.AddLineBreak();
            spalte.AddFormattedText(Resources.LanguageFiles.DictPluginMain.Referee, TextFormat.Italic);

            spalte = zeile.Cells[4].AddParagraph();
            spalte.AddFormattedText(trainerGast, TextFormat.Bold);
            spalte.AddLineBreak();
            spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Trainer} {Resources.LanguageFiles.DictPluginMain.OpponentTeamNameShort}", TextFormat.Italic);

            return table;
        }

        private TextFrame Bemerkungen(string homeTeamName, int homePoints, string opponentTeamName, int opponentPoints, string editorComment)
        {
            string sieger = Resources.LanguageFiles.DictPluginMain.Undetermined;
            if (homePoints > opponentPoints)
            {
                sieger = $"{Resources.LanguageFiles.DictPluginMain.HomeTeamNameShort} {homeTeamName}";
            }
            else if(homePoints < opponentPoints)
            {
                sieger = $"{Resources.LanguageFiles.DictPluginMain.OpponentTeamNameShort} {opponentTeamName}";
            }
            else if (homePoints == opponentPoints)
            {
                sieger = $"{Resources.LanguageFiles.DictPluginMain.Tie}";
            }

            var result = new TextFrame();
            result.Width = "25cm";

            var table = result.AddTable();
            table.Borders.Visible = false;
            table.TopPadding = "0.5cm";

            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts);

            Column column = table.AddColumn($"10cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{a4LandscapeBreite-10}cm");
            column.Format.Alignment = ParagraphAlignment.Left;


            Row zeile = table.AddRow();

            var spalte = zeile.Cells[0].AddParagraph();
            spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Winner}:", TextFormat.Bold);
            spalte.Format.Font.Bold = true;

            spalte = zeile.Cells[0].AddParagraph();
            spalte.AddFormattedText(string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolWinnerLine, sieger, homePoints, opponentPoints), TextFormat.Underline);
            spalte.Format.Font.Size = CustomStyles.fontSizeGross;
            spalte.Format.Font.Bold = true;

            spalte = zeile.Cells[1].AddParagraph();
            //spalte.Format.Borders.Visible = true;
            spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.EditorComment}:", TextFormat.Bold);
            spalte.AddLineBreak();
            spalte.AddText(editorComment);

            
            //result.AddParagraph($"Gesamtkampfzeit: ??"); //TODO: Gesamte Kampfzeit woher?
            
            
            return result;
        }

        private void GesamtErgebnis(Row zeile, int homePoints, int opponentPoints)
        {
            zeile.Borders.Visible = false;
            
            zeile.Cells[6].AddParagraph($"{homePoints}");
            zeile.Cells[6].Format.Font.Bold = true;
            zeile.Cells[6].Format.Font.Size = CustomStyles.fontSizeNormal;
            zeile.Cells[6].Borders.Visible = true;
            zeile.Cells[6].Borders.Top.Width = 1.5;

            zeile.Cells[11].AddParagraph($"{opponentPoints}");
            zeile.Cells[11].Format.Font.Bold = true;
            zeile.Cells[11].Format.Font.Size = CustomStyles.fontSizeNormal;
            zeile.Cells[11].Borders.Visible = true;
            zeile.Cells[11].Borders.Top.Width = 1.5;
        }

        private Paragraph AddAbstandNachOben(string abstand)
        {
            Paragraph p = new Paragraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = "0mm";
            p.Format.SpaceBefore = abstand;

            return p;
        }

        private Table ErgaenzeKampftabelle(Competition competition)
        {
            Table table = new Table();
            SetUpKampftabelle(table);

            Row zeileMannschaften = table.AddRow();
            MannschaftenUeberschrift(zeileMannschaften, competition);
            
            var kopfzeile = table.AddRow();
            Kopfzeilen(kopfzeile);

            Kampfzeilen(table, competition.Children);
            
            Row zeile = table.AddRow();
            GesamtErgebnis(zeile, Convert.ToInt32(competition.HomePoints), Convert.ToInt32(competition.OpponentPoints));

            return table;
        }

        private void Kopfzeilen(Row kopfzeile)
        {
            SetUpKopfzeile(kopfzeile);

            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlingStyle);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WeightClass, 1);

            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.ActualWeightShort);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerName);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerId);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerStatus);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.TeamPointsShort, 2);

            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.ActualWeightShort);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerName);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerId);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerStatus);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.TeamPointsShort, 1);

            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.VictoryType);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.PdfProtocolSinglePoints);
        }

        private void MannschaftenUeberschrift(Row mannschaften, Competition competition)
        {
            mannschaften.Format.Font.Bold = true;
            mannschaften.Borders.Visible = false;

            mannschaften.Cells[2].AddParagraph(competition.HomeTeamName);
            mannschaften.Cells[2].Format.Font.Bold = true;
            mannschaften.Cells[2].Format.Font.Size = CustomStyles.fontSizeGross;
            mannschaften.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            mannschaften.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            mannschaften.Cells[2].MergeRight = 4;
            mannschaften.Cells[2].Borders.Right.Width = 2;

            mannschaften.Cells[7].AddParagraph(competition.OpponentTeamName);
            mannschaften.Cells[7].Format.Font.Bold = true;
            mannschaften.Cells[7].Format.Font.Size = CustomStyles.fontSizeGross;
            mannschaften.Cells[7].Format.Alignment = ParagraphAlignment.Left;
            mannschaften.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            mannschaften.Cells[7].MergeRight = 4;
            mannschaften.Cells[7].Borders.Right.Width = 1;
        }

        private static void SetUpKopfzeile(Row kopfzeile)
        {
            kopfzeile.HeadingFormat = true;
            kopfzeile.Format.Alignment = ParagraphAlignment.Center;
            kopfzeile.Format.Font.Bold = true;
            kopfzeile.Format.Font.Color = Colors.White;
            kopfzeile.Shading.Color = Colors.Black;
        }

        private void SetUpKampftabelle(Table table)
        {
            table.Style = CustomStyles.TABLE;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.25;
            table.Borders.Right.Width = 0.25;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts);

            double breite_Stilart = 1.5;
            double breite_Gewichtsklasse = 1.0;
            double breite_IstGewicht = 1.1;
            double breite_PassNr = 1.1;
            double breite_Status = 1.2;
            double breite_Punkte = 1.0;
            double breite_Siegart = 2.8;
            double breite_Wertungen = 3.5;

            double nochUebrig = a4LandscapeBreite - breite_Stilart - breite_Gewichtsklasse - 2 * breite_IstGewicht -
                                2 * breite_PassNr - 2 * breite_Status - 2 * breite_Punkte - breite_Siegart - breite_Wertungen;
            double breite_Name = nochUebrig / 2;

            Column column = table.AddColumn($"{breite_Stilart}cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn($"{breite_Gewichtsklasse}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_IstGewicht}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_PassNr}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Status}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{breite_Punkte}cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column.Format.Font.Bold = true;

            column = table.AddColumn($"{breite_IstGewicht}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_PassNr}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Status}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Punkte}cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column.Format.Font.Bold = true;

            column = table.AddColumn($"{breite_Siegart}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Wertungen}cm");
            column.Format.Alignment = ParagraphAlignment.Left;
        }

        private Paragraph Ueberschrift()
        {
            Paragraph header = new Paragraph();
            header.AddFormattedText(Resources.LanguageFiles.DictPluginMain.PdfProtocolHeadline, TextFormat.Underline);
            header.Format.Font.Size = CustomStyles.fontSizeTitel;
            header.Format.Alignment = ParagraphAlignment.Left;
            header.Format.Font.Bold = true;

            return header;
        }

        private Table KampfInfos(Competition competition, CompetitionInfos zusatzInfos)
        {
            var table = new Table();
            table.Style = CustomStyles.TABLEINFO;
            table.Borders.Visible = false;

            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts) / 3.0;

            Column column = table.AddColumn($"{a4LandscapeBreite-2}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{a4LandscapeBreite+4}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{a4LandscapeBreite-2}cm");
            column.Format.Alignment = ParagraphAlignment.Right;


            Row zeile = table.AddRow();

            var spalte = zeile.Cells[0].AddParagraph();
            var ft = spalte.AddFormattedText($"{competition.LigaId} {competition.TableId}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;
            spalte.AddLineBreak();

            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Location}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{competition.Location.Split(' ').Last()}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            spalte = zeile.Cells[1].AddParagraph();
            ft = spalte.AddFormattedText($"{zusatzInfos.Kampfart}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;
            spalte.AddLineBreak();

            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.LocationAddress}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{competition.Location}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            spalte = zeile.Cells[2].AddParagraph();
            ft = spalte.AddFormattedText($"{zusatzInfos.VorKampfRueckKampf}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;
            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.BoutDate}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{DateTime.Parse(competition.BoutDate).ToShortDateString()}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            return table;
        }

        private int kopfSpaltenCounter = 0;
        private Cell AddKopfSpalte(Row zeile, string bezeichnung, int borderRightWidth = 0)
        {
            Cell spalte = zeile.Cells[kopfSpaltenCounter];
            spalte.AddParagraph(bezeichnung);
            spalte.Format.Font.Bold = true;
            spalte.Format.Alignment = ParagraphAlignment.Left;
            spalte.VerticalAlignment = VerticalAlignment.Center;

            if (borderRightWidth > 0)
            {
                spalte.Borders.Right.Width = borderRightWidth;
            }

            kopfSpaltenCounter++;

            return spalte;
        }


        private void Kampfzeilen(Table table, List<Bout> kaempfe)
        {
            int cnt = 0;
            foreach (var kampf in kaempfe)
            {
                Row zeile = table.AddRow();
                InhaltKampfzeile(zeile, kampf);
                
                if (cnt % 2 != 0)
                {
                    zeile.Shading.Color = Colors.LightGray;
                }
                cnt++;
                kampfSpaltenCounter = 0;
            }
        }

        private void InhaltKampfzeile(Row zeile, Bout kampf)
        {
            AddKampfSpalte(zeile, ((BoutSettings.WrestleStyles) kampf.WrestleStyle).AsString(EnumFormat.Description));

            AddKampfSpalte(zeile, kampf.WeightClass, 1);

            AddKampfSpalte(zeile, kampf.HomeWrestlerWeight.ToString("0.0"));
            var homeWrestlerSpalte = AddKampfSpalte(zeile,
                string.IsNullOrEmpty(kampf.HomeWrestlerFullnname.Trim()) ? "--" : kampf.HomeWrestlerFullnname.Trim());
            if (kampf.HomeWrestlerPoints > kampf.OpponentWrestlerPoints)
            {
                homeWrestlerSpalte.Format.Font.Bold = true;
            }

            AddKampfSpalte(zeile, kampf.HomeWrestlerId > 0 ? kampf.HomeWrestlerId.ToString() : "--");
            AddKampfSpalte(zeile, kampf.HomeWrestlerStatus ?? string.Empty);
            AddKampfSpalte(zeile, kampf.HomeWrestlerPoints > 0 ? kampf.HomeWrestlerPoints.ToString() : "0", 2);


            AddKampfSpalte(zeile, kampf.OpponentWrestlerWeight.ToString("0.0"));
            var opponentWrestlerSpalte = AddKampfSpalte(zeile,
                string.IsNullOrEmpty(kampf.OpponentWrestlerFullnname.Trim()) ? "--" : kampf.OpponentWrestlerFullnname.Trim());
            if (kampf.OpponentWrestlerPoints > kampf.HomeWrestlerPoints)
            {
                opponentWrestlerSpalte.Format.Font.Bold = true;
            }

            AddKampfSpalte(zeile, kampf.OpponentWrestlerId > 0 ? kampf.OpponentWrestlerId.ToString() : "--");
            AddKampfSpalte(zeile, kampf.OpponentWrestlerStatus ?? string.Empty);
            AddKampfSpalte(zeile, kampf.OpponentWrestlerPoints > 0 ? kampf.OpponentWrestlerPoints.ToString() : "0", 1);

            var kampfzeit = TimeSpan.FromSeconds(kampf.Settings.Times[Ringen.Core.CS.BoutTime.Types.Bout.ToString()].Time).ToString("m':'ss");
            AddKampfSpalte(zeile, string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolResult, kampf.Result, kampfzeit));

            var spalte = zeile.Cells[kampfSpaltenCounter];
            kampfSpaltenCounter++;
            spalte.Borders.Color = Colors.Black;

            Paragraph punkteParagraph = spalte.AddParagraph();
            
            foreach (var punkt in kampf.Points)
            {
                var font = new Font();
                var ft = punkteParagraph.AddFormattedText(punkt.Value, font);

                if (punkt.HomeOrOpponent == Core.CS.BoutPoint.Wrestler.Home)
                {
                    ft.Style = CustomStyles.WERTUNG_ROT;
                }
                else if (punkt.HomeOrOpponent == Core.CS.BoutPoint.Wrestler.Opponent)
                {
                    ft.Style = CustomStyles.WERTUNG_BLAU;
                }

                var ftTrennzeichen = punkteParagraph.AddFormattedText(" ");
                ftTrennzeichen.Style = StyleNames.Normal;
            }

        }

        private int kampfSpaltenCounter = 0;
        private Cell AddKampfSpalte(Row zeile, string inhalt, int borderRightWidth = 0)
        {
            var spalte = zeile.Cells[kampfSpaltenCounter]; 
            spalte.Borders.Color = Colors.Black;

            spalte.AddParagraph(inhalt);

            if (borderRightWidth > 0)
            {
                spalte.Borders.Right.Width = borderRightWidth;
                spalte.Borders.Right.Color = Colors.DarkGray;
            }

            kampfSpaltenCounter++;
            return spalte;
        }
    }
}