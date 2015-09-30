using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia.View
{
    public sealed partial class NotificationStatusControl : UserControl
    {
        DispatcherTimer notificationTimer;
        public NotificationState State { get; set; }

        NotificationState BeforeState { get; set; }
        public NotificationStatusControl()
        {
            this.InitializeComponent();
            (Resources["rotateAnimetion"] as Storyboard).Begin();
            VisualStateManager.GoToState(this, "StateNotConnection", true);
            State = NotificationState.NotConnection;
            notificationTimer = new DispatcherTimer();
            notificationTimer.Tick += (s, e) =>
            {
                switch (BeforeState)
                {
                    case NotificationState.Connection:
                        VisualStateManager.GoToState(this, "StateConnection", true);
                        State = NotificationState.Connection;
                        break;
                    case NotificationState.NotConnection:
                        VisualStateManager.GoToState(this, "StateNotConnection", true);
                        State = NotificationState.NotConnection;
                        break;
                    case NotificationState.Notification:
                        VisualStateManager.GoToState(this, "StateNotification", true);
                        State = NotificationState.Notification;
                        break;
                }
                notificationTimer.Stop();
            };
        }

        public void Connection(string message){
            textConnection.Text = message;
            VisualStateManager.GoToState(this,"StateConnection",true);
            State = NotificationState.Connection;
        }

        public void NotConnection(string message)
        {
            textNotConnection.Text = message;
            VisualStateManager.GoToState(this, "StateNotConnection", true);
            State = NotificationState.NotConnection;
        }

        public void Notification(string message, TimeSpan length)
        {
            BeforeState = this.State;
            State = NotificationState.Notification;
            VisualStateManager.GoToState(this, "StateNotification", true);
            textNotification.Text = message;
            notificationTimer.Interval = length;
            notificationTimer.Start();
        }
    }

    public enum NotificationState
    {
        Connection,NotConnection,Notification
    }

}
