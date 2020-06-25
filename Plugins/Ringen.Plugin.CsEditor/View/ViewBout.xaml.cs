﻿using GalaSoft.MvvmLight.Command;
using Ringen.Core;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Bout.xaml
    /// </summary>
    public partial class ViewBout : ExtendedNotifyPropertyChangedUserControl
    {
        private Core.CS.Bout bout;

        public Core.CS.Bout Bout
        {
            get { return bout; }
            set
            {
                bout = value;
                OnPropertyChanged("Bout");
            }
        }

        public ViewBout()
        {
            InitializeComponent();
            UpdateUi();
            Explorer.SelectedItemChanged += ((object sender, Explorer.SelectedItemChangedEventArgs e) => { UpdateUi(); });
        }

        public void UpdateUi()
        {
            Bout = Explorer.SelectedItem as Core.CS.Bout;
        }


        private RelayCommand<string> m_Start;
        public RelayCommand<string> Start => m_Start ?? (m_Start = new RelayCommand<string>((string TimeType) => { Bout.Settings.Times[TimeType].Start(); }));

        private RelayCommand<string> m_Stop;
        public RelayCommand<string> Stop => m_Stop ?? (m_Stop = new RelayCommand<string>((string TimeType) => { Bout.Settings.Times[TimeType].Stop(); }));
    }
}
