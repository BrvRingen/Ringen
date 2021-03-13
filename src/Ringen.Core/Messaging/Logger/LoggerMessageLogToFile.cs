using System;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Ringen.Core.Messaging
{
    public class LogToFile : MessageBase
    {
        private StreamWriter sw;

        public LogToFile(string _outputFile, int _maxLength)
        {
            // Variablen initialisieren
            if (!string.IsNullOrEmpty(_outputFile))
            {
                if (File.Exists(_outputFile))
                {
                    var logContents = File.ReadAllLines(_outputFile);

                    if (logContents.Length > _maxLength)
                    {
                        File.WriteAllLines(_outputFile, logContents.Skip(logContents.Length - _maxLength).ToArray());
                    }
                }

                if (!Directory.Exists(Directory.GetParent(_outputFile).FullName))
                    Directory.CreateDirectory(Directory.GetParent(_outputFile).FullName);

                sw = new StreamWriter(_outputFile, true);
            }

            // Messaging
            Messenger.Default.Register<LoggerMessage>(this, LoggerMessageRecieved);
        }

        private void LoggerMessageRecieved(LoggerMessage obj)
        {
            // Ausgabe: Datei
            if (sw != null)
            {
                sw.WriteLine(obj.LogEntry.ToString());
                sw.Flush();
            }

            // Ausbage: Console
            //Console.WriteLine(obj.LogEntry.ToString());
        }

        public void Dispose()
        {
            sw.Dispose();
        }
    }
}
