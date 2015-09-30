using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Neuronia.Hub.Converter
{
    public class TweetTimeConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = ((DateTime)value).ToLocalTime().ToString("HH:mm");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
