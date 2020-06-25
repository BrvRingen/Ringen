using Ringen.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Plugin.CsEditor
{
    public class Protocol
    {

        public static async Task OnCreateProtocolAsync(Core.CS.Competition Competition)
        {
            var myTask = Task.Run(() =>
            {
                LoggerMessage.Send(new LogEntry(LogEntryType.Message, Competition.Value));

                foreach (Core.CS.Bout Bout in Competition.Children)
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
