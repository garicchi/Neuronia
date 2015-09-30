using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.DirectMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;
using Neuronia.Hub.Detail.Parameter;

namespace Neuronia.Hub.Row
{
    [DataContract]
    public class NotificationRow:RowBase
    {
        

        private string message;
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; ModelPropertyChanged("Message"); }
        }

        

        private NotificationType nType;
        [DataMember]
        public NotificationType NType
        {
            get { return nType; }
            set { nType = value; ModelPropertyChanged("NType");}
        }

        public RelayCommand<string> UserDetailCommand { get; set; }
        
        
         public NotificationRow(Tweet tweet,string message,SettingData setting,NotificationType type,string ownerScreenName,Action<RowAction> actionCallback)
             :base(tweet,ownerScreenName,setting,actionCallback,RowType.Notification)
        {
            Initialize(rowActionCallback);
             this.NType = type;
             this.Message = message;
             
        }

         public void Initialize(Action<RowAction> rowActionCallBack)
         {
             SharedDispatcher.RunAsync(() =>
             {
                 switch (NType)
                 {
                     case NotificationType.System:
                         BarColorBrush = (Application.Current.Resources["SystemNotificationBrush"] as SolidColorBrush).Color;
                         break;
                     case NotificationType.Favorite:
                         BarColorBrush = (Application.Current.Resources["FavoriteForegroundBrush"] as SolidColorBrush).Color;
                         break;
                     case NotificationType.Retweet:
                         BarColorBrush = (Application.Current.Resources["RetweetForegroundBrush"] as SolidColorBrush).Color;
                         break;
                     case NotificationType.DirectMessage:
                         BarColorBrush = (Application.Current.Resources["DirectMessageForegroundBrush"] as SolidColorBrush).Color;
                         break;
                     case NotificationType.Follow:
                         BarColorBrush = (Application.Current.Resources["DirectMessageForegroundBrush"] as SolidColorBrush).Color;
                         break;
                 }
             });
             
             CommandInitialize();
         }

         private void CommandInitialize()
         {
             UserDetailCommand = new RelayCommand<string>(screenName =>
             {
                 this.rowActionCallback(new RowAction(RowActionType.UserDetail, new UserDetailParameter(this.OwnerScreenName,screenName)));
             });
         }
    }
}
