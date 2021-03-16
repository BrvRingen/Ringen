using Hardcodet.Wpf.TaskbarNotification;
using Ringen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Ringen.Core.TranslationManager;
using Ringen.Core.Services;
using Ringen.DependencyInjection;

namespace Ringen
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        public static List<string> Args { get; set; }
        public App()
        {
            ServiceBasic.Register(typeof(IRingenService), typeof(RingenService));
            Service.Plugin.InitializeSystem();
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Args = new List<string>();

            if (e.Args.Length > 0)
            {
                Args = e.Args.ToList();
            }

            DependencyInjectionContainer.CreateKernel();

            // Prüfe .Net-Framework (theoretisch mit MSI Installer nicht mehr nötig)
            Version dotNetFrameworkVersion = Version.Parse(System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).ProductVersion.ToString().Substring(0, 5));
            if (dotNetFrameworkVersion < Version.Parse("4.7.2"))
                throw new System.ArgumentException(string.Format("wrong .Net-Framework installed ({0})", dotNetFrameworkVersion.ToString()));

            // lokale Wörterbucher laden
            TransManager.Instance.AddTranslationResource(ResourcesEnum.Ringen_DictMainForm, "Ringen.Resources.LanguageFiles.DictMainForm", Assembly.GetExecutingAssembly());

            // Logger starten (trick, da sonst wird er erst geladen, nachdem die MainViewModel Steht)
            //var logger = ViewModelLocator.LoggerViewModel;

            // Resourcen laden
            RingenResourceManager.AddResource("Resources/NotificationIcon/NotificationIconResources.xaml", Assembly.GetExecutingAssembly());

            var notificationIcon = RingenResourceManager.GetResource<TaskbarIcon>("NotificationIcon");
            notificationIcon.DataContext = ViewModel.ViewModelLocator.GetViewModel<ViewModel.MainViewModel>(nameof(ViewModel.MainViewModel));

            // View starten
            var mainWindow = new View.MainWindow();
            mainWindow.Show();

        }

    }
}
