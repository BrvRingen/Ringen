using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.ServiceProcess;
using System.DirectoryServices.ActiveDirectory;
using System.Net;

namespace Ringen
{
    public class Startup
    {
        private static int exitCode;

        [STAThread]
        public static int Main(string[] args)
        {
            foreach (string arg in args)
            {
                // Remote Debugging
                if (arg.ToLower() == "/remote")
                {
                    ConsoleManager.Show();

                    //Process Open = new Process();
                    //Open.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"RemoteDebugger\msvsmon.exe";
                    //Open.StartInfo.Arguments = @"";
                    //Open.Start();

                    Console.WriteLine("Waiting for debugger to attach");

                    while (!Debugger.IsAttached)
                    {
                        Thread.Sleep(100);
                    }

                    Console.WriteLine("Debugger attached");
                }
                else if (arg.ToLower() == "/debug")
                {
                    ConsoleManager.Show();
                }
            }

            // Sprache festlegen, falls es fehlt.
            if (string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                if (CultureInfo.CurrentUICulture.IetfLanguageTag == "de-DE")
                    Properties.Settings.Default.Language = "de-DE";
                else
                    Properties.Settings.Default.Language = "en-US";

                Properties.Settings.Default.Save();
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(Properties.Settings.Default.Language);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            // Argumente prüfen auf Hilfe
            if (args.Length == 1 && args[0] == "/?")
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Application.GetResourceStream(new Uri("/Ringen;component/resources/help/terminal_help." + Properties.Settings.Default.Language.ToLower() + ".txt", UriKind.Relative)).Stream);
                MessageBox.Show(sr.ReadToEnd());
                return 0;
            }


            (new App()).Run();

            return ExitCode;
        }

        public static int ExitCode
        {
            get
            {
                return exitCode;
            }
            set
            {
                exitCode = value;
            }
        }
    }
}
