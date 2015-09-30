using Neuronia.Hub.Common;
using Neuronia.Hub.Tab;

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

namespace Neuronia.View
{
    public sealed partial class AddTabFlyout : SettingsFlyout
    {
        public Action<bool> AddTabCallBack { get; set; }

        private NeuroniaViewModel viewModel;
        public AddTabFlyout(NeuroniaViewModel viewModel,Action<bool> addTabCallBack)
        {
            this.InitializeComponent();
            this.AddTabCallBack = addTabCallBack;
            this.viewModel = viewModel;

        }

        private void btn_addTab_Click(object sender, RoutedEventArgs e)
        {
            bool isOK = true;
            if (textTabName.Text == string.Empty)
            {
                isOK = false;
            }

            if (isOK == true)
            {
                var tab = new TimelineTab(textTabName.Text,viewModel.CallTabAction,viewModel.CallTimelineAction,viewModel.CallRowAction);
                viewModel.AddTimelineTabCommand.Execute(tab);
                AddTabCallBack(true);
                this.Hide();
            }
            else
            {
                AddTabCallBack(false);
            }
        }

    }
}
