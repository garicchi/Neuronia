using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Neuronia.Hub.UIThemeSet;
using Windows.UI.Xaml.Media;

namespace Neuronia.Hub.Converter
{
    public class SelectedItemToUIBrushConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            if (value != null)
            {
                var color = (value as UIBrush).UIColor;
                return color;
            }
            else
            {
                return Colors.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var c = value as SolidColorBrush;
                UIBrush brush = new UIBrush();
                brush.IsImageEnable = false;
                brush.UIColor = c.Color;
                return brush;
            }
            else
            {
                return new UIBrush();
            }
        }

    }
}
