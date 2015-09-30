using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Neuronia.Hub.Converter
{
    public class ByteToBitmapConverterForBackground:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {

                byte[] bytes = (byte[])value;
                BitmapImage myBitmapImage = new BitmapImage();
                if (bytes.Count() > 0)
                {


                    InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                    DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));

                    writer.WriteBytes(bytes);
                    writer.StoreAsync().GetResults();
                    myBitmapImage.SetSource(stream);
                }
                else
                {
                    myBitmapImage.UriSource = new Uri("ms-appx:///Assets/firstBackgroundImage.jpg");
                }

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
