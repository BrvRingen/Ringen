using System;
using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using Ringen.Shared.Models;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportSprecher : IReport
    {
        private const double _randLinksRechts = 0.8;

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
            Document document = new Document();
            CustomStyles.Definiere(document);
            document.Add(ErstelleHauptSektion(competition, zusatzInfos, document.DefaultPageSetup));

            return document;
        }


        private Section ErstelleHauptSektion(Competition competition, CompetitionInfos zusatzInfos, PageSetup defaultPageSetup)
        {
            var section = new Section();
            SetUpPage(section, defaultPageSetup);

            ErgaenzeHeader(section, competition, zusatzInfos);
            ErgaenzeInhalt(section, competition, zusatzInfos);
            ErgaenzeFooter(section, competition, zusatzInfos.MannschaftsfuehrerHeim, zusatzInfos.MannschaftsfuehrerGast);

            return section;
        }

        private void ErgaenzeInhalt(Section section, Competition competition, CompetitionInfos zusatzInfos)
        {
            section.Add(Kampftabelle(competition));
        }
        
        private void ErgaenzeHeader(Section section, Competition competition, CompetitionInfos zusatzInfos)
        {
            section.Headers.Primary.Add(Ueberschrift());
            section.Headers.Primary.Add(KampfInfos(competition, zusatzInfos));

            section.Headers.Primary.Add(PdfHelper.AddAbstandNachOben("0.1cm"));
        }

        private Paragraph Ueberschrift()
        {
            Paragraph header = new Paragraph();
            header.AddFormattedText("Sprecher Informationen", TextFormat.Underline);
            header.Format.Font.Size = CustomStyles.fontSizeGross;
            header.Format.Alignment = ParagraphAlignment.Left;
            header.Format.Font.Bold = true;
            header.Format.Font.Name = CustomStyles.fontUeberschriften;

            return header;
        }
        
        private Table KampfInfos(Competition competition, CompetitionInfos zusatzInfos)
        {
            var table = new Table();
            table.Style = CustomStyles.TABLEINFO;

            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts) / 3.0;

            Column column = table.AddColumn($"{a4LandscapeBreite - 2}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{a4LandscapeBreite + 4}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{a4LandscapeBreite - 2}cm");
            column.Format.Alignment = ParagraphAlignment.Right;
            
            Row zeile = table.AddRow();

            var spalte = zeile.Cells[0].AddParagraph();
            var ft = spalte.AddFormattedText($"{competition.LigaId} {competition.TableId}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;
            
            spalte = zeile.Cells[1].AddParagraph();
            ft = spalte.AddFormattedText($"{zusatzInfos.Kampfart} {competition.HomeTeamName} vs. {competition.OpponentTeamName}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;

            spalte = zeile.Cells[2].AddParagraph();
            ft = spalte.AddFormattedText($"{DateTime.Parse(competition.BoutDate).ToShortDateString()}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;

            return table;
        }

        private void ErgaenzeFooter(Section section, Competition competition, string trainerHeim, string trainerGast)
        {
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
        }


        private Table Kampftabelle(Competition competition)
        {
            Table table = SetUpKampftabelle();
            Row kopfzeile = table.AddRow();
            Kopfzeilen(kopfzeile);
            Kampfzeilen(table, competition.Children);

            return table;
        }

        private void Kampfzeilen(Table table, List<Bout> kaempfe)
        {
            int cnt = 0;
            foreach (var kampf in kaempfe.OrderBy(li => li.KampfNr))
            {
                Row zeile = table.AddRow();
                InhaltKampfzeile(zeile, kampf);
                _kampfSpaltenCounter = 0;
                zeile.Borders.Bottom.Width = "0.15cm";
            }
        }

        private void Kopfzeilen(Row kopfzeile)
        {
            _kopfSpaltenCounter = 0;

            SetUpKopfzeile(kopfzeile);

            AddKopfSpalte(kopfzeile, "Kampf-Nr.");
            AddKopfSpalte(kopfzeile, "Bis kg");
            var spalteRot = AddKopfSpalte(kopfzeile, "Name Rot");
            spalteRot.Shading.Color = Colors.Red;

            var spalteBlau = AddKopfSpalte(kopfzeile, "Name Blau");
            spalteBlau.Shading.Color = Colors.Blue;
        }

        private void InhaltKampfzeile(Row zeile, Bout kampf)
        {
            AddKampfSpalte(zeile, kampf.KampfNr.ToString());
            AddKampfSpalte(zeile, kampf.WeightClass);

            var homeWrestlerSpalte = AddKampfSpalte(zeile, kampf.IsNoHomeWrestler() ? "--" : kampf.HomeWrestlerFullnname.Trim());
            homeWrestlerSpalte.Borders.Color = Colors.Red;
            homeWrestlerSpalte.Format.Font.Bold = true;
            homeWrestlerSpalte.Format.Font.Color = Colors.Red;

            var opponentWrestlerSpalte = AddKampfSpalte(zeile, kampf.IsNoOpponentWrestler() ? "--" : kampf.OpponentWrestlerFullnname.Trim());
            opponentWrestlerSpalte.Borders.Color = Colors.Blue;
            opponentWrestlerSpalte.Format.Font.Bold = true;
            opponentWrestlerSpalte.Format.Font.Color = Colors.Blue;
        }
        
        private int _kampfSpaltenCounter = 0;
        private Cell AddKampfSpalte(Row zeile, string inhalt, int borderRightWidth = 0)
        {
            var spalte = zeile.Cells[_kampfSpaltenCounter];
            spalte.AddParagraph(inhalt);

            if (borderRightWidth > 0)
            {
                spalte.Borders.Right.Width = borderRightWidth;
                spalte.Borders.Right.Color = Colors.DarkGray;
            }

            _kampfSpaltenCounter++;
            return spalte;
        }

        private Table SetUpKampftabelle()
        {
            Table table = new Table();

            table.Style = CustomStyles.TABLEKLEIN;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.25;
            table.Borders.Right.Width = 0.25;
            table.Rows.LeftIndent = 0;
            table.Rows.VerticalAlignment = VerticalAlignment.Center;

            // Before you can add a row, you must define the columns
            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts);

            double breite_KampfNr = 1.2;
            double breite_Gewichtsklasse = 1;
            double breite_Name = 3;
            
            Column column = table.AddColumn($"{breite_KampfNr}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Gewichtsklasse}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            return table;
        }

        
        private void SetUpKopfzeile(Row kopfzeile)
        {
            kopfzeile.HeadingFormat = true;
            kopfzeile.Format.Alignment = ParagraphAlignment.Center;
            kopfzeile.Format.Font.Bold = true;
            kopfzeile.Format.Font.Color = Colors.White;
            kopfzeile.Shading.Color = Colors.Black;
        }

        private int _kopfSpaltenCounter = 0;
        private Cell AddKopfSpalte(Row zeile, string bezeichnung, int borderRightWidth = 0)
        {
            Cell spalte = zeile.Cells[_kopfSpaltenCounter];
            spalte.AddParagraph(bezeichnung);
            spalte.Format.Font.Bold = true;
            spalte.Format.Alignment = ParagraphAlignment.Left;
            spalte.VerticalAlignment = VerticalAlignment.Center;

            if (borderRightWidth > 0)
            {
                spalte.Borders.Right.Width = borderRightWidth;
            }

            _kopfSpaltenCounter++;

            return spalte;
        }
    }
}
