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
            
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Args = new List<string>();
            if (e.Args.Length > 0)
            {
                Args = e.Args.ToList();
            }

            var notificationIcon = RingenResourceManager.GetResource<TaskbarIcon>("NotificationIcon");
            notificationIcon.DataContext = ViewModel.ViewModelLocator.GetViewModel<ViewModel.MainViewModel>(nameof(ViewModel.MainViewModel));

            // View starten
            var mainWindow = new View.MainWindow();
            mainWindow.Show();
        }

    }
}
