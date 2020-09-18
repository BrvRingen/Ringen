using MigraDoc.DocumentObjectModel;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Reporting.BaseReport;
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportFarbigPdf : IReport
    {
        private ExportPdf exportPdf = new ExportPdf();
        private BaseReportCreator baseReportCreator = new BaseReportCreator();

        public void Export(string pfad, Competition daten, CompetitionInfos zusatzInfos)
        {
            Document report = baseReportCreator.ErstelleBericht(daten, zusatzInfos, true);
            exportPdf.Export(pfad, report);
        }
    }
}