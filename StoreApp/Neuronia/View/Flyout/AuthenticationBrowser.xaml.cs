using Neuronia.Common;
using Neuronia.Core.Data;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia.Flyout
{
    public sealed partial class AuthenticationBrowser : UserControl
    {

        AttachedProgress progress;

        Popup popup;

        private int navigationCounter;

        public event Action<string> PostAuthorizedPin;

        public AuthenticationBrowser()
        {
            this.InitializeComponent();
            navigationCounter = 0;
            webView.NavigationCompleted += (s, e) =>
            {
                navigationCounter++;
                if (navigationCounter > 1)
                {
                    VisualStateManager.GoToState(this,"VisualStatePinCode",true);
                }
                progress.InActive();
            };
            progress = new AttachedProgress(this.gridRoot,100,100);

            progress.ProgressBrush =Application.Current.Resources["AppThemeBrush"] as SolidColorBrush;
            Grid.SetRow(progress.Popup,1);

            PostAuthorizedPin += (str) =>
            {
            };
            VisualStateManager.GoToState(this, "VisualStateNavigate", true);
            popup = new Popup();
        }

        public void NavigateUrl(string url)
        {
            this.webView.Navigate(new Uri(url));
        }

        public void Close()
        {
            this.popup.IsOpen = false;
        }

        public void Show()
        {

            Height = Window.Current.Bounds.Height - Window.Current.Bounds.Height/10;
            Width = Window.Current.Bounds.Width;
            popup.Child = this;
            popup.Width = Width;
            popup.Height = Height;
            Canvas.SetTop(popup, Window.Current.Bounds.Height / 2 - popup.Height / 2);
           
            popup.IsOpen = true;
            progress.Active();
        }

        

        private void btnPin_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.textBoxPin.Text != string.Empty)
            {
                btnPin.IsEnabled = false;
                PostAuthorizedPin(textBoxPin.Text);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        
    }
}
