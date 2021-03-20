using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using Ringen.Core;
using Ringen.Core.Messaging;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Point.xaml
    /// </summary>
    public partial class BoutPoint : UserControl
    {
        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Core.CS.BoutPoint), typeof(BoutPoint), new PropertyMetadata());

        public Core.CS.BoutPoint Data
        {
            get { return (Core.CS.BoutPoint)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public BoutPoint()
        {
            InitializeComponent();
        }


        private RelayCommand m_Delete;
        public RelayCommand Delete => m_Delete ?? (m_Delete = new RelayCommand(() =>
        {
            Data.EinzelkampfViewModel.Points.Remove(Data);
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, "Point deleted to Points"));
        }
        ));

    }
}
