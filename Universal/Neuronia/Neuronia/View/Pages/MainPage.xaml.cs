using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Neuronia.View.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //バックボタンをフックする
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            //アプリのライフサイクルをフック
            App.Current.Resuming += Current_Resuming;
            App.Current.Suspending += Current_Suspending;
        }

        //ページが読み込まれた時
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //HomePageにNavigateする
            frameContent.Navigate(typeof(HomePage));
        }

        //検索ボックスで検索しようとしたとき
        private void suggestBoxSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var text = args.QueryText;
        }

        //SplitViewのPaneに作ったナビゲーションボタンをおした時、各Pageに移動
        private void appButtonSetting_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(SettingPage));
            splitView.IsPaneOpen = false;
        }

        private void appButtonHome_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(HomePage));
            splitView.IsPaneOpen = false;
        }

        private void appButtonFavorite_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(FavoritePage));
            splitView.IsPaneOpen = false;
        }

        //アプリが一時停止しようとしたとき
        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //ここでアプリのデータや状態を保存するdeferral.Complete()が呼ばれるまではawaitしても待ってくれる
            //例 ApplicationData.Current.LocalSettings.Values["MyData"] = 1;

            deferral.Complete();
        }

        //アプリが再開しようとしたとき
        private void Current_Resuming(object sender, object e)
        {
            //ここでアプリのデータや状態を復元する
            /*例
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MyData"))
            {
                var data = ApplicationData.Current.LocalSettings.Values["MyData"];
            }
            */
        }

        //バックボタンが押された時
        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (frameContent.CanGoBack)
            {
                e.Handled = true;
                frameContent.GoBack();
            }
        }

    }


}
