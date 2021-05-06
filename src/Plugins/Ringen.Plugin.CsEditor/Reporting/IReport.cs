using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public interface IReport
    {
        void Export(string pfad, MannschaftskampfViewModel daten, CompetitionInfosViewModel zusatzInfos);
    }
}
