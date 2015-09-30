using Neuronia.Common;
using Neuronia.Core.Tweets.List;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Timeline;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class EditTimelineFlyout : SettingsFlyout
    {

        NeuroniaViewModel viewModel;
        public EditTimelineFlyout(NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();

            this.viewModel = viewModel;
            this.DataContext = viewModel;
           
        }


        private void toggleTimelineFiltering_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleTimelineFiltering.IsOn)
            {
                stackPanelFiltering.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelFiltering.Visibility = Visibility.Collapsed;
            }
        }

       
    }
}
