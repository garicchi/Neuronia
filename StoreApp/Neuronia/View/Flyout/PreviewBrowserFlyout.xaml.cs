using Neuronia.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia.Flyout
{
    public sealed partial class PreviewBrowserFlyout : UserControl
    {
        
        Popup popup;
        public PreviewBrowserFlyout()
        {
            this.InitializeComponent();
            
            popup = new Popup();
           

            webView.NavigationStarting += (s, e) =>
            {
                progressBar.IsIndeterminate = true;
                
            };
            
            webView.NavigationCompleted += (s, e) =>
            {
                progressBar.IsIndeterminate = false;
                
            };
        }

        public async void Show(Uri uri, double width)
        {
            if (Window.Current.Bounds.Width < 500)
            {
                Close();
                await Launcher.LaunchUriAsync(uri);
            }
            else
            {
                this.Width = width;
                transEndKeyFrame.Value = -Width;
                var story = Resources["OpenAnimation"] as Storyboard;
                story.Begin();

                webView.Navigate(uri);

                this.Height = Window.Current.Bounds.Height;
                popup.Height = Height;

                Canvas.SetLeft(popup, Window.Current.Bounds.Width);
                Canvas.SetTop(popup, 0);
                popup.Child = this;
                popup.IsOpen = true;
            }
        }

        public void Close()
        {
            
            webView.NavigateToString("<html>Empty</html>");
            popup.IsOpen = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnOpenBrowser_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(webView.Source);
            Close();
        }
    }
}
