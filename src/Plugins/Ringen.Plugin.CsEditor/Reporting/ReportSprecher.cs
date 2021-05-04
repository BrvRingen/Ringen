using System;
using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.BerichtErsteller;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using Ringen.Shared.Models;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportSprecher : IReport
    {
        private const double _randLinksRechts = 0.8;
        private ExportPdf exportPdf= new ExportPdf();
        private DefaultElemente defaultElemente = new DefaultElemente();
        private KampfInformationen kampfInformationen = new KampfInformationen();


        public void Export(string pfad, MannschaftskampfViewModel daten, CompetitionInfos zusatzInfos)
        {
            Document report = ErstelleBericht(daten, zusatzInfos);
            exportPdf.Export(pfad, report);
        }


        public Document ErstelleBericht(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            string title = "";
            //TODO kläre warum ein Dokument einen Titel hat und diese nicht, was ist der default wert?
            Document document = defaultElemente.GetDocument(title);
            Section hauptSection = defaultElemente.GetHauptSection(_randLinksRechts);
            ErstelleHauptSektion(hauptSection, mannschaftskampfViewModel, zusatzInfos);
            document.Add(hauptSection);

            return document;
        }


        private Section ErstelleHauptSektion(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            ErgaenzeHeader(section, mannschaftskampfViewModel, zusatzInfos);
            ErgaenzeInhalt(section, mannschaftskampfViewModel, zusatzInfos);
            ErgaenzeFooter(section, mannschaftskampfViewModel, zusatzInfos.MannschaftsfuehrerHeim, zusatzInfos.MannschaftsfuehrerGast);

            return section;
        }

        private void ErgaenzeInhalt(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            section.Add(Kampftabelle(mannschaftskampfViewModel));
        }
        
        private void ErgaenzeHeader(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            section.Headers.Primary.Add(defaultElemente.GetUeberschrift("Sprecher Informationen"));
            section.Add(kampfInformationen.generate(mannschaftskampfViewModel, zusatzInfos, _randLinksRechts));

            section.Headers.Primary.Add(PdfHelper.AddAbstandNachOben("0.1cm"));
        }
        
        private Table KampfInfos(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
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
            var ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.LigaId} {mannschaftskampfViewModel.TableId}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;
            
            spalte = zeile.Cells[1].AddParagraph();
            ft = spalte.AddFormattedText($"{zusatzInfos.Kampfart} {mannschaftskampfViewModel.HeimMannschaft} vs. {mannschaftskampfViewModel.GastMannschaft}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;

            spalte = zeile.Cells[2].AddParagraph();
            ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.Kampfdatum.ToShortDateString()}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeNormal;
            ft.Font.Name = CustomStyles.fontUeberschriften;

            return table;
        }

        private void ErgaenzeFooter(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, string trainerHeim, string trainerGast)
        {
        }
        
        private Table Kampftabelle(MannschaftskampfViewModel mannschaftskampfViewModel)
        {
            Table table = SetUpKampftabelle();
            Row kopfzeile = table.AddRow();
            Kopfzeilen(kopfzeile);
            Kampfzeilen(table, mannschaftskampfViewModel.Children);

            return table;
        }

        private void Kampfzeilen(Table table, List<EinzelkampfViewModel> kaempfe)
        {
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
            spalteRot.Shading.Color = CustomStyles.ROT;

            var spalteBlau = AddKopfSpalte(kopfzeile, "Name Blau");
            spalteBlau.Shading.Color = CustomStyles.BLAU;
        }

        private void InhaltKampfzeile(Row zeile, EinzelkampfViewModel kampf)
        {
            AddKampfSpalte(zeile, kampf.KampfNr.ToString());
            AddKampfSpalte(zeile, kampf.Gewichtsklasse);

            var heimRingerSpalte = AddKampfSpalte(zeile, kampf.IsNoHomeWrestler() ? "--" : $"{kampf.HeimRinger.Vorname} {kampf.HeimRinger.Nachname}");
            heimRingerSpalte.Borders.Color = CustomStyles.ROT;
            heimRingerSpalte.Format.Font.Bold = true;
            heimRingerSpalte.Format.Font.Color = CustomStyles.ROT;

            var gastRingerSpalte = AddKampfSpalte(zeile, kampf.IsNoOpponentWrestler() ? "--" : $"{kampf.GastRinger.Vorname} {kampf.GastRinger.Nachname}");
            gastRingerSpalte.Borders.Color = CustomStyles.BLAU;
            gastRingerSpalte.Format.Font.Bold = true;
            gastRingerSpalte.Format.Font.Color = CustomStyles.BLAU;
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
