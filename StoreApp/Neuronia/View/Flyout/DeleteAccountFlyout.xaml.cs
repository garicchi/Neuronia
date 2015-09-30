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

namespace Neuronia
{
    public sealed partial class DeleteAccountFlyout : SettingsFlyout
    {
        NeuroniaViewModel viewModel;
        Action deleteCompleteCallBack;
        public DeleteAccountFlyout(NeuroniaViewModel viewModel,Action deleteCompleteCallBack)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.deleteCompleteCallBack = deleteCompleteCallBack;
            gridViewDeleteAccount.ItemsSource = viewModel.AccountList;
        }

        private void btnAccountDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ac in gridViewDeleteAccount.SelectedItems)
            {
                viewModel.DeleteAccountCommand.Execute(ac as TwitterAccount);
            }
            deleteCompleteCallBack();
        }
    }
}
