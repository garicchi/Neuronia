using Windows.System;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;

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
using Windows.UI.Xaml.Navigation;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace Neuronia.Flyout
{
    public sealed partial class UserDetailFlyout : SettingsFlyout
    {
        NeuroniaViewModel viewModel;

        public string FollowString { get; set; }

        public Action CallDirectMessage { get; set; }
        public UserDetailFlyout(NeuroniaViewModel viewModel,Action callDirectMessage)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            this.CallDirectMessage = callDirectMessage;
            this.comboBoxDirectMessageAccount.ItemsSource = viewModel.AccountList;
            this.comboBoxDirectMessageAccount.SelectedIndex = 0;
        }

        private void btn_DirectMessage_Click(object sender, RoutedEventArgs e)
        {
            CallDirectMessage();
        }

        private void btn_viewWeb_Click(object sender, RoutedEventArgs e)
        {
            PreviewBrowserFlyout flyout=new PreviewBrowserFlyout();
            flyout.Show(viewModel.UserDetail.UserInformation.Getlink(),Window.Current.Bounds.Width*2/3);
        }

        private void btn_sendDirectMessage_Click(object sender, RoutedEventArgs e)
        {
            
            viewModel.SendDirectMessageCommand.Execute(new SendDirectMessage
            {
                SenderScreenName = (comboBoxDirectMessageAccount.SelectedItem as TwitterAccount).UserInfomation.screen_name,
                RecipientScreenName = viewModel.UserDetail.UserInformation.screen_name,
                Message = text_directMessage.Text
            });
            
        }

        private void btn_sendMute_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddMuteAccountCommand.Execute(viewModel.UserDetail.UserInformation.screen_name);
        }

    }
}
