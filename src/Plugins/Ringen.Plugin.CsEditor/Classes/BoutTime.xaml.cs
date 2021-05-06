using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für BoutTime.xaml
    /// </summary>
    public partial class BoutTime : UserControl
    {
        public BoutTime()
        {
            InitializeComponent();
        }

        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Core.CS.BoutTime), typeof(BoutTime), new PropertyMetadata());

        public Core.CS.BoutTime Data
        {
            get { return (Core.CS.BoutTime)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BoutTime), new PropertyMetadata());

        public string Title
        {
            get { return (string)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        private RelayCommand m_Start;
        public RelayCommand Start => m_Start ?? (m_Start = new RelayCommand(() => {Data.Start();}));

        private RelayCommand m_Stop;
        public RelayCommand Stop => m_Stop ?? (m_Stop = new RelayCommand(() => { Data.Stop(); }));
    }
}
