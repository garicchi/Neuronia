using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Neuronia.Hub.Converter
{
    public class StringToAccountListConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            var list = value as ObservableCollection<string>;
            string str = string.Join(",", list);
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string str = value.ToString();
            str.Replace("@", "");
            var list = new ObservableCollection<string>();
            foreach (var ac in str.Split(','))
            {
                list.Add(ac);
            }
            return list;
        }
    }
}
