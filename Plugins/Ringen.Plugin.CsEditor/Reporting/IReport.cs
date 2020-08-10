using Ringen.Core.CS;

namespace Ringen.Plugin.CsEditor.Reporting
{
    public interface IReport
    {
        void Export(string pfad, Competition daten);
    }
}
