using Windows.UI.Xaml.Media.Imaging;
using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.Storage.Streams;

namespace Neuronia.Hub.UIThemeSet
{
    [DataContract]
    public class UIBrush:ModelBase
    {
        public UIBrush()
        {
            UIColor = Colors.Transparent;
            UIImage=new byte[]{};
        }
        private Color uiColor;
        [DataMember]
        public Color UIColor
        {
            get { return uiColor; }
            set
            {
                
                uiColor = value; ModelPropertyChanged("UIColor");
            }
        }

        private byte[] uiImage;     
        [IgnoreDataMember]
        public byte[] UIImage
        {
            get { return uiImage; }
            set { uiImage = value; ModelPropertyChanged("UIImage"); }
        }

        private bool isImageEnable;
        [DataMember]
        public bool IsImageEnable
        {
            get { return isImageEnable; }
            set { isImageEnable = value; ModelPropertyChanged("IsImageEnable"); }
        }

        public Brush GetBrush()
        {
            if (IsImageEnable)
            {
                var brush = new ImageBrush();
                brush.Stretch = Stretch.UniformToFill;
                BitmapImage myBitmapImage = new BitmapImage();
                InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));

                writer.WriteBytes(UIImage);
                writer.StoreAsync().GetResults();
                myBitmapImage.SetSource(stream);

                brush.ImageSource = myBitmapImage;
                return brush;
            }
            else
            {
                var brush = new SolidColorBrush(UIColor);
                return brush;
            }
        }

        
        
    }
}
