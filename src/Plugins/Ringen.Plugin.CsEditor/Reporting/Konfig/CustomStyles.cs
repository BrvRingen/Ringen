using MigraDoc.DocumentObjectModel;

namespace Ringen.Plugin.CsEditor.Reporting.Konfig
{
    class CustomStyles
    {
        public const string TABLEINFO = "TableInfo";
        public const string TABLE = "Table";
        public const string WERTUNG_ROT = "WertungRot";
        public const string WERTUNG_BLAU = "WertungBlau";
        public const string BEMERKUNG = "Bemerkung";

        public const int fontSizeTitel = 22;
        public const int fontSizeSehrKlein = 6;
        public const int fontSizeKlein = 10;
        public const int fontSizeNormal = 12;
        public const int fontSizeGross = 16;

        internal static void Definiere(Document document)
        {
            Style style = document.Styles["Normal"];

            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Arial";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("4cm", TabAlignment.Center);

            style = document.Styles.AddStyle(TABLE, "Normal");
            style.Font.Name = "Times New Roman";
            style.Font.Size = fontSizeKlein;

            style = document.Styles.AddStyle(TABLEINFO, "Normal");
            style.Font.Name = "Arial";
            style.Font.Size = fontSizeNormal;

            style = document.Styles.AddStyle(WERTUNG_ROT, "Normal");
            style.Font.Color = Colors.Red;
            style.Font.Bold = true;
            style.Font.Underline = Underline.Single;

            style = document.Styles.AddStyle(WERTUNG_BLAU, "Normal");
            style.Font.Color = Colors.Blue;
            style.Font.Bold = true;
            style.Font.Italic = true;

            style = document.Styles.AddStyle(BEMERKUNG, "Normal");
            style.Font.Size = fontSizeKlein;
        }
    }
}
