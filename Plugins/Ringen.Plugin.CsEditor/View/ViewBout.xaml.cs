using GalaSoft.MvvmLight.Command;
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
            if (Explorer.SelectedItem is Core.CS.Bout)
            {
                Bout = (Core.CS.Bout)Explorer.SelectedItem;

                PosPointsHome.Children.Clear();
                PosPointsOpponent.Children.Clear();
                foreach (var PosPoint in Bout.Settings.PosPoints)
                {
                    PosPointsHome.Children.Add(new BoutPoint(new Core.CS.BoutPoint(PosPoint, Core.CS.BoutPoint.Wrestler.Home, 0)));
                    PosPointsOpponent.Children.Add(new BoutPoint(new Core.CS.BoutPoint(PosPoint, Core.CS.BoutPoint.Wrestler.Opponent, 0)));
                }
            }
            else
                Bout = null;
        }


        private RelayCommand<string> m_Start;
        public RelayCommand<string> Start => m_Start ?? (m_Start = new RelayCommand<string>((string TimeType) => { Bout.Settings.Times[TimeType].Start(); }));

        private RelayCommand<string> m_Stop;
        public RelayCommand<string> Stop => m_Stop ?? (m_Stop = new RelayCommand<string>((string TimeType) => { Bout.Settings.Times[TimeType].Stop(); }));
    }
}
