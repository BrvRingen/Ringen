using MigraDoc.DocumentObjectModel;
using Ringen.Plugin.CsEditor.Reporting.Konfig;
using System;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class HinweißVorläufigesErgebnis
    {
        public Paragraph generate()
        {
            Paragraph footer = new Paragraph();
            footer.AddFormattedText(string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolFooterHint, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()).Replace("\\n", Environment.NewLine));
            footer.Format.Font.Size = CustomStyles.fontSizeExtremKlein;
            footer.Format.Alignment = ParagraphAlignment.Center;
            footer.Format.SpaceBefore = "0.5cm";

            return footer;
        }
    }
}
