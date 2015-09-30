using Windows.Storage.Streams;
using Windows.UI;
using GalaSoft.MvvmLight.Messaging;
using Neuronia.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ハブ ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=321224 を参照してください
using Neuronia.Hub.Common;
using Neuronia.Hub.UIThemeSet;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Diagnostics;

namespace Neuronia.View.Pages
{
    /// <summary>
    /// グループ化されたアイテムのコレクションを表示するページです。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private NeuroniaViewModel viewModel { get; set; }

        /// <summary>
        /// これは厳密に型指定されたビュー モデルに変更できます。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper は、ナビゲーションおよびプロセス継続時間管理を
        /// 支援するために、各ページで使用します。
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public SettingPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;

            Messenger.Default.Register<string>(this,"ChangeUIBrushImage",async str =>
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");

                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    var stream = await file.OpenAsync(FileAccessMode.Read);
                    var size = stream.Size;
                    byte[] bytes = new byte[size];
                    var reader = new DataReader(stream.GetInputStreamAt(0));
                    await reader.LoadAsync((uint) size);
                    reader.ReadBytes(bytes);
                    var uiBrush = new UIBrush();

                    uiBrush.IsImageEnable = true;
                    uiBrush.UIImage = bytes;
                    if (str == "MainBackground")
                    {
                        viewModel.Setting.AppTheme.MainBackground = uiBrush;
                    }
                    if (str == "BottomTweetBarBackground")
                    {
                        viewModel.Setting.AppTheme.BottomTweetBarBackground = uiBrush;
                    }
                    if (str == "BottomAppBarBackground")
                    {
                        viewModel.Setting.AppTheme.BottomAppBarBackground = uiBrush;
                    }
                    if (str == "TopAppBarBackground")
                    {
                        viewModel.Setting.AppTheme.TopAppBarBackground = uiBrush;
                    }
                    if (str == "SettingsFlyoutBackground")
                    {
                        viewModel.Setting.AppTheme.SettingsFlyoutBackground = uiBrush;
                    }
                }
                

            });
        }

        


        /// <summary>
        /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
        /// 再作成する場合は、保存状態も指定されます。
        /// </summary>
        /// <param name="sender">
        /// イベントのソース (通常、<see cref="NavigationHelper"/>)
        /// </param>
        /// <param name="e">このページが最初に要求されたときに
        /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
        /// 前のセッションでこのページによって保存された状態の辞書を提供する
        /// イベント データ。ページに初めてアクセスするとき、状態は null になります。</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: バインド可能なグループのコレクションを this.DefaultViewModel["Groups"] に割り当てます
        }

        #region NavigationHelper の登録

        /// このセクションに示したメソッドは、NavigationHelper がページの
        /// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
        /// 
        /// ページ固有のロジックは、
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// および <see cref="GridCS.Common.NavigationHelper.SaveState"/> のイベント ハンドラーに配置する必要があります。
        /// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
        /// ナビゲーション パラメーターを使用できます。

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = e.Parameter as NeuroniaViewModel;
            
            this.DataContext = viewModel;
            
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void backButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["TimelineFontSize"] = viewModel.Setting.TimelineFontSize;
            Application.Current.Resources["AppThemeBrush"] = new SolidColorBrush(viewModel.Setting.AppTheme.AppTheme);
            
            await viewModel.SaveSettingDataAsync();
            
            
            this.Frame.GoBack();
        }

        
        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
