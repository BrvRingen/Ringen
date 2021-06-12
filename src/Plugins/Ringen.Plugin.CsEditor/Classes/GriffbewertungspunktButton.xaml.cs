using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Ringen.Core.Messaging;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Point.xaml
    /// </summary>
    public partial class GriffbewertungspunktButton : UserControl
    {
        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Schnittstellen.Contracts.Models.Griffbewertungspunkt), typeof(GriffbewertungspunktButton), new PropertyMetadata());

        public Schnittstellen.Contracts.Models.Griffbewertungspunkt Data
        {
            get { return (Schnittstellen.Contracts.Models.Griffbewertungspunkt)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public GriffbewertungspunktButton()
        {
            InitializeComponent();
        }


        private RelayCommand m_AddToPoints;
        public RelayCommand AddToPoints => m_AddToPoints ?? (m_AddToPoints = new RelayCommand(() =>
        {
            //Data.EinzelkampfViewModel.Points.Add(new Core.CS.BoutPoint(Data.Value, Data.EinzelkampfViewModel, Data.HomeOrOpponent));
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, "Point added to Points"));
        }
        ));

    }
}
