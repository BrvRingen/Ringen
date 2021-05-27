using Hardcodet.Wpf.TaskbarNotification;
using Ringen.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

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
