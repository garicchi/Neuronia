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

namespace Neuronia.Flyout
{
    public sealed partial class EditTabFlyout : SettingsFlyout
    {

        NeuroniaViewModel viewModel;
        public EditTabFlyout(NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();

            this.viewModel = viewModel;
            this.DataContext = viewModel;
        }

        
    }
}
