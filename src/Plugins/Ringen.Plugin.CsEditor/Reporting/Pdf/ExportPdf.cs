using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace Ringen.Plugin.CsEditor.Reporting
{
    class ExportPdf
    {
        public void Export(string path, Document report)
        {
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = report;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(path);
        }
    }
}
