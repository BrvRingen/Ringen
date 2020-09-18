using MigraDoc.DocumentObjectModel;
using Ringen.Plugin.CsEditor.Reporting.Konfig;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class DefaultElemente
    {
        public Document GetDocument(string title)
        {
            Document document = new Document()
            {
                Info =
                {
                    Title =title
                }
            };
            CustomStyles.Definiere(document);
            return document;
        }

        public Section GetHauptSection(double randLinksRechts)
        {
            Section section = new Section();
            SetUpPage(section, randLinksRechts);
            return section;
        }

        public Paragraph GetUeberschrift(string uberschrift)
        {
            Paragraph header = new Paragraph();
            header.AddFormattedText(uberschrift, TextFormat.Underline);
            header.Format.Font.Size = CustomStyles.fontSizeGross;
            header.Format.Alignment = ParagraphAlignment.Left;
            header.Format.Font.Bold = true;
            header.Format.Font.Name = CustomStyles.fontUeberschriften;

            return header;
        }

        private void SetUpPage(Section section, double randLinksRechts)
        {
            var defaultPageSetup = GetDocument("").DefaultPageSetup;
            section.PageSetup = defaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.LeftMargin = $"{randLinksRechts}cm";
            section.PageSetup.RightMargin = $"{randLinksRechts}cm";
            section.PageSetup.HeaderDistance = "1.2cm";
            section.PageSetup.FooterDistance = "0.5cm";
            //section.PageSetup.TopMargin = 0;
        }
    }
}
