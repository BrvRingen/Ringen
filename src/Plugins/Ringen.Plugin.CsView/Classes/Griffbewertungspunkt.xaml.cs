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

namespace Ringen.Plugin.CsView
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

    }
}
