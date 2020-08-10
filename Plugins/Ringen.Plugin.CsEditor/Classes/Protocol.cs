using System.Diagnostics;
using Ringen.Core.Messaging;
using System.Threading.Tasks;
using Ringen.Plugin.CsEditor.Reporting;

namespace Ringen.Plugin.CsEditor
{
    public class Protocol
    {
        public static async Task OnCreateProtocolAsync(Core.CS.Competition competition)
        {
            var myTask = Task.Run(() =>
            {
                string filename = $"{competition.BoutDate}_{competition.HomeTeamName.Replace(' ', '-')}_vs_{competition.OpponentTeamName.Replace(' ', '-')}.pdf";

                
                IReport bericht = new ReportPdf();
                bericht.Export(filename, competition);
                Process.Start(filename);//Öffne PDF

                LoggerMessage.Send(new LogEntry(LogEntryType.Message, competition.Value));

                foreach (Core.CS.Bout Bout in competition.Children)
                {
                    LoggerMessage.Send(new LogEntry(LogEntryType.Message, Bout.Value));
                }

                return;
            });
            await myTask;

            return;
        }
    }
}
