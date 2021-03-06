﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Neuronia.Hub.Converter
{
    public class ColorToBrushConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value as SolidColorBrush).Color;
        }
    }
}
