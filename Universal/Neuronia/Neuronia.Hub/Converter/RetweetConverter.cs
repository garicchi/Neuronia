using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Neuronia.Hub.Converter
{
    public class RetweetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string str)
        {
            var isFav = System.Convert.ToBoolean(value);

            var brush = new SolidColorBrush();

            if (isFav)
            {
                brush.Color =Colors.Yellow;
            }
            else
            {
                brush.Color=Colors.Gray;
            }

            return brush;
        }

        //今回は実装なし
        public object ConvertBack(object value, Type targetType, object parameter, string str)
        {
            return false;
        }
    }
}
