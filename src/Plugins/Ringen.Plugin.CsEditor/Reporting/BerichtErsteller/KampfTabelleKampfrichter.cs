using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.BerichtErsteller;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using Ringen.Shared.Models;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class KampfTabelleKampfrichter
    {
        public Table generate(MannschaftskampfViewModel mannschaftskampfViewModel, double randLinksRechts)
        {
            Table table = SetUpKampftabelle(randLinksRechts);
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

            AddKopfSpalte(kopfzeile, "Wertungen");
            AddKopfSpalte(kopfzeile, "Sieger");
            AddKopfSpalte(kopfzeile, "Siegart");
            AddKopfSpalte(kopfzeile, "Unterschrift Kampfrichter");
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

            var spalte = zeile.Cells[_kampfSpaltenCounter];
            spalte.Add(WertungenRunden());
            _kampfSpaltenCounter++;

            // Sieger ROT / Blau
            spalte = zeile.Cells[_kampfSpaltenCounter];
            spalte.VerticalAlignment = VerticalAlignment.Center;
            spalte.Format.Alignment = ParagraphAlignment.Center;
            spalte.Add(Sieger());
            _kampfSpaltenCounter++;

            // Siegarten
            spalte = zeile.Cells[_kampfSpaltenCounter];
            spalte.VerticalAlignment = VerticalAlignment.Center;
            spalte.Format.Alignment = ParagraphAlignment.Center;
            spalte.Add(Siegarten());
            _kampfSpaltenCounter++;
        }

        private TextFrame Sieger()
        {
            var result = new TextFrame();

            var paragraph = result.AddParagraph();
            paragraph.Format.Font.Size = CustomStyles.fontSizeNormal;

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText(" ROT");
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText(" BLAU");

            return result;
        }

        private TextFrame Siegarten()
        {
            var result = new TextFrame();
            result.Width = $"{_breite_Siegart}cm";

            var paragraph = result.AddParagraph();
            paragraph.Format.Font.Size = CustomStyles.fontSizeNormal;


            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("SS  ");

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("TÜ  ");

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("AS");

            paragraph.AddLineBreak();
            paragraph.AddLineBreak();

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("PS  ");

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("DS  ");

            paragraph.AddFormattedText("\u00A8", new Font("Wingdings"));
            paragraph.AddFormattedText("___");

            return result;
        }

        private TextFrame WertungenRunden()
        {
            var result = new TextFrame();

            var maxBreite = _breite_Wertungen;
            double breite_RundenNr = 1;
            double breite_Wertungen = maxBreite - breite_RundenNr;

            var table = result.AddTable();
            table.Borders.Visible = false;
            table.Style = CustomStyles.TABLEKLEIN;
            table.Rows.Height = "0.95cm";
            table.Rows.VerticalAlignment = VerticalAlignment.Center;

            Column column = table.AddColumn($"{breite_RundenNr}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{breite_Wertungen / 2}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{breite_Wertungen / 2}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            _kopfSpaltenCounter = 0;
            Row kopfzeile = table.AddRow();
            kopfzeile.Height = "0.3cm";
            Cell kopfSpalte = AddKopfSpalte(kopfzeile, "Runde");
            kopfSpalte.Format.Alignment = ParagraphAlignment.Center;
            kopfSpalte.Shading.Color = Colors.Gray;
            kopfSpalte.Format.Font.Color = Colors.White;

            kopfSpalte = kopfzeile.Cells[1];
            kopfSpalte.Format.Alignment = ParagraphAlignment.Center;
            kopfSpalte.Shading.Color = CustomStyles.ROT;
            kopfSpalte.Format.Font.Color = Colors.White;
            kopfSpalte.AddParagraph($"ROT");

            kopfSpalte = kopfzeile.Cells[2];
            kopfSpalte.Format.Alignment = ParagraphAlignment.Center;
            kopfSpalte.Shading.Color = CustomStyles.BLAU;
            kopfSpalte.Format.Font.Color = Colors.White;
            kopfSpalte.AddParagraph($"BLAU");

            int maxKampfrunden = 2;
            for (int i = 1; i <= maxKampfrunden; i++)
            {
                Row zeile = table.AddRow();
                Cell spalte = zeile.Cells[0];
                spalte.AddParagraph($"{i}");
                spalte.Borders.Right.Visible = true;

                var spalteZwischen = zeile.Cells[1];
                spalteZwischen.Borders.Right.Visible = true;

                if (i != maxKampfrunden)
                {

                    Row zeilePause = table.AddRow();
                    zeilePause.Height = "0.3cm";

                    Cell spalteLeer = zeilePause.Cells[0];
                    spalteLeer.Shading.Color = Colors.Gray;

                    Cell spaltePauseRot = zeilePause.Cells[1];
                    spaltePauseRot.Shading.Color = CustomStyles.ROT;
                    spaltePauseRot.Format.Font.Color = Colors.White;
                    spaltePauseRot.AddParagraph($"30 Sekunden Pause");


                    Cell spaltePauseBlau = zeilePause.Cells[2];
                    spaltePauseBlau.Shading.Color = CustomStyles.BLAU;
                    spaltePauseBlau.Format.Font.Color = Colors.White;
                    spaltePauseBlau.AddParagraph($"30 Sekunden Pause");
                }

            }


            return result;
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

        private double _breite_Wertungen = 12;
        private double _breite_Siegart = 3.4;
        private Table SetUpKampftabelle(double randLinksRechts)
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
            double a4LandscapeBreite = (29.7 - 2 * randLinksRechts);

            double breite_KampfNr = 1.2;
            double breite_Gewichtsklasse = 1;
            double breite_Name = 3;

            double breite_Sieger = 2;


            double breite_Unterschrift = a4LandscapeBreite - breite_KampfNr - breite_Gewichtsklasse - 2 * breite_Name - _breite_Wertungen - breite_Sieger - _breite_Siegart;

            Column column = table.AddColumn($"{breite_KampfNr}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Gewichtsklasse}cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Name}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{_breite_Wertungen}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Sieger}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{_breite_Siegart}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Unterschrift}cm");
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
