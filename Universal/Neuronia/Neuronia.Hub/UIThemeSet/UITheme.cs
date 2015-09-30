using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Neuronia.Core.Common;

namespace Neuronia.Hub.UIThemeSet
{
    [DataContract]
    public class UITheme:ModelBase
    {
        public UITheme()
        {
            AppTheme = Colors.Tomato;
            MainBackground=new UIBrush();
            BottomTweetBarBackground=new UIBrush();
            BottomAppBarBackground=new UIBrush();
           TopAppBarBackground=new UIBrush();
            SettingsFlyoutBackground=new UIBrush();
        }
        private Color appThemeBrush;

        [DataMember]
        public Color AppTheme
        {
            get { return appThemeBrush; }
            set { this.appThemeBrush = value;ModelPropertyChanged("AppTheme"); }
        }

        private UIBrush mainBackgroundBrush;

        [DataMember]
        public UIBrush MainBackground
        {
            get { return mainBackgroundBrush; }
            set { this.mainBackgroundBrush = value; ModelPropertyChanged("MainBackground"); }
        }

        private UIBrush bottomTweetBarBackgroundBrush;

        [DataMember]
        public UIBrush BottomTweetBarBackground
        {
            get { return this.bottomTweetBarBackgroundBrush; }
            set
            {
                UIBrush brush= value;
                //brush.UIColor = Color.FromArgb(100,brush.UIColor.R,brush.UIColor.G,brush.UIColor.B);
                this.bottomTweetBarBackgroundBrush = brush;
                ModelPropertyChanged("BottomTweetBarBackground");
            }
        }

        private UIBrush bottomAppBarBackgroundBrush;

        [DataMember]
        public UIBrush BottomAppBarBackground
        {
            get { return this.bottomAppBarBackgroundBrush; }
            set {
                UIBrush brush = value;
               // brush.UIColor = Color.FromArgb(100, brush.UIColor.R, brush.UIColor.G, brush.UIColor.B);
                this.bottomAppBarBackgroundBrush = brush;
                
                ModelPropertyChanged("BottomAppBarBackground");
            }
        }

        private UIBrush topAppBarBackgroundBrush;

        [DataMember]
        public UIBrush TopAppBarBackground
        {
            get { return this.topAppBarBackgroundBrush; }
            set
            {
                UIBrush brush = value;
                //brush.UIColor = Color.FromArgb(100, brush.UIColor.R, brush.UIColor.G, brush.UIColor.B);
                
                this.topAppBarBackgroundBrush = brush; 
                ModelPropertyChanged("TopAppBarBackground");
            }
        }

        private UIBrush settingsFlyoutBackgroundBrush;

        [DataMember]
        public UIBrush SettingsFlyoutBackground
        {
            get { return this.settingsFlyoutBackgroundBrush; }
            set
            {
                UIBrush brush = value;
                //brush.UIColor = Color.FromArgb(100, brush.UIColor.R, brush.UIColor.G, brush.UIColor.B);
                
                this.settingsFlyoutBackgroundBrush = brush; 
                ModelPropertyChanged("SettingsFlyoutBackground");
            }
        }

    }
}
