using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Neuronia.Hub.Converter
{
    public class ColorListToBrushListConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var list=value as ObservableCollection<Color>;
            var brushList = new ObservableCollection<SolidColorBrush>();
            return list.Select(q => new SolidColorBrush(q)).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
