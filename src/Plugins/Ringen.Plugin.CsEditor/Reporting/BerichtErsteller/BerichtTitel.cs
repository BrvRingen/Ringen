using Ringen.Core.CS;
using System;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class BerichtTitel
    {
        public string GetTitle(Competition competition)
        {
            string title = string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolTitle, competition.LigaId, competition.TableId, competition.HomeTeamName, competition.OpponentTeamName, DateTime.Parse(competition.BoutDate).ToShortDateString());
            return title;
        }
    }
}
