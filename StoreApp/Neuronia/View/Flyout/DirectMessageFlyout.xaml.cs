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
using Neuronia.Hub.Common;
using GalaSoft.MvvmLight.Messaging;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください
using Neuronia.Hub.Detail.Parameter;

namespace Neuronia.View.Flyout
{

    public sealed partial class DirectMessageFlyout : SettingsFlyout
    {
        NeuroniaViewModel viewModel;
        public DirectMessageFlyout(NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;

            
        }

        private void btnSendDM_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UserDetailCommand.Execute(new UserDetailParameter(viewModel.DMDetail.OwnerAccount.UserInfomation.screen_name,viewModel.DMDetail.DMessage.sender_screen_name));
            
        }
    }
}
