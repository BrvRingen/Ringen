using MigraDoc.DocumentObjectModel;
using Ringen.Core.ViewModels;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.BerichtErsteller;


namespace Ringen.Plugin.CsEditor.Reporting.BaseReport
{
    public class BaseReportCreator
    {

        private const double _randLinksRechts = 1.2;
        private DefaultElemente defaultElemente = new DefaultElemente();
        private BerichtTitel berichtTitel = new BerichtTitel();
        private KampfInformationen kampfInformationen = new KampfInformationen();
        private KampfBemerkungen kampfBemerkungen = new KampfBemerkungen();
        private HinweißVorläufigesErgebnis hinweißVorläufigesErgebnis = new HinweißVorläufigesErgebnis();
        private UnterschriftKampfrichter unterschriftKampfrichter = new UnterschriftKampfrichter();
        private KampfTabelle kampfTabelle = new KampfTabelle();

        public Document ErstelleBericht(MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos, bool useColor)
        {
            string title = berichtTitel.GetTitle(mannschaftskampfViewModel);
            Document document = defaultElemente.GetDocument(title);
            Section hauptSection = defaultElemente.GetHauptSection(_randLinksRechts);

            ErstelleHauptSektion(hauptSection, mannschaftskampfViewModel, zusatzInfos, useColor);
            document.Add(hauptSection);

            return document;
        }

        private Section ErstelleHauptSektion(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos, bool useColor)
        {
            ErgaenzeHeader(section, mannschaftskampfViewModel, zusatzInfos);
            ErgaenzeInhalt(section, mannschaftskampfViewModel, zusatzInfos, useColor);
            ErgaenzeFooter(section, mannschaftskampfViewModel, zusatzInfos.MannschaftsfuehrerHeim, zusatzInfos.MannschaftsfuehrerGast);

            return section;
        }

        private void ErgaenzeInhalt(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos, bool useColor)
        {
            section.Add(PdfHelper.AddAbstandNachOben("0.8cm"));
            section.Add(kampfTabelle.generate(mannschaftskampfViewModel, _randLinksRechts, useColor));
            section.Add(kampfBemerkungen.generate(mannschaftskampfViewModel, zusatzInfos, _randLinksRechts, useColor));
        }

        private void ErgaenzeHeader(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, CompetitionInfosViewModel zusatzInfos)
        {
            section.Headers.Primary.Add(defaultElemente.GetUeberschrift(Resources.LanguageFiles.DictPluginMain.PdfProtocolHeadline));
            section.Add(kampfInformationen.generate(mannschaftskampfViewModel, zusatzInfos, _randLinksRechts));
        }

        private void ErgaenzeFooter(Section section, MannschaftskampfViewModel mannschaftskampfViewModel, string trainerHeim, string trainerGast)
        {
            section.Footers.Primary.Add(unterschriftKampfrichter.generate(mannschaftskampfViewModel.Schiedsrichter_Vorname, trainerHeim, trainerGast, _randLinksRechts));
            section.Footers.Primary.Add(hinweißVorläufigesErgebnis.generate());
        }
    }
}
