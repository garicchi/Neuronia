using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Notifications;
using Neuronia.Flyout;
using Neuronia.Hub.Common;

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
using Windows.UI.Xaml.Navigation;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください
using Neuronia.Utility;

namespace Neuronia.View
{
    public sealed partial class TweetDetailFlyout : SettingsFlyout
    {


        NeuroniaViewModel viewModel;
        public TweetDetailFlyout(NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            
        }

        private void SettingsFlyout_BackClick(object sender, BackClickEventArgs e)
        {
            this.Hide();
        }

        private async void btn_viewWeb_Click(object sender, RoutedEventArgs e)
        {
            PreviewBrowserFlyout flyout = new PreviewBrowserFlyout();
            flyout.Show(viewModel.TweetDetail.Row.Tweet.Getlink(), Window.Current.Bounds.Width * 2 / 3);
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
           
            viewModel.CopyClipBoardCommand.Execute(viewModel.TweetDetail.Row.Tweet.text);
        }

        

        

        
    }
}
