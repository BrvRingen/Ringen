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
using Ringen.Core;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Point.xaml
    /// </summary>
    public partial class BoutPoint : UserControl
    {
        private Core.CS.BoutPoint data;

        public Core.CS.BoutPoint Data
        {
            get { return data; }
            set { data = value; }
        }

        public BoutPoint(Core.CS.BoutPoint BoutPoint)
        {
            Data = BoutPoint;
            this.Background = Data.HomeOrOpponent == Core.CS.BoutPoint.Wrestler.Home ? Brushes.Red : Brushes.Blue;

            InitializeComponent();
        }
    }
}
