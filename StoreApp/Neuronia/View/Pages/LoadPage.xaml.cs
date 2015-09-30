using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください
using GalaSoft.MvvmLight.Messaging;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;
using System.Threading.Tasks;
using Neuronia.Hub.Tab;
using Neuronia.Hub.UIThemeSet;
using Windows.Storage.Streams;
using Neuronia.Core.Data;
using Windows.UI.ApplicationSettings;
using Windows.System;
using Neuronia.Core.Twitter;

namespace Neuronia.View.Pages
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class LoadPage : Page
    {
        private NeuroniaViewModel viewModel;
        private Storyboard progressStoryboard;
        private DispatcherTimer firstLaunchTimer;
        public LoadPage()
        {
            this.InitializeComponent();
            firstLaunchTimer=new DispatcherTimer();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var cData = new ConsumerData(App.Current.Resources["ConsumerKey"].ToString(), App.Current.Resources["ConsumerSecret"].ToString());


            viewModel = new NeuroniaViewModel(Window.Current.Dispatcher, cData);
            this.DataContext = viewModel;

            SettingsPane.GetForCurrentView().CommandsRequested += (s, arg) =>
            {
                arg.Request.ApplicationCommands.Add(new SettingsCommand("Setting", "設定", (a) =>
                {
                    
                    this.Frame.Navigate(typeof(SettingPage), viewModel);
                }));

                arg.Request.ApplicationCommands.Add(new SettingsCommand("TwitterReport", "作者に直接文句をいう", async (a) =>
                {
                    await Launcher.LaunchUriAsync(new Uri("http://www.twitter.com/garicchi"));
                }));

                arg.Request.ApplicationCommands.Add(new SettingsCommand("ModeInformation", "MoreInformation", async (a) =>
                {
                    await Launcher.LaunchUriAsync(new Uri("http://neuronia.garicchi.com"));
                }));
                
                arg.Request.ApplicationCommands.Add(new SettingsCommand("PrivacyPolicy", "プライバシーポリシー", async (a) =>
                {
                    await Launcher.LaunchUriAsync(new Uri("http://garicchi.hatenablog.jp/entry/2014/02/08/151814"));
                }));
            };
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.IsFirstNavigate = true;
            progressStoryboard = Resources["ProgressStoryboard"] as Storyboard;
            progressStoryboard.Begin();
            //ApplicationData.Current.LocalSettings.Values.Clear();
            if (SaveData.IsFirstVersion())
            {
                MessageDialog dialog=new MessageDialog("Neuronia初期バージョンからつかって頂いてありがとうございます！！大変申し訳無いのですが、セーブデータの構造が変わったため、アプリを一度削除して入れなおしていただけるとたすかります","お願い");
                await dialog.ShowAsync();
                Application.Current.Exit();
            }

           
            
            await viewModel.LoadSettingDataAsync(bln => { });
            
            await viewModel.LoadTwitterDataAsync(async bln =>
            {
                await RuntimeProxy.ProxySetting.SetProxyAsync(new Uri("http://google.com"));
                if (viewModel.IsFirstLaunch)
                {

                    viewModel.Setting.SettingInitialize();
                    viewModel.Setting.SettingConstInitialize();
                    firstLaunchTimer.Interval = TimeSpan.FromSeconds(3);
                    firstLaunchTimer.Tick += (s, ee) =>
                    {
                        this.Frame.Navigate(typeof(MainPage), this.DataContext as NeuroniaViewModel);
                        firstLaunchTimer.Stop();
                    };
                    firstLaunchTimer.Start();
                }
                else
                {
                    viewModel.Setting.SettingConstInitialize();
                    Application.Current.Resources["AppThemeBrush"] = new SolidColorBrush(viewModel.Setting.AppTheme.AppTheme);
                    Application.Current.Resources["TimelineFontSize"] = viewModel.Setting.TimelineFontSize;

                    this.Frame.Navigate(typeof(MainPage), this.DataContext as NeuroniaViewModel);
                }
            });

            
        }
    }
}
