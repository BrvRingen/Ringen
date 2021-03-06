﻿using MigraDoc.DocumentObjectModel;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Reporting.BaseReport;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportPdf : IReport
    {
        private ExportPdf exportPdf = new ExportPdf();
        private BaseReportCreator baseReportCreator = new BaseReportCreator();

        public void Export(string pfad, MannschaftskampfViewModel daten, CompetitionInfosViewModel zusatzInfos)
        {
            Document report = baseReportCreator.ErstelleBericht(daten, zusatzInfos, false);
            exportPdf.Export(pfad, report);
        }
    }
}
