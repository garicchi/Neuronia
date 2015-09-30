using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Neuronia.Hub.Row;

namespace Neuronia.Hub.Converter
{
    public class TweetRowToBrushConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var row = value as TimelineRow;
            if (row.Tweet.text.Contains("@" + row.OwnerScreenName))
            {
                return Application.Current.Resources["MentionForegroundBrush"] as SolidColorBrush;
            }
            else
            {
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
