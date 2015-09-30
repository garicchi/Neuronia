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
    public sealed partial class BugReportFlyout : SettingsFlyout
    {
        public BugReportFlyout()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxMain.Text!=string.Empty)
            {
                await App.MobileService.GetTable<ReportItem>().InsertAsync(new ReportItem()
                {
                    Text=textBoxMain.Text
                });
            }
            MessageDialog dialog = new MessageDialog("ご意見、ご感想ありがとうございました","Thanks!");
            await dialog.ShowAsync();
            this.Hide();

        }

        
    }

    public class ReportItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
