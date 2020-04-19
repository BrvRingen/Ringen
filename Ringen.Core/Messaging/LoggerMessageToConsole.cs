using System;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Ringen.Core.Messaging
{
    public class LogToConsole : MessageBase
    {
        #region declarations

        #endregion

        #region constructors

        public LogToConsole()
        {
            // Messaging
            Messenger.Default.Register<LoggerMessage>(this, LoggerMessageRecieved);
        }

        #endregion

        private void LoggerMessageRecieved(LoggerMessage obj)
        {
            // Ausbage: Console
            Console.WriteLine(obj.LogEntry.Message);
        }
    }
}
