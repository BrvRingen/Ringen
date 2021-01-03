using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static Ringen.Core.CS.BoutPoint;

namespace Ringen.Plugin.CsView
{
    public class NullToVisiblityConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class TimeRunningToVisiblityConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Core.CS.BoutTime.Modes)value == Core.CS.BoutTime.Modes.Running)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IntToTimeConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((int)value).ToString("m':'ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class TeamToBrushConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Wrestler)value == Wrestler.Home)
                return Brushes.Red;
            else
                return Brushes.LightSkyBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class PointsSumConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as ObservableCollection<Core.CS.BoutPoint>).Select(x => (x.HomeOrOpponent == (Wrestler)Enum.Parse(typeof(Wrestler), parameter.ToString()) && int.TryParse(x.Value, out int Point) ? Point : 0)).Sum();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

}
