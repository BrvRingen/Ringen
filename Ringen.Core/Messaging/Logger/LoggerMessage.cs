using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Ringen.Core.Messaging
{
    public class LoggerMessage : MessageBase
    {
        #region declarations

        public LogEntry LogEntry { get; }

        #endregion

        #region constructors

        private LoggerMessage(LogEntry _logEntry)
        {
            LogEntry = _logEntry;
        }

        #endregion

        #region constructors

        public static void Send(LogEntry _logEntry)
        {
            Messenger.Default.Send(new LoggerMessage(_logEntry));
        }

        #endregion

    }
    #region classes

    public enum LogEntryType
    {
        Debug,
        Error,
        Warning,
        Message,
        Success
    };

    public class LogEntry
    {
        #region declarations
        public LogEntryType Type { get; } = LogEntryType.Message;
        public DateTime Time { get; }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }



        #endregion

        #region constructors

        public LogEntry(LogEntryType _type, string _text)
        {
            Type = _type;
            Message = _text;
            Time = DateTime.Now;
        }

        #endregion

        #region properties

        #endregion

        #region protected functions


        #endregion

        #region public functions

        public override string ToString()
        {
            return $"{Time} -> {Type} -> {Message}";
        }

        #endregion
    }

    #endregion
}
