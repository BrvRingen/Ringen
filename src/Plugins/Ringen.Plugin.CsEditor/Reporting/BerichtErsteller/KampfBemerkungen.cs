using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using Ringen.Core.Messaging;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using System;
using System.Linq;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class KampfBemerkungen
    {
        public TextFrame generate(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos, double randLinksRechts, bool useColor)
        {
            var result = new TextFrame();
            result.Width = "25cm";

            var table = result.AddTable();
            table.Borders.Visible = false;
            table.TopPadding = "0.5cm";

            double a4LandscapeBreite = (29.7 - 2 * randLinksRechts);

            double breite_Sieger = 8;
            double breite_Wettkampf = 6;
            double breite_Orga = 6;
            double breite_Kommentar = a4LandscapeBreite - breite_Orga - breite_Wettkampf - breite_Sieger;

            Column column = table.AddColumn($"{breite_Sieger}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Wettkampf}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Orga}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{breite_Kommentar}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            Row zeile = table.AddRow();
            zeile.Style = CustomStyles.BEMERKUNG;

            zeile.Cells[0].AddParagraph().AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Winner}:", TextFormat.Bold);
            zeile.Cells[0].Add(SiegerTeam(mannschaftskampfViewModel.HeimMannschaft, Convert.ToInt32(mannschaftskampfViewModel.HeimPunkte), mannschaftskampfViewModel.GastMannschaft, Convert.ToInt32(mannschaftskampfViewModel.GastPunkte), useColor));

            zeile.Cells[1].Add(Wettkampf(mannschaftskampfViewModel));

            zeile.Cells[2].Add(Organisation(zusatzInfos));

            zeile.Cells[3].Add(Kommentar(mannschaftskampfViewModel.Kommentar));

            return result;
            
        }
        private Paragraph SiegerTeam(string homeTeamName, int homePoints, string opponentTeamName, int opponentPoints, bool useColor)
        {
            Paragraph siegParagraph = new Paragraph();

            string sieger = Resources.LanguageFiles.DictPluginMain.Undetermined;

            var colorText = Colors.Black;
            if (homePoints > opponentPoints)
            {
                sieger = $"{homeTeamName}";
                colorText = CustomStyles.ROT;
            }
            else if (homePoints < opponentPoints)
            {
                sieger = $"{opponentTeamName}";
                colorText = CustomStyles.BLAU;
            }
            else if (homePoints == opponentPoints)
            {
                sieger = $"{Resources.LanguageFiles.DictPluginMain.Tie}";
            }

            if (!useColor)
            {
                colorText = Colors.Black;
            }

            siegParagraph.AddFormattedText(string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolWinnerLine, sieger, homePoints, opponentPoints), TextFormat.Underline);
            siegParagraph.Format.Font.Size = CustomStyles.fontSizeGross;
            siegParagraph.Format.Font.Bold = true;
            siegParagraph.Format.Font.Color = colorText;

            return siegParagraph;
        }

        private Paragraph Wettkampf(MannschaftskampfViewModel mannschaftskampfViewModel)
        {
            Paragraph paragraph = new Paragraph();

            var ft = paragraph.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Bout}:", TextFormat.Bold);
            ft.Underline = Underline.Single;

            paragraph.AddLineBreak();
            paragraph.AddFormattedText("Geplanter Beginn: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddFormattedText($"{mannschaftskampfViewModel.GeplanterKampfbeginn:hh':'mm} Uhr");
            paragraph.AddLineBreak();

            DateTime ersterKampf = DateTime.Now;
            DateTime letzterKampf = DateTime.Now;
            try
            {
                ersterKampf = mannschaftskampfViewModel.Children.Min(li => li.Points.Min(na => na.Zeit.Value));
                letzterKampf = mannschaftskampfViewModel.Children.Max(li => li.Points.Max(na => na.Zeit.Value));
            }
            catch (Exception)
            {
                //TODO: Kampfzeiten konnten nicht ermittelt werden, weil na.Zeit == null war.
                LoggerMessage.Send(new LogEntry(LogEntryType.Error, "Kampfzeiten konnten nicht ermittelt werden"));
            }

            paragraph.AddFormattedText("Erster Kampf: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddTab();
            paragraph.AddFormattedText($"{ersterKampf.ToShortTimeString()} Uhr");
            paragraph.AddLineBreak();

            paragraph.AddFormattedText("Letzter Kampf: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddTab();
            paragraph.AddFormattedText($"{letzterKampf.ToShortTimeString()} Uhr");
            paragraph.AddLineBreak();

            paragraph.AddFormattedText("Anzahl Zuschauer: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddFormattedText($"{mannschaftskampfViewModel.AnzahlZuschauer}");
            paragraph.AddLineBreak();

            return paragraph;
        }
        private Paragraph Organisation(CompetitionInfosViewModel zusatzInfos)
        {
            Paragraph paragraph = new Paragraph();

            var ft = paragraph.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Organization}:", TextFormat.Bold);
            ft.Underline = Underline.Single;
            paragraph.AddLineBreak();

            paragraph.AddFormattedText("Protokoll: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText(zusatzInfos.Protokollfuehrer);
            paragraph.AddLineBreak();

            paragraph.AddFormattedText("Punktzettel: ", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText(zusatzInfos.ErgebnislistenSchreiber);

            foreach (var ordner in zusatzInfos.Ordner)
            {
                paragraph.AddLineBreak();
                paragraph.AddFormattedText("Ordner: ", TextFormat.Bold);
                paragraph.AddTab();
                paragraph.AddText(ordner);
            }

            return paragraph;
        }

        private Paragraph Kommentar(string kommentar)
        {
            Paragraph kommentarParagraph = new Paragraph();

            var ft = kommentarParagraph.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.EditorComment}:", TextFormat.Bold);
            ft.Underline = Underline.Single;
            kommentarParagraph.AddLineBreak();
            kommentarParagraph.AddText(kommentar);

            return kommentarParagraph;
        }

    }
}
