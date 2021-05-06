using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
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
            //Data.EinzelkampfViewModel.Points.Remove(Data);
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, "Point deleted to Points"));
        }
        ));

    }
}
