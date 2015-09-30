using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

namespace Neuronia.Hub.Converter
{
    public class StringToUriConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string str)
        {
            string url_str = value.ToString();
            
            return new Uri(url_str);
        }

        //今回は実装なし
        public object ConvertBack(object value, Type targetType, object parameter, string str)
        {
            return null;
        }
    }
}
