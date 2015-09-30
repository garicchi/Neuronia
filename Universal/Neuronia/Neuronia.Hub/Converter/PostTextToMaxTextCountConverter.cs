using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Neuronia.Hub.Converter
{
    public class PostTextToMaxTextCountConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value.ToString();
            return (140 - str.Length).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
