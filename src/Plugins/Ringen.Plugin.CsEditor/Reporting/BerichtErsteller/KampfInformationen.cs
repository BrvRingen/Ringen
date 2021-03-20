using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using Ringen.Shared.Models;
using System;
using System.Linq;
using Ringen.Core.ViewModels;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class KampfInformationen
    {
        public Table generate(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos, double randLinksRechts)
        {
            var table = new Table();
            table.Style = CustomStyles.TABLEINFO;

            table.Borders.Visible = false;

            double a4LandscapeBreite = (29.7 - 2 * randLinksRechts) / 3.0;

            Column column = table.AddColumn($"{a4LandscapeBreite - 2}cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn($"{a4LandscapeBreite + 4}cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn($"{a4LandscapeBreite - 2}cm");
            column.Format.Alignment = ParagraphAlignment.Right;


            Row zeile = table.AddRow();

            var spalte = zeile.Cells[0].AddParagraph();
            var ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.LigaId} {mannschaftskampfViewModel.TableId}", TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;
            spalte.AddLineBreak();

            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.Location}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            ft.Bold = true;

            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.Wettkampfstaette.Split(' ').Last()}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            spalte = zeile.Cells[1].AddParagraph();
            ft = spalte.AddFormattedText(zusatzInfos.Kampfart, TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;
            spalte.AddLineBreak();

            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.LocationAddress}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            ft.Bold = true;

            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.Wettkampfstaette}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            spalte = zeile.Cells[2].AddParagraph();
            ft = spalte.AddFormattedText(zusatzInfos.VorKampfRueckKampf, TextFormat.Bold);
            ft.Font.Size = CustomStyles.fontSizeGross;

            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{Resources.LanguageFiles.DictPluginMain.BoutDate}:");
            ft.Font.Size = CustomStyles.fontSizeKlein;
            ft.Bold = true;

            spalte.AddLineBreak();
            ft = spalte.AddFormattedText($"{mannschaftskampfViewModel.Kampfdatum.ToShortDateString()}");
            ft.Font.Size = CustomStyles.fontSizeNormal;

            return table;
        }

    }
}
