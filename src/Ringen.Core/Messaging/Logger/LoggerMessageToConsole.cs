using System;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Ringen.Core.Messaging
{
    public class LogToConsole : MessageBase
    {
        public LogToConsole()
        {
            // Messaging
            Messenger.Default.Register<LoggerMessage>(this, LoggerMessageRecieved);
        }

        private void LoggerMessageRecieved(LoggerMessage obj)
        {
            // Ausbage: Console
            Console.WriteLine(obj.LogEntry.Message);
        }
    }
}
