using MigraDoc.DocumentObjectModel;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.BerichtErsteller;
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public class ReportErgebnislisteKampfrichtertischPdf : IReport
    {
        private const double _randLinksRechts = 0.8;

        private ExportPdf exportPdf = new ExportPdf();
        private DefaultElemente defaultElemente = new DefaultElemente();
        private KampfInformationen kampfInformationen = new KampfInformationen();
        private KampfTabelleKampfrichter kampfTabelleKampfrichter = new KampfTabelleKampfrichter();

        public void Export(string pfad, MannschaftskampfViewModel daten, CompetitionInfos zusatzInfos)
        {
            Document report = ErstelleBericht(daten, zusatzInfos);
            exportPdf.Export(pfad, report);
        }

        public Document ErstelleBericht(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            string title = "";
            //TODO kläre warum ein Dokument einen Titel hat und diese nicht, was ist der default wert?
            Document document = defaultElemente.GetDocument(title);
            Section hauptSection = defaultElemente.GetHauptSection(_randLinksRechts);

            ErstelleHauptSektion(hauptSection, mannschaftskampfViewModel, zusatzInfos);
            document.Add(hauptSection);

            return document;
        }


        private Section ErstelleHauptSektion(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            ErgaenzeHeader(section, mannschaftskampfViewModel, zusatzInfos);
            ErgaenzeInhalt(section, mannschaftskampfViewModel, zusatzInfos);

            return section;
        }

        private void ErgaenzeInhalt(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            section.Add(kampfTabelleKampfrichter.generate(mannschaftskampfViewModel, _randLinksRechts));
        }

        private void ErgaenzeHeader(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfos zusatzInfos)
        {
            section.Headers.Primary.Add(defaultElemente.GetUeberschrift("Ergebnisliste für Kampfrichtertisch"));
            section.Add(kampfInformationen.generate(mannschaftskampfViewModel, zusatzInfos, _randLinksRechts));

            section.Headers.Primary.Add(PdfHelper.AddAbstandNachOben("0.1cm"));
        }
    }
}
