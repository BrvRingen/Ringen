using AvalonDock.Layout;
using Ringen.Core.UI;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Ringen
{
    public class LanguageToIconConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri($"/Ringen;component/Resources/Images/Language_{(string)value}.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class ResultToImageConverter : IValueConverter
    {
        #region public functions

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri($"/Ringen;component/Resources/Images/ResultImages/{value.ToString()}.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class PluginToHeaderConverter : IMultiValueConverter
    {
        #region public functions

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return ((values[0] as LayoutDocument)?.Content as IRingenTabItem)?.RingenTabItemHeaderName;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
