using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media.Animation;
using KeyBindManager;
using Neuronia.Common;
using Neuronia.Core.Data;
using Neuronia.Flyout;
using Neuronia.Hub.Detail.Parameter;
using Neuronia.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Neuronia.Hub.Common;
using Neuronia.Core.Post;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Tab;
using GalaSoft.MvvmLight.Messaging;
using Neuronia.Hub.Detail;
using Neuronia.Hub.Data;
using Neuronia.Core.Tweets;
using System.Net.Http;
using Neuronia.Hub.Timeline;
using Neuronia.Input;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.View.Flyout;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace Neuronia.View.Pages
{
    /// <summary>
    /// 多くのアプリケーションに共通の特性を指定する基本ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaElement mediaElementNotification;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        
        private NeuroniaViewModel viewModel;

        private TimelineState timelineState;

        private AuthenticationBrowser authenticationBrowser;

        private KeyManager textKeyManager { get; set; }

        private KeyManager textBottomKeyManager { get; set; }

        private KeyManager pageKeyManager { get; set; }
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

        
        public MainPage()
        {
           
            this.InitializeComponent();
           
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            

            this.SizeChanged+=MainPage_SizeChanged;
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            
            

            VisualStateManager.GoToState(this,VisualStateTimeline.MultiTimeline,true);
            
            timelineState = TimelineState.MultiTimeline;
            
            Application.Current.Suspending += async(s, e) =>
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                //TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
               
                 await viewModel.SaveTwitterDataAsync();
                deferral.Complete();
                
            };
            Application.Current.Resuming += (s,e) =>
            {
            };
           
            textKeyManager = new TweetKeyManager(textPost,KeyBindings.PostTextKeyBinder);
            textKeyManager.CommandList.Add("PostTweet",(bind)=>{
                viewModel.PostStatusCommand.Execute(null);
            });

            textBottomKeyManager = new TweetKeyManager(textPostBottom, KeyBindings.PostTextKeyBinder);
            textBottomKeyManager.CommandList.Add("PostTweet", (bind) =>
            {
                viewModel.PostStatusCommand.Execute(null);
            });

            pageKeyManager=new KeyManager(this,KeyBindings.PageTextKeyBinder);
            pageKeyManager.CommandList.Add("UpTab", (bind) =>
            {
                viewModel.NextTabCommand.Execute(null);
                ChangeTimelineSize(new Size(Window.Current.Bounds.Width,Window.Current.Bounds.Height));
                
            });
            pageKeyManager.CommandList.Add("DownTab", (bind) =>
            {
                viewModel.PrevTabCommand.Execute(null);
                ChangeTimelineSize(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
                
            });
            MessengerRegister();


            mediaElementNotification = new MediaElement();
            mediaElementNotification.AutoPlay = false;
            var uri = new Uri("ms-appx:///Assets/Sound/notification.wav");
            var file = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri).AsTask<StorageFile>().Result;
            var stream = file.OpenAsync(Windows.Storage.FileAccessMode.Read).AsTask<IRandomAccessStream>().Result;
            mediaElementNotification.SetSource(stream,file.ContentType);
           
            
        }

        private void MessengerRegister()
        {
            Messenger.Default.Register<string>(this, "GetAuthorizedUrl", str =>
            {
                authenticationBrowser.NavigateUrl(str);
            });

            Messenger.Default.Register<bool>(this, "AuthorizedCompleted",async bln =>
            {
                if (bln)
                {
                    authenticationBrowser.Close();
                }
                else
                {
                    MessageDialog dialog=new MessageDialog("ピンコードが違います","エラー");
                    await dialog.ShowAsync();
                    authenticationBrowser.Close();
                    Authentication();
                }
            });

            Messenger.Default.Register<string>(this, "BrowsUrl", str =>
            {
                PreviewBrowserFlyout flyout = new PreviewBrowserFlyout();
                flyout.Show(new Uri(str), Window.Current.Bounds.Width * 2 / 3);
            });

            Messenger.Default.Register<SearchDetail>(this, "ShowSearchDetail", sd =>
            {
                SearchDetailFlyout flyout = new SearchDetailFlyout(viewModel);
                flyout.onAddTimeline += (timelineName, searchWord, account) =>
                {
                    var timeline = new SearchTimeline(account, timelineName, viewModel.GetNowTab().TabTitle, searchWord,viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                    viewModel.AddTimelineCommand.Execute(timeline);
                };
                flyout.ShowIndependent();
            });

            Messenger.Default.Register<UserDetail>(this, "ShowUserDetail", ud =>
            {
                UserDetailFlyout flyout = new UserDetailFlyout(viewModel, () =>
                {
                    viewModel.DirectMessageDetailCommand.Execute(new DirectMessageDetailParameter(ud.OwnerAccount.UserInfomation.screen_name,new DirectMessage
                    {
                        recipient_screen_name = viewModel.UserDetail.UserInformation.screen_name
                    }));
                    DirectMessageFlyout d_flyout = new DirectMessageFlyout(this.viewModel);
                    d_flyout.ShowIndependent();
                });
                flyout.ShowIndependent();
            });

            Messenger.Default.Register<TweetDetail>(this, "ShowTweetDetail", td =>
            {
                TweetDetailFlyout flyout = new TweetDetailFlyout(viewModel);
                flyout.ShowIndependent();
            });

         
            Messenger.Default.Register<bool>(this, "InternalNotification",async tweet =>
            {
                 notificationBarStoryboard.Begin();
                if (viewModel.Setting.IsSoundEnable)
                {
                    mediaElementNotification.Play();
                }

            });

            Messenger.Default.Register<bool>(this, "ToastNotification", async tweet =>
            {
                

                ToastText toast = new ToastText(viewModel.NotifyMessage.Message, Windows.UI.Notifications.ToastTemplateType.ToastText01);
                toast.Show();

            });

            Messenger.Default.Register<NotificationState>(this, "ChangeNotificationState", state =>
            {

            });

            Messenger.Default.Register<PostStatusBase>(this, "OnTweetBegin", status =>
            {
   
            });
            Messenger.Default.Register<PostStatusBase>(this, "OnTweetFailed", status =>
            {
            });
            Messenger.Default.Register<PostStatusBase>(this, "OnTweetCompleted", status =>
            {
            });
            Messenger.Default.Register<HttpRequestException>(this, "OnHttpGetError", e =>
            {
                
            });
            Messenger.Default.Register<HttpRequestException>(this, "OnHttpPostError", e =>
            {
            });
            Messenger.Default.Register<HttpRequestException>(this, "OnUserStreamHttpError", e =>
            {
            });
            Messenger.Default.Register<HttpRequestException>(this, "OnFollowStreamHttpError", e =>
            {
            });
            Messenger.Default.Register<TimelineBase>(this, "EditTimeline", timeline =>
            {
                EditTimelineFlyout flyout = new EditTimelineFlyout(viewModel);
                flyout.ShowIndependent();
            });
            Messenger.Default.Register<TimelineBase>(this, "DeleteTimeline", timeline =>
            {
                ChangeTimelineSize(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
            });

            Messenger.Default.Register<TimelineTab>(this, "EditTimelineTab", tab =>
            {
                EditTabFlyout flyout = new EditTabFlyout(viewModel);
                flyout.ShowIndependent();
            });

            Messenger.Default.Register<TimelineTab>(this, "DeleteTimelineTab", tab =>
            {
                
            });

            Messenger.Default.Register<string>(this, "DeletePostImage", str =>
            {
      
            });
            Messenger.Default.Register<int>(this, "SetPostTextCursor", pos =>
            {
                
                if (timelineState == TimelineState.MultiTimeline)
                {
                    textPost.Select(pos, 0);
                }
                else
                {
                    textPostBottom.Select(pos, 0);
                }
            });

            Messenger.Default.Register<TimelineBase>(this, "AddTimeline", timeline =>
            {
                ChangeTimelineSize(new Size(Window.Current.Bounds.Width,Window.Current.Bounds.Height));
            });

            Messenger.Default.Register<DirectMessage>(this, "ShowDirectMessageDetail", dm =>
            {
                DirectMessageFlyout flyout = new DirectMessageFlyout(this.viewModel);
                flyout.ShowIndependent();
            });

            Messenger.Default.Register<TimelineTab>(this, "ChangeTab", tab =>
            {
                ChangeTimelineSize(new Size(Window.Current.Bounds.Width,Window.Current.Bounds.Height));
            });

            
        }

        
        private void ChangeTimelineSize(Size size)
        {
            
                if (size.Width <= 320)
                {
                    VisualStateManager.GoToState(this, VisualStateTimeline.FlipTimeline, true);
                    viewModel.ChangeTimelineWidthCommand.Execute(320.0);

                }
                else if (size.Width <= 1024 && size.Width > 320)
                {
                    VisualStateManager.GoToState(this, VisualStateTimeline.FlipTimeline, true);
                    viewModel.ChangeTimelineWidthCommand.Execute(size.Width);
                }
                else if (size.Width > 1024)
                {
                    VisualStateManager.GoToState(this,VisualStateTimeline.MultiTimeline,true);
                    int timelineNum = 1;

                    if (viewModel.GetNowTab() == null || viewModel.GetNowTab().TimelineList.Count == 0)
                    {
                        timelineNum = 1;
                    }
                    else
                    {
                        timelineNum = viewModel.GetNowTab().TimelineList.Count;
                    }
                    
                    viewModel.ChangeTimelineWidthCommand.Execute((size.Width - 320) / timelineNum-20);
                }
            
        }

        private void MainPage_SizeChanged(object s, SizeChangedEventArgs e)
        {
            if (viewModel!=null)
            {
                ChangeTimelineSize(e.NewSize);
            }
        }

        private void ChangeTimelineWidth(double width)
        {
            foreach (var tab in viewModel.TimelineListTab)
            {
                foreach (var timeline in tab.TimelineList)
                {
                    
                    timeline.TimelineWidth = width;
                }
            }
        }
        /// <summary>
        /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
        /// 再作成する場合は、保存状態も指定されます。
        /// </summary>
        /// <param name="sender">
        /// イベントのソース (通常、<see cref="NavigationHelper"/>)>
        /// </param>
        /// <param name="e">このページが最初に要求されたときに
        /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
        /// 前のセッションでこのページによって保存された状態の辞書を提供する
        /// セッション。ページに初めてアクセスするとき、状態は null になります。</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
           /* foreach (var i in await ApplicationData.Current.LocalFolder.GetFilesAsync())
            {
                i.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
                
            */

        }

        /// <summary>
        /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
        /// このページに関連付けられた状態を保存します。値は、
        /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
        /// </summary>
        /// <param name="sender">イベントのソース (通常、<see cref="NavigationHelper"/>)</param>
        /// <param name="e">シリアル化可能な状態で作成される空のディクショナリを提供するイベント データ
        ///。</param>
        private async void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            
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
            navigationHelper.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is NeuroniaViewModel&&e.NavigationMode!=NavigationMode.Back)
            {
                this.viewModel = e.Parameter as NeuroniaViewModel;
                this.DataContext = viewModel;

                if (viewModel.IsFirstNavigate)
                {
                    viewModel.IsFirstNavigate = false;
                    if (viewModel.IsFirstLaunch)
                    {
                        Authentication();
                    }
                    else
                    {
                        for (int i = 0; i < listViewTab.Items.Count; i++)
                        {
                            if ((listViewTab.Items[i] as TimelineTab).TabTitle == viewModel.GetNowTab().TabTitle)
                            {
                                listViewTab.SelectedIndex = i;
                            }
                        }
                        ChangeTimelineSize(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
                    }
                }
                else
                {

                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        
        private void Authentication()
        {
            
            authenticationBrowser = new AuthenticationBrowser();
            authenticationBrowser.PostAuthorizedPin+=(pin)=>
            {
                viewModel.PinAuthCommand.Execute(pin);
            };

            authenticationBrowser.Show();
            viewModel.BeginAuthCommand.Execute(null);
        }

        private async void btnPost_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PostStatusCommand.Execute(null);
        }

        
        

        private void btnPostBottom_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PostStatusCommand.Execute(null);
        }

        private void appBarAddAcount_Click(object sender, RoutedEventArgs e)
        {
            Authentication();
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
             
            
        }

        

        private void btnOpenAppBar_Click(object sender, RoutedEventArgs e)
        {
            appBar_Bottom.IsOpen = true;
            appBar_Top.IsOpen = true;
        }


        private void listViewTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
        }


        
        private async void appBarAddTimeline_Click(object sender, RoutedEventArgs e)
        {
            
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
            if (viewModel.TimelineListTab.Count == 0)
            {
                MessageDialog dialog = new MessageDialog("タイムラインを追加するタブがありません","エラー");
                await dialog.ShowAsync();
                return;
            }
            if (viewModel.NowTimelineList.Count==4)
            {
                MessageDialog dialog = new MessageDialog("タイムラインは一つのタブにつき4つまでです", "エラー");
                await dialog.ShowAsync();
                return;
            }
            CreateTimelineFlyout time = new CreateTimelineFlyout(viewModel,async controlModel =>
            {
                
            });
            time.Show();
        }

        private void appBarAddTab_Click(object sender, RoutedEventArgs e)
        {
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
            AddTabFlyout time = new AddTabFlyout(viewModel,async bln =>
            {
                               
                if (bln == false)
                {
                    MessageDialog dialog = new MessageDialog("タブ名が競合しています","入力エラー");
                    await dialog.ShowAsync();
                }

            });
            time.Show();
        }

        private void appBarButtonDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
            DeleteAccountFlyout time = new DeleteAccountFlyout(viewModel,() =>
            {
                
            });
            time.Show();
        }

        

        private async Task SetPostImageAsync(StorageFile file)
        {
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var size = stream.Size;
            byte[] bytes = new byte[size];
            var reader = new DataReader(stream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)size);
            reader.ReadBytes(bytes);
            var media = new PostMedia();
            media.Data = bytes;
            media.FileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            viewModel.SetPostImageCommand.Execute(media);
        }

        private async void menuFlyoutItemMediaFromCamera_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var camUI = new CameraCaptureUI();
                StorageFile file = await camUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (file != null)
                {
                    await SetPostImageAsync(file);
                }
            }
            catch (Exception)
            {
                ToastText text=new ToastText("カメラの起動に失敗しました",ToastTemplateType.ToastText01);
                text.Show();
            }

        }

        private async void menuFlyoutItemMediaFromLibrary_Click(object sender, RoutedEventArgs e)
        {
            try{
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                await SetPostImageAsync(file);
            }
            }
            catch (Exception)
            {
                ToastText text=new ToastText("メディアの取得に失敗しました",ToastTemplateType.ToastText01);
                text.Show();
            }
        }

        private void menuFlyoutItemMediaDelete_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePostImageCommand.Execute(null);
            
        }
        

        

        private void appBarButtonSearch_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
            SearchDetailFlyout flyout = new SearchDetailFlyout(viewModel);
            flyout.onAddTimeline += (timelineName, searchWord, account) =>
            {
                var timeline = new SearchTimeline(account,timelineName,viewModel.GetNowTab().TabTitle,searchWord,viewModel.Setting,viewModel.CallTimelineAction,viewModel.CallRowAction);
                viewModel.AddTimelineCommand.Execute(timeline);
            };
            flyout.Show();

        }

        private void appBarButtonReport_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Show();
        }

        private void gridViewAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            var account = (e.ClickedItem as TwitterAccount);
            viewModel.ToggleAccountActivityCommand.Execute(account);
        }

        private void gridViewAccountTop_ItemClick(object sender, ItemClickEventArgs e)
        {
            var account = (e.ClickedItem as TwitterAccount);
            viewModel.ToggleAccountActivityCommand.Execute(account);
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void listViewTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            var t = e.ClickedItem as TimelineTab;
            if (t != null)
            {
                
                viewModel.ChangeTabCommand.Execute(t);
            }
        }

        private void listViewSuggest_ItemClick(object sender, ItemClickEventArgs e)
        {
            viewModel.SelectSuggestCommand.Execute(e.ClickedItem.ToString());
        }

        private void appBarButtonMentionNeuronia_Click(object sender, RoutedEventArgs e)
        {
            appBar_Bottom.IsOpen = false;
            appBar_Top.IsOpen = false;
            viewModel.TwitterUIComponent.SetPostText(" #Neuronia http://neuronia.garicchi.com");
        }

        private void appBarButtonSetting_Click(object sender, RoutedEventArgs e)
        {
          
           this.Frame.Navigate(typeof (SettingPage), viewModel);
        }

        private async void menuFlyoutItemMediaFromClipboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var image = await Clipboard.GetContent().GetBitmapAsync();
                
                var stream = await image.OpenReadAsync();
                var size = stream.Size;
                byte[] bytes = new byte[size];
                var reader = new DataReader(stream.GetInputStreamAt(0));
                await reader.LoadAsync((uint) size);
                reader.ReadBytes(bytes);

               

                var media = new PostMedia();
                media.Data = bytes;
                media.FileName = DateTime.Now.ToString("yyyyMMddhhmmss");

                viewModel.SetPostImageCommand.Execute(media);
            }
            catch (Exception)
            {
                ToastText text=new ToastText("クリップボードの取得に失敗しました",ToastTemplateType.ToastText01);
                text.Show();
            }
        }

        private async void appBarButtonReview_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=2c9802e8-9488-4db4-843f-bad93877c5e6"));
        }

        

        

       
    }

    
}
