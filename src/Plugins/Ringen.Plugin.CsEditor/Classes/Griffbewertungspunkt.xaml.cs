using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Ringen.Core.Messaging;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Point.xaml
    /// </summary>
    public partial class Griffbewertungspunkt : UserControl
    {
        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Schnittstellen.Contracts.Models.Griffbewertungspunkt), typeof(Griffbewertungspunkt), new PropertyMetadata());

        public Schnittstellen.Contracts.Models.Griffbewertungspunkt Data
        {
            get { return (Schnittstellen.Contracts.Models.Griffbewertungspunkt)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public Griffbewertungspunkt()
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
