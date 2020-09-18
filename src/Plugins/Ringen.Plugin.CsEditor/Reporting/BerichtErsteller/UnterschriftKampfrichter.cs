using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Ringen.Plugin.CsEditor.Reporting.Konfig;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class UnterschriftKampfrichter
    {
        public Table generate(string schiedsrichter, string trainerHeim, string trainerGast, double _randLinksRechts)
        {
            Table table = new Table();
            table.Style = CustomStyles.TABLE;
            table.Borders.Visible = false;

            double abstand = 1;
            double a4LandscapeBreite = (29.7 - 2 * _randLinksRechts - 2 * abstand);
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
        
    }
}
