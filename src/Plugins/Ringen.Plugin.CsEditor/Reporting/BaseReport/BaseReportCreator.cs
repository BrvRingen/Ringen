using MigraDoc.DocumentObjectModel;
using Ringen.Core.CS;
using Ringen.Plugin.CsEditor.Helper;
using Ringen.Plugin.CsEditor.Reporting.BerichtErsteller;
using Ringen.Shared.Models;


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

        public Document ErstelleBericht(Competition competition, CompetitionInfos zusatzInfos, bool useColor)
        {
            string title = berichtTitel.GetTitle(competition);
            Document document = defaultElemente.GetDocument(title);
            Section hauptSection = defaultElemente.GetHauptSection(_randLinksRechts);

            ErstelleHauptSektion(hauptSection, competition, zusatzInfos, useColor);
            document.Add(hauptSection);

            return document;
        }

        private Section ErstelleHauptSektion(Section section, Competition competition, CompetitionInfos zusatzInfos, bool useColor)
        {
            ErgaenzeHeader(section, competition, zusatzInfos);
            ErgaenzeInhalt(section, competition, zusatzInfos, useColor);
            ErgaenzeFooter(section, competition, zusatzInfos.MannschaftsfuehrerHeim, zusatzInfos.MannschaftsfuehrerGast);

            return section;
        }

        private void ErgaenzeInhalt(Section section, Competition competition, CompetitionInfos zusatzInfos, bool useColor)
        {
            section.Add(PdfHelper.AddAbstandNachOben("0.8cm"));
            section.Add(kampfTabelle.generate(competition, _randLinksRechts, useColor));
            section.Add(kampfBemerkungen.generate(competition, zusatzInfos, _randLinksRechts, useColor));
        }

        private void ErgaenzeHeader(Section section, Competition competition, CompetitionInfos zusatzInfos)
        {
            section.Headers.Primary.Add(defaultElemente.GetUeberschrift(Resources.LanguageFiles.DictPluginMain.PdfProtocolHeadline));
            section.Add(kampfInformationen.generate(competition, zusatzInfos, _randLinksRechts));
        }

        private void ErgaenzeFooter(Section section, Competition competition, string trainerHeim, string trainerGast)
        {
            section.Footers.Primary.Add(unterschriftKampfrichter.generate(competition.Referee, trainerHeim, trainerGast, _randLinksRechts));
            section.Footers.Primary.Add(hinweißVorläufigesErgebnis.generate());
        }
    }
}
