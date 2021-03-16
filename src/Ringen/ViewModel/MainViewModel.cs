using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Ringen.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Ringen.Core.TranslationManager;
using Ringen.Core;
using Ringen.Core.UI;
using GalaSoft.MvvmLight;
using Ringen.Core.Services;
using System.Windows.Input;
using System.Collections.Specialized;
using AvalonDock;
using AvalonDock.Layout;
using Ringen.Core.ViewModels;

namespace Ringen.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {

        #region properties
        LogToFile logToFile;
        public new event PropertyChangedEventHandler PropertyChanged;

        private readonly HashSet<LogEntry> m_DataHashSet = new HashSet<LogEntry>();
        private static readonly object m_LockObj = new object();

        private ObservableCollection<LogEntry> logData;

        public ObservableCollection<LogEntry> LogData
        {
            get
            {
                if (logData == null)
                    logData = new ObservableCollection<LogEntry>();
                return logData;
            }
            set
            {
                logData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogData"));
            }
        }

        private ObservableCollection<IRingenTabItem> openedTabs;

        public ObservableCollection<IRingenTabItem> OpenedTabs
        {
            get
            {
                if (openedTabs == null)
                {
                    openedTabs = new ObservableCollection<IRingenTabItem>();
                }
                return openedTabs;
            }
        }

        public Dictionary<string, string> Languages
        {
            get
            {
                var Languages = new Dictionary<string, string>();
                Languages.Add("German", "de-DE");
                Languages.Add("English", "en-US");
                return Languages;
            }
        }

        public string SelectedLanguage
        {
            get
            {
                return Properties.Settings.Default.Language;
            }
            set
            {
                if (value == null)
                    return;

                if (Properties.Settings.Default.Language != value)
                {
                    Properties.Settings.Default.Language = value;
                    Properties.Settings.Default.Save();

                    OnLanguageChanged();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedLanguage"));
                }
            }
        }

        private IRingenTabItem m_TabControlSelectedItem;
        public object TabControlSelectedItem
        {
            get
            {
                return m_TabControlSelectedItem;
            }
            set
            {
                //var tempPlugin = value is IRingenTabItem;
                if (!(value is IRingenTabItem tempPlugin))
                {
                    Set(ref m_TabControlSelectedItem, null);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TabControlSelectedItem"));
                }
                else
                {
                    Set(ref m_TabControlSelectedItem, tempPlugin);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TabControlSelectedItem"));
                }
            }
        }

        public string UserName
        {
            get
            {
                return Service.Login.UserName;
            }
            set
            {
                Service.Login.UserName = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return Service.Login.Password;
            }
            set
            {
                Service.Login.Password = value;
            }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainViewModel()
        {
            ConstructorUniversal();
        }

        private void ConstructorUniversal()
        {
            string tempLogFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName}\\{Assembly.GetEntryAssembly().GetName().Name}\\Log.log";
            //string tempLogFile = @"C:\temp\xx\Log.log";
            logToFile = new LogToFile(tempLogFile, 1000);
            Messenger.Default.Register(this, (ExitCodeMessage obj) => { Startup.ExitCode = obj.ExitCode; });
            Messenger.Default.Register<LoggerMessage>(this, LoggerMessageRecieved);
        }


        private void LoggerMessageRecieved(LoggerMessage obj)
        {

            RunInUI.Run(() =>
            {
                lock (m_LockObj)
                {
                    var logEntry = obj.LogEntry;

                    if (logEntry == null || !m_DataHashSet.Add(logEntry))
                    {
#if DEBUG
                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Break();
                        // oder logentry ist null -> der DataGrid von Xceed mag keine nulls
                        // oder das selbe Objekt (logentry) wollte auf die Liste kommen -> DataGrid mag es auch nicht
#endif
                        return;
                    }

                    LogData.Add(logEntry);
                }
            });


        }

        #endregion

        #region commands

        private RelayCommand m_CloseNotificationIcon;
        public RelayCommand CloseNotificationIcon => m_CloseNotificationIcon ?? (m_CloseNotificationIcon = new RelayCommand(OnCloseNotificationIcon));


        private RelayCommand m_RcOpenHelp;
        public RelayCommand RcOpenHelp => m_RcOpenHelp ?? (m_RcOpenHelp = new RelayCommand(OnRcOpenHelp));

        private void OnRcOpenHelp()
        {
            Process.Start(@"Resources\Help\de-DE\RingenHelp.pdf");
        }


        private void OnCloseNotificationIcon()
        {
            Application.Current.Shutdown();
        }

        public RelayCommand<MouseEventArgs> CmExplorerDoubleClick
        {
            get
            {
                return new RelayCommand<MouseEventArgs>(new Action<MouseEventArgs>((MouseEventArgs e) =>
                {
                    OpenWithDoubleClickMessage.Send((x) =>
                    {
                        if (x.Open && !OpenedTabs.Contains(x.RingenTabItem))
                        {
                            OpenedTabs.Add(x.RingenTabItem);
                            // Tab selektieren
                            TabControlSelectedItem = x.RingenTabItem;
                        }
                    });

                    e.Handled = true;
                }));
            }
        }

        public RelayCommand<DocumentClosedEventArgs> CmTabControlTabClosing
        {
            get
            {
                return new RelayCommand<DocumentClosedEventArgs>(new Action<DocumentClosedEventArgs>((DocumentClosedEventArgs _tabItem) =>
                {
                    //if (_tabItem?.Document.Content as IRingenTabItem != null)
                    OpenedTabs.Remove((IRingenTabItem)((LayoutDocument)_tabItem?.Document).Content);

                }), true);
            }
        }

        private RelayCommand<NotifyCollectionChangedEventArgs> m_CMAvalonDockDocumentsCollectionChanged;
        public RelayCommand<NotifyCollectionChangedEventArgs> CMAvalonDockDocumentsCollectionChanged => m_CMAvalonDockDocumentsCollectionChanged ?? (m_CMAvalonDockDocumentsCollectionChanged = new RelayCommand<NotifyCollectionChangedEventArgs>(OnCMAvalonDockDocumentsCollectionChanged));

        public RelayCommand<RoutedPropertyChangedEventArgs<object>> CmExplorerSelectedItemChanged
        {
            get
            {
                return new RelayCommand<RoutedPropertyChangedEventArgs<object>>(new Action<RoutedPropertyChangedEventArgs<object>>((RoutedPropertyChangedEventArgs<object> e) =>
                {
                    MannschaftskaempfeExplorer.SelectedItem = (IExplorerItemViewModel)e.NewValue;
                }));
            }
        }
        public RelayCommand CmExplorerSelectionMade
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //OpenPluginWithDoubleClick();
                });
            }
        }

        #endregion

        #region Message Handlers

        #endregion

        #region events

        /// <summary>
        /// Language Changed Event auslösen
        /// </summary>
        public void OnLanguageChanged()
        {
            TransManager.Instance.CurrentLanguage = new CultureInfo(Properties.Settings.Default.Language);
        }

        private void OnCMAvalonDockDocumentsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count == 1)
            {
                LayoutDocument tempDoc = e.NewItems[0] as LayoutDocument;

                if (tempDoc?.Content is IRingenTabItem tempPlugin && tempPlugin != null)
                {
                    tempPlugin.Container = tempDoc;
                }
            }
        }

        #endregion

    }
}
