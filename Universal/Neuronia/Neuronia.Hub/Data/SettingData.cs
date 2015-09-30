using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Hub.UIThemeSet;
using Windows.UI.Xaml;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Neuronia.Hub.Data
{
    [DataContract]
    public class SettingData:ModelBase
    {
        public SettingData()
        {
            
            IsNotificationEnable = true;
            IsInternalNotification = true;
            IsToastNotification = false;
            IsSoundEnable = true;
            AppTheme = new UITheme();
            Footer = "";
            IsRoseVisible = true;
            
            
            SettingInitialize();

        }

        public async Task<byte[]> ImageInitialize(IRandomAccessStream stream)
        {
           

                var size = stream.Size;

                 byte[] bytes = new byte[size];
            
                var reader = new DataReader(stream.GetInputStreamAt(0));
                await reader.LoadAsync((uint)size);
                reader.ReadBytes(bytes);
                return bytes;
            
        }

        public void SettingInitialize()
        {

            Color appThemeColor = Color.FromArgb(255, 178, 62, 116);
            Color appBarColor = Color.FromArgb(100, 0, 0, 0);
            Application.Current.Resources["AppThemeBrush"] = new SolidColorBrush(appThemeColor);
            double timelineFontSize = 16.0;
            Application.Current.Resources["TimelineFontSize"] = timelineFontSize;
            TimelineFontSize = timelineFontSize;

            AppTheme.AppTheme = appThemeColor;
            AppTheme.BottomTweetBarBackground.UIColor = appThemeColor;
            AppTheme.BottomTweetBarBackground.IsImageEnable = false;
            AppTheme.BottomAppBarBackground.UIColor = appBarColor;
            AppTheme.BottomAppBarBackground.IsImageEnable = false;
            AppTheme.TopAppBarBackground.UIColor = appBarColor;
            AppTheme.TopAppBarBackground.IsImageEnable = false;
            AppTheme.SettingsFlyoutBackground.UIColor = appBarColor;
            AppTheme.SettingsFlyoutBackground.IsImageEnable = false;
            AppTheme.MainBackground.IsImageEnable = true;

            MuteAccountList = new ObservableCollection<string>();
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/firstBackground.jpg")).AsTask<StorageFile>().Result;
            var stream = file.OpenAsync(FileAccessMode.Read).AsTask<IRandomAccessStream>().Result;

            AppTheme.MainBackground.UIImage = ImageInitialize(stream).Result;
            IsRoseVisible = true;
            
        }

        public void SettingConstInitialize()
        {
            SuggestColorList = new ObservableCollection<Color>()
            {
                Colors.Violet,
                Colors.SteelBlue,
                Colors.Teal,
                Colors.YellowGreen,
                Colors.DarkOrange,
                Color.FromArgb(100, 0, 0, 0),
                Color.FromArgb(255, 178, 62, 116),
                Color.FromArgb(255,224,235,175),
                Color.FromArgb(255,254,244,244),
                Color.FromArgb(255,155,124,182),
                Color.FromArgb(255,108,187,90),
                Color.FromArgb(255,228,77,147),
                Color.FromArgb(255,169,204,81),
                Color.FromArgb(255,0,121,194),
                Color.FromArgb(255,51,51,0),
                Color.FromArgb(255,238,123,26),
                Color.FromArgb(255,255,153,204),
                Color.FromArgb(255,255,51,0),
                Color.FromArgb(255,255,215,0),
                Color.FromArgb(255,238,195,98),
                Color.FromArgb(255,0,0,128),
                Color.FromArgb(255,56,180,139),
                Color.FromArgb(255,190,210,195),
                Color.FromArgb(255,215,207,58),
                Color.FromArgb(255,183,40,46),
                Color.FromArgb(255,39,74,120),
                Color.FromArgb(255,135,206,235),
                Color.FromArgb(255,0,255,127),
                Color.FromArgb(255,71,131,132),
                Color.FromArgb(255,202,184,217),
                Color.FromArgb(255,231,53,98),
                Color.FromArgb(255,153,0,204),
                Color.FromArgb(255,70,130,180),
                Color.FromArgb(255,112,128,144),
                Color.FromArgb(255,95,158,160),
                Color.FromArgb(255,255,127,80),
            };
        }
        
        
        private bool isNotificationEnable;

        [DataMember]
        public bool IsNotificationEnable
        {
            get { return isNotificationEnable; }
            set { isNotificationEnable = value; ModelPropertyChanged("IsNotificationEnable"); }
        }

        private bool isInternalNotification;
        [DataMember]
        public bool IsInternalNotification
        {
            get { return isInternalNotification; }
            set { isInternalNotification = value; ModelPropertyChanged("IsInternalNotification"); }
        }

        private bool isToastNotification;
        [DataMember]
        public bool IsToastNotification
        {
            get { return isToastNotification; }
            set { isToastNotification = value; ModelPropertyChanged("IsToastNotification"); }
        }

        
        private double timelineFontSize;
        [DataMember]
        public double TimelineFontSize
        {
            get { return timelineFontSize; }
            set { timelineFontSize = value; ModelPropertyChanged("TimelineFontSize");}
        }
        private UITheme appTheme;
        [DataMember]
        public UITheme AppTheme
        {
            get { return appTheme; }
            set { appTheme = value; ModelPropertyChanged("AppTheme"); }
        }

        [DataMember]
        public ObservableCollection<string> MuteAccountList { get; set; } 
        
        [IgnoreDataMember]
        public ObservableCollection<Color> SuggestColorList { get; set; }

        private bool isSoundEnable;
        [DataMember]
        public bool IsSoundEnable
        {
            get { return isSoundEnable; }
            set { isSoundEnable = value; ModelPropertyChanged("IsSoundEnable");}
        }

        private string footer;
        [DataMember]
        public string Footer
        {
            get { return footer; }
            set { footer = value; ModelPropertyChanged("Footer");}
        }

        private bool isRoseVisible;
        [DataMember]
        public bool IsRoseVisible
        {
            get { return isRoseVisible; }
            set { isRoseVisible = value; ModelPropertyChanged("IsRoseVisible"); }
        }
        
        


    }
}
