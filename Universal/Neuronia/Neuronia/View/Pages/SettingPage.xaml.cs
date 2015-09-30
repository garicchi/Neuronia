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

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Neuronia.View.Pages
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        //設定適用ボタンを押した時
        private async void buttonSettingOK_Click(object sender, RoutedEventArgs e)
        {
            //このようなコードでコントロールの値を取得します
            //var text = textBoxSetting1.Text;
            //var isOK = toggleSwitchSetting2.IsOn;

            //このようなコードでダイアログを表示します
            var dialog = new MessageDialog("設定を適用しました");
            await dialog.ShowAsync();
        }
    }
}
