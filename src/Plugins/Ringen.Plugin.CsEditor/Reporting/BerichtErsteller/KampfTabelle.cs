using EnumsNET;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class KampfTabelle
    {
        public Table generate(MannschaftskampfViewModel mannschaftskampfViewModel, double randLinksRechts, bool useColor)
        {

            Color colorRed = Colors.Black;
            Color colorBlue = Colors.Black;
            if (useColor)
            {
                colorRed = CustomStyles.ROT;
                colorBlue = CustomStyles.BLAU;
            }

            Table table = SetUpKampftabelle(randLinksRechts);

            Row zeileMannschaften = table.AddRow();
            MannschaftenUeberschrift(zeileMannschaften, mannschaftskampfViewModel);

            var kopfzeile = table.AddRow();
            Kopfzeilen(kopfzeile, colorRed, colorBlue);

            Kampfzeilen(table, mannschaftskampfViewModel.Children, colorRed, colorBlue);

            Row zeile = table.AddRow();
            GesamtErgebnis(zeile, Convert.ToInt32(mannschaftskampfViewModel.HeimPunkte), Convert.ToInt32(mannschaftskampfViewModel.GastPunkte), colorRed, colorBlue);

            return table;
        }

        private Table SetUpKampftabelle(double randLinksRechts)
        {
            Table table = new Table();

            table.Style = CustomStyles.TABLE;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.25;
            table.Borders.Right.Width = 0.25;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            double a4LandscapeBreite = (29.7 - 2 * randLinksRechts);

            double breite_KampfNr = 0.7;
            double breite_Stilart = 1.5;
            double breite_Gewichtsklasse = 1.0;
            double breite_IstGewicht = 1.1;
            double breite_PassNr = 1.1;
            double breite_Status = 1.2;
            double breite_Punkte = 1.0;
            double breite_Siegart = 2.8;
            double breite_Wertungen = 3.2;

            double nochUebrig = a4LandscapeBreite - breite_KampfNr - breite_Stilart - breite_Gewichtsklasse - 2 * breite_IstGewicht -
                                2 * breite_PassNr - 2 * breite_Status - 2 * breite_Punkte - breite_Siegart - breite_Wertungen;
            double breite_Name = nochUebrig / 2;

            Column column = table.AddColumn($"{breite_KampfNr}cm");
            column.Format.Alignment = ParagraphAlignment.Right;
            column = table.AddColumn($"{breite_Stilart}cm");
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

            return table;
        }
        private void MannschaftenUeberschrift(Row mannschaften, MannschaftskampfViewModel mannschaftskampfViewModel)
        {
            mannschaften.Format.Font.Bold = true;
            mannschaften.Borders.Visible = false;

            mannschaften.Cells[2].AddParagraph(mannschaftskampfViewModel.HeimMannschaft);
            mannschaften.Cells[2].Format.Font.Bold = true;
            mannschaften.Cells[2].Format.Font.Size = CustomStyles.fontSizeGross;
            mannschaften.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            mannschaften.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            mannschaften.Cells[2].MergeRight = 4;
            mannschaften.Cells[2].Borders.Right.Width = 2;

            mannschaften.Cells[7].AddParagraph(mannschaftskampfViewModel.GastMannschaft);
            mannschaften.Cells[7].Format.Font.Bold = true;
            mannschaften.Cells[7].Format.Font.Size = CustomStyles.fontSizeGross;
            mannschaften.Cells[7].Format.Alignment = ParagraphAlignment.Left;
            mannschaften.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            mannschaften.Cells[7].MergeRight = 4;
            mannschaften.Cells[7].Borders.Right.Width = 1;
        }

        private void Kopfzeilen(Row kopfzeile,Color colorRed,Color colorBlue)
        {
            SetUpKopfzeile(kopfzeile);

            AddKopfSpalte(kopfzeile, "Nr."); //TODO: In Dict auslagern
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlingStyle);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WeightClass, 1);

            var spalteRot = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.ActualWeightShort);
            spalteRot.Shading.Color = colorRed;
            spalteRot = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerName);
            spalteRot.Shading.Color = colorRed;
            spalteRot = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerId);
            spalteRot.Shading.Color = colorRed;
            spalteRot = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerStatus);
            spalteRot.Shading.Color = colorRed;
            spalteRot = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.TeamPointsShort, 2);
            spalteRot.Shading.Color = colorRed;

            var spalteBlau = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.ActualWeightShort);
            spalteBlau.Shading.Color = colorBlue;

            spalteBlau = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerName);
            spalteBlau.Shading.Color = colorBlue;
            spalteBlau = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerId);
            spalteBlau.Shading.Color = colorBlue;
            spalteBlau = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.WrestlerStatus);
            spalteBlau.Shading.Color = colorBlue;
            spalteBlau = AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.TeamPointsShort, 1);
            spalteBlau.Shading.Color = colorBlue;

            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.VictoryType);
            AddKopfSpalte(kopfzeile, Resources.LanguageFiles.DictPluginMain.PdfProtocolSinglePoints);

        }

        private static void SetUpKopfzeile(Row kopfzeile)
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

        private void Kampfzeilen(Table table, List<EinzelkampfViewModel> kaempfe, Color colorRed, Color colorBlue)
        {
            int cnt = 0;
            foreach (var kampf in kaempfe)
            {
                Row zeile = table.AddRow();
                InhaltKampfzeile(zeile, kampf, colorRed, colorBlue);

                if (cnt % 2 != 0)
                {
                    zeile.Shading.Color = Colors.LightGray;
                }
                cnt++;
                _kampfSpaltenCounter = 0;
            }
        }


        private void InhaltKampfzeile(Row zeile, EinzelkampfViewModel kampf, Color colorRed, Color colorBlue)
        {
            AddKampfSpalte(zeile, $"{kampf.KampfNr}");

            AddKampfSpalte(zeile, ((BoutSettings.WrestleStyles)kampf.WrestleStyle).AsString(EnumFormat.Description));
            var weightClass = AddKampfSpalte(zeile, kampf.WeightClass);
            weightClass.Borders.Right.Width = 1;
            weightClass.Borders.Right.Color = colorRed;

            AddKampfSpalte(zeile, kampf.IsNoHomeWrestler() ? "--" : kampf.HomeWrestlerWeight.ToString("0.0")).Borders.Color = colorRed;
            var homeWrestlerSpalte = AddKampfSpalte(zeile, kampf.IsNoHomeWrestler() ? "--" : kampf.HomeWrestlerFullname.Trim());
            homeWrestlerSpalte.Borders.Color = colorRed;
            if (kampf.HomeWrestlerPoints > kampf.OpponentWrestlerPoints)
            {
                homeWrestlerSpalte.Format.Font.Bold = true;
                homeWrestlerSpalte.Format.Font.Color = colorRed;
            }

            AddKampfSpalte(zeile, kampf.HomeWrestlerId > 0 ? kampf.HomeWrestlerId.ToString() : "--").Borders.Color = colorRed;
            AddKampfSpalte(zeile, kampf.HomeWrestlerStatus ?? string.Empty).Borders.Color = colorRed;
            var heimPunkte = AddKampfSpalte(zeile, kampf.HomeWrestlerPoints > 0 ? kampf.HomeWrestlerPoints.ToString() : "0", 2);
            heimPunkte.Borders.Color = colorRed;
            if (kampf.HomeWrestlerPoints > kampf.OpponentWrestlerPoints)
            {
                heimPunkte.Format.Font.Color = colorRed;
            }

            AddKampfSpalte(zeile, kampf.IsNoOpponentWrestler() ? "--" : kampf.OpponentWrestlerWeight.ToString("0.0")).Borders.Color = colorBlue;
            var opponentWrestlerSpalte = AddKampfSpalte(zeile, kampf.IsNoOpponentWrestler() ? "--" : kampf.OpponentWrestlerFullname.Trim());
            opponentWrestlerSpalte.Borders.Color = colorBlue;
            if (kampf.OpponentWrestlerPoints > kampf.HomeWrestlerPoints)
            {
                opponentWrestlerSpalte.Format.Font.Bold = true;
                opponentWrestlerSpalte.Format.Font.Color = colorBlue;
            }

            AddKampfSpalte(zeile, kampf.OpponentWrestlerId > 0 ? kampf.OpponentWrestlerId.ToString() : "--").Borders.Color = colorBlue;
            AddKampfSpalte(zeile, kampf.OpponentWrestlerStatus ?? string.Empty).Borders.Color = colorBlue;

            var gastPunkte = AddKampfSpalte(zeile, kampf.OpponentWrestlerPoints > 0 ? kampf.OpponentWrestlerPoints.ToString() : "0");
            gastPunkte.Borders.Color = colorBlue;
            gastPunkte.Borders.Right.Width = 1;
            gastPunkte.Borders.Right.Color = colorBlue;

            if (kampf.OpponentWrestlerPoints > kampf.HomeWrestlerPoints)
            {
                gastPunkte.Format.Font.Color = colorBlue;
            }

            var kampfzeit = TimeSpan.FromSeconds(kampf.Settings.Times[Ringen.Core.CS.BoutTime.Types.Bout.ToString()].Time).ToString("m':'ss");
            AddKampfSpalte(zeile, string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolResult, kampf.Result, kampfzeit));

            var spalte = zeile.Cells[_kampfSpaltenCounter];
            _kampfSpaltenCounter++;
            spalte.Borders.Color = Colors.Black;


            Paragraph punkteParagraph = spalte.AddParagraph();

            foreach (Core.CS.BoutPoint punkt in kampf.Points)
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

        private void GesamtErgebnis(Row zeile, int homePoints, int opponentPoints, Color colorRed, Color colorBlue)
        {
            zeile.Borders.Visible = false;

            zeile.Cells[6].AddParagraph($"{homePoints}");
            zeile.Cells[6].Format.Font.Bold = true;
            zeile.Cells[6].Format.Font.Size = CustomStyles.fontSizeGross;
            zeile.Cells[6].Borders.Visible = true;
            zeile.Cells[6].Borders.Top.Width = 1.5;
            zeile.Cells[6].Borders.Color = colorRed;
            if (homePoints > opponentPoints)
            {
                zeile.Cells[6].Format.Font.Color = colorRed;
            }

            zeile.Cells[12].AddParagraph($"{opponentPoints}");
            zeile.Cells[12].Format.Font.Bold = true;
            zeile.Cells[12].Format.Font.Size = CustomStyles.fontSizeGross;
            zeile.Cells[12].Borders.Visible = true;
            zeile.Cells[12].Borders.Top.Width = 1.5;
            zeile.Cells[12].Borders.Color = colorBlue;
            if (opponentPoints > homePoints)
            {
                zeile.Cells[12].Format.Font.Color = colorBlue;
            }

        }
    }
}