using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public interface IReport
    {
        void Export(string pfad, MannschaftskampfViewModel daten, CompetitionInfos zusatzInfos);
    }
}
