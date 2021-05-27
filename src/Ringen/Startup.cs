using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Reflection;
using Ringen.Core;
using Ringen.Core.DependencyInjection;
using Ringen.Core.Services;
using Ringen.Core.TranslationManager;

namespace Ringen
{
    public class Startup
    {
        private static int exitCode;

        [STAThread]
        public static int Main(string[] args)
        {
            Lese_Eingabeparameter(args);
            Sprache_festlegen();
            Pruefe_NetFramework_Version();

            Init_DependencyInjection();
            Init_Schnittstellen();

            Lokale_Woerterbucher_laden();
            //Resourcen_laden();

            (new App()).Run();

            return ExitCode;
        }

        private static void Init_Schnittstellen()
        {
            //TODO
            //RdbErgebnisdienstConfigSection.
            //Ringen.Schnittstellen.RDB.Startup.Init(Erstelle_RdbSystemSettings());
        }

        //private static RdbSystemSettings Erstelle_RdbSystemSettings(RdbErgebnisdienstConfigSection configSection)
        //{
        //    RdbSystemSettings settings;
        //    settings.Credentials = new NetworkCredential(configSection.Credentials.Benutzername, PasswordHelper.DecryptString(configSection.Credentials.EnryptedPasswort));
        //    settings.BaseUrl = configSection.Api.Host;
        //    settings.JsonReaderService = new KeyValuePair<string, string>(configSection.Api.JsonReaderService.Key, configSection.Api.JsonReaderService.Value);
        //    settings.TaskCompetitionSystem = new KeyValuePair<string, string>(configSection.Api.TaskCompetitionSystem.Key, configSection.Api.TaskCompetitionSystem.Value);
        //    settings.TaskOrganisationsmanager = new KeyValuePair<string, string>(configSection.Api.TaskOrganisationsmanager.Key, configSection.Api.TaskOrganisationsmanager.Value);

        //    return settings;
        //}

        private static void Lokale_Woerterbucher_laden()
        {
            TransManager.Instance.AddTranslationResource(ResourcesEnum.Ringen_DictMainForm,
                "Ringen.Resources.LanguageFiles.DictMainForm", Assembly.GetExecutingAssembly());
        }

        private static void Init_DependencyInjection()
        {
            DependencyInjectionContainer.CreateKernel();

            ServiceBasic.Register(typeof(IRingenService), typeof(RingenService));
            Service.Plugin.InitializeSystem();
        }
        
        private static void Pruefe_NetFramework_Version()
        {
            Version dotNetFrameworkVersion = Version.Parse(System.Diagnostics.FileVersionInfo
                .GetVersionInfo(typeof(int).Assembly.Location).ProductVersion.ToString().Substring(0, 5));
            if (dotNetFrameworkVersion < Version.Parse("4.7.2"))
                throw new System.ArgumentException(string.Format("wrong .Net-Framework installed ({0})",
                    dotNetFrameworkVersion.ToString()));
        }

        private static void Sprache_festlegen()
        {
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
        }

        private static void Lese_Eingabeparameter(string[] args)
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

            // Argumente prüfen auf Hilfe
            if (args.Length == 1 && args[0] == "/?")
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Application.GetResourceStream(new Uri("/Ringen;component/resources/help/terminal_help." + Properties.Settings.Default.Language.ToLower() + ".txt", UriKind.Relative)).Stream);
                MessageBox.Show(sr.ReadToEnd());
                Thread.Sleep(10000);
                throw new Exception();
            }
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
