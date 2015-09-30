using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Neuronia.Hub.UIThemeSet;

namespace Neuronia.Hub.Converter
{
    public class UIThemeToBrushConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value as UIBrush).GetBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw  new Exception();
        }
    }
}
