using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Neuronia.Hub.Common;

namespace Neuronia.Hub.Converter
{
    public class ByteToBitmapImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                
                byte[] bytes = (byte[])value;
                BitmapImage myBitmapImage = new BitmapImage();
                


                    InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                    DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));

                    writer.WriteBytes(bytes);
                    writer.StoreAsync().GetResults();
                    myBitmapImage.SetSource(stream);
                
                return myBitmapImage;
            }
            else
            {
                return new BitmapImage();
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
