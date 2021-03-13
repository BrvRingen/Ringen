using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Ringen.Core.Messaging
{
    public class LoggerMessage : MessageBase
    {
        public LogEntry LogEntry { get; }

        private LoggerMessage(LogEntry _logEntry)
        {
            LogEntry = _logEntry;
        }

        public static void Send(LogEntry _logEntry)
        {
            Messenger.Default.Send(new LoggerMessage(_logEntry));
        }
    }

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
        public LogEntryType Type { get; } = LogEntryType.Message;
        public DateTime Time { get; }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }


        public LogEntry(LogEntryType _type, string _text)
        {
            Type = _type;
            Message = _text;
            Time = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Time} -> {Type} -> {Message}";
        }
    }
}
