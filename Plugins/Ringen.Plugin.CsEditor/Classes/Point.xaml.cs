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
    public partial class Point : UserControl
    {
        private Core.CS.Bout.Point data;

        public Core.CS.Bout.Point Data
        {
            get { return data; }
            set { data = value; }
        }

        public Point(Core.CS.Bout.Point Point)
        {
            Data = Point;
            this.Background = Data.HomeOrOpponent == Core.CS.Bout.Point.Wrestler.Home ? Brushes.Red : Brushes.Blue;

            InitializeComponent();
        }
    }
}
