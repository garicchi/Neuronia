using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Microsoft.WindowsAzure.MobileServices;
using Neuronia.Flyout;
using Neuronia.Report;
using Neuronia.Utility;
using Neuronia.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Neuronia.Core.Common;
using Neuronia.Core.Data;
using Neuronia.Hub.Common;

// 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください
using Neuronia.View.Pages;

namespace Neuronia
{
    /// <summary>
    /// 既定の Application クラスに対してアプリケーション独自の動作を実装します。
    /// </summary>
    sealed partial class App : Application
    {
       

        public static MobileServiceClient MobileService = new MobileServiceClient(
    "https://neuronia.azure-mobile.net/",
    "BrJDKvJkkdmbVLBPmjMKKKEgrLXNDd61"
);
        /// <summary>
        /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        /// 最初の行であり、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            
            this.Suspending += OnSuspending;
            
            this.UnhandledException += (s, e) =>
            {
                /*MobileService.GetTable<ExceptionItem>().InsertAsync(new ExceptionItem
                {
                    Exception = e.GetType().ToString(),Message = e.Message,Sender = e.Exception.Source.ToString()
                }).Wait(TimeSpan.FromSeconds(10));
                */
            };
            

            
        }

        /// <summary>
        /// アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。他のエントリ ポイントは、
        /// アプリケーションが特定のファイルを開くために呼び出されたときなどに使用されます。
        /// </summary>
        /// <param name="e">起動要求とプロセスの詳細を表示します。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            AsyncOAuthInitializer.Initialize();
            
            
           
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // ウィンドウに既にコンテンツが表示されている場合は、アプリケーションの初期化を繰り返さずに、
            // ウィンドウがアクティブであることだけを確認してください
            if (rootFrame == null)
            {
                // ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
                rootFrame = new Frame();
                // 既定の言語を設定します
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 以前中断したアプリケーションから状態を読み込みます。
                }

                // フレームを現在のウィンドウに配置します
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                
            
                // ナビゲーションの履歴スタックが復元されていない場合、最初のページに移動します。
                // このとき、必要な情報をナビゲーション パラメーターとして渡して、新しいページを
                // 作成します

                Application.Current.Resources["ProgressBarForegroundThemeBrush"] = Application.Current.Resources["AppThemeBrush"];
                Application.Current.Resources["ProgressBarIndeterminateForegroundThemeBrush"] = Application.Current.Resources["AppThemeBrush"];
                Application.Current.Resources["SearchBoxButtonBackgroundThemeBrush"] = Application.Current.Resources["AppThemeBrush"];
                Application.Current.Resources["ToggleSwitchCurtainBackgroundThemeBrush"] = Application.Current.Resources["AppThemeBrush"];
                Application.Current.Resources["TextSelectionHighlightColorThemeBrush"] = Application.Current.Resources["AppThemeBrush"];

                

                rootFrame.Navigate(typeof(LoadPage));
            }
            
            // 現在のウィンドウがアクティブであることを確認します
            Window.Current.Activate();
        }

        /// <summary>
        /// 特定のページへの移動が失敗したときに呼び出されます
        /// </summary>
        /// <param name="sender">移動に失敗したフレーム</param>
        /// <param name="e">ナビゲーション エラーの詳細</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// アプリケーションの実行が中断されたときに呼び出されます。アプリケーションの状態は、
        /// アプリケーションが終了されるのか、メモリの内容がそのままで再開されるのか
        /// わからない状態で保存されます。
        /// </summary>
        /// <param name="sender">中断要求の送信元。</param>
        /// <param name="e">中断要求の詳細。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
            
            
            deferral.Complete();
        }

        

        
    }
}
