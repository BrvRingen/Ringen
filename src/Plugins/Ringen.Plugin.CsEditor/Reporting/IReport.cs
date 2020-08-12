using Ringen.Core.CS;
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public interface IReport
    {
        void Export(string pfad, Competition daten, CompetitionInfos zusatzInfos);
    }
}
