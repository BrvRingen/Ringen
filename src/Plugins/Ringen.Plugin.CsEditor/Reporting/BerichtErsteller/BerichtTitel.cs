using Ringen.Core.CS;
using System;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor.Reporting.BerichtErsteller
{
    class BerichtTitel
    {
        public string GetTitle(MannschaftskampfViewModel mannschaftskampfViewModel)
        {
            string title = string.Format(Resources.LanguageFiles.DictPluginMain.PdfProtocolTitle, mannschaftskampfViewModel.LigaId, mannschaftskampfViewModel.TableId, mannschaftskampfViewModel.HomeTeamName, mannschaftskampfViewModel.OpponentTeamName, DateTime.Parse(mannschaftskampfViewModel.BoutDate).ToShortDateString());
            return title;
        }
    }
}
