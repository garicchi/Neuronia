using System.Runtime.InteropServices;
using Windows.ApplicationModel.Store;
using Windows.Devices.PointOfService;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Tab;
using Neuronia.Hub.Timeline;
using Neuronia.Core.Tweets;
using Windows.UI.Core;
using Neuronia.Core.Data;
using AsyncOAuth;
using Neuronia.Core.Post;
using Neuronia.Hub.Detail;
using Neuronia.Hub.Row;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;
using Neuronia.Hub.Data;
using System.Net.Http;
using Neuronia.Core.TwitterStream;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Hub.Detail.Parameter;
using Windows.UI.Xaml;
using Neuronia.Hub.UIThemeSet;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace Neuronia.Hub.Common
{

    public class NeuroniaViewModel : ViewModelBase
    {

        #region Model


        TwitterUIComponent twitterUIComponent;

        public TwitterUIComponent TwitterUIComponent
        {
            get { return twitterUIComponent; }
            set { twitterUIComponent = value; RaisePropertyChanged("TwitterUIModel"); }
        }

        TwitterAuthorizer authorizer;

        public TwitterAuthorizer Authorizer
        {
            get { return authorizer; }
            set { authorizer = value; }
        }


        private ObservableCollection<TwitterAccount> accountList;
        public ObservableCollection<TwitterAccount> AccountList
        {
            get { return this.accountList; }
            set { accountList = value; RaisePropertyChanged("AccountList"); }
        }

        private ObservableCollection<TimelineTab> timelineListTab;

        public ObservableCollection<TimelineTab> TimelineListTab
        {
            get { return timelineListTab; }
            set { timelineListTab = value; RaisePropertyChanged("TimelineListTab"); }
        }

        private ObservableCollection<TimelineBase> nowTimelineList;

        public ObservableCollection<TimelineBase> NowTimelineList
        {
            get { return nowTimelineList; }
            set { nowTimelineList = value; RaisePropertyChanged("NowTimelineList"); }
        }


        private ConsumerData consumerData;

        public ConsumerData ConsumerData
        {
            get { return consumerData; }
            set { consumerData = value; }
        }

        private SearchDetail searchDetail;

        public SearchDetail SearchDetail
        {
            get { return searchDetail; }
            set { searchDetail = value; RaisePropertyChanged("SearchDetail"); }
        }

        private TweetDetail tweetDetail;

        public TweetDetail TweetDetail
        {
            get { return tweetDetail; }
            set { tweetDetail = value; RaisePropertyChanged("TweetDetail"); }
        }

        private UserDetail userDetail;

        public UserDetail UserDetail
        {
            get { return userDetail; }
            set { userDetail = value; RaisePropertyChanged("UserDetail");}
        }

        private DirectMessageDetail directMesssageDetail;

        public DirectMessageDetail DMDetail
        {
            get { return directMesssageDetail; }
            set { directMesssageDetail = value; RaisePropertyChanged("DMDetail"); }
        }

        private TimelineBase nowEditTimeline;

        public TimelineBase NowEditTimeline
        {
            get { return nowEditTimeline; }
            set { nowEditTimeline = value; RaisePropertyChanged("NowEditTimeline"); }
        }

        private TimelineTab nowEditTimelineTab;

        public TimelineTab NowEditTimelineTab
        {
            get { return nowEditTimelineTab; }
            set { nowEditTimelineTab = value; RaisePropertyChanged("NowEditTimelineTab"); }
        }

        private SettingData setting;
        
        public SettingData Setting
        {
            get { return setting; }
            set { setting = value; RaisePropertyChanged("Setting"); }
        }

        

        private bool isFirstLaunch;

        public bool IsFirstLaunch
        {
            get { return isFirstLaunch; }
            set { isFirstLaunch = value; RaisePropertyChanged("IsFirstLaunch");}
        }

        public bool IsFirstNavigate { get; set; }
        
        

        private string connectionStatusStr;

        public string ConnectionStatusStr
        {
            get { return connectionStatusStr; }
            set { connectionStatusStr = value; RaisePropertyChanged("ConnectionStatusStr"); }
        }

        private NotificationMessage notifyMessage;

        public NotificationMessage NotifyMessage
        {
            get { return notifyMessage; }
            set { notifyMessage = value; RaisePropertyChanged("NotifyMessage"); }
        }
        

       
        
	

        private double timelineWidth;

        public double TimelineWidth
        {
            get { return timelineWidth; }
            set { timelineWidth = value; RaisePropertyChanged("TimelineWidth");}
        }

        private DateTime nowTime;

        public DateTime NowTime
        {
            get { return nowTime; }
            set { nowTime = value; RaisePropertyChanged("NowTime");}
        }

        private string nowTimeStr;

        public string NowTimeStr
        {
            get { return nowTimeStr; }
            set { nowTimeStr = value; RaisePropertyChanged("NowTimeStr");}
        }

        private bool isPurchase;

        public bool IsPurchase
        {
            get { return isPurchase; }
            set
            {
                isPurchase = value;
                RaisePropertyChanged("IsPurchase");
            }
        }


        public LicenseInformation LicenseInfo { get; set; }

        private int restTimerCounter;

        private DispatcherTimer RestTimer;

        #endregion

        #region Command

        public RelayCommand<TwitterAccount> AddAccountCommand { get; set; }

        public RelayCommand<TwitterAccount> DeleteAccountCommand { get; set; }

        public RelayCommand PostStatusCommand { get; set; }

        public RelayCommand<PostMedia> SetPostImageCommand { get; set; }

        public RelayCommand DeletePostImageCommand { get; set; }

        public RelayCommand<TimelineTab> AddTimelineTabCommand { get; set; }

        public RelayCommand<TimelineTab> DeleteTimelineTabCommand { get; set; }

        public RelayCommand<TimelineTab> ChangeTabCommand { get; set; }

        public RelayCommand<TimelineBase> AddTimelineCommand { get; set; }

        public RelayCommand<TimelineBase> DeleteTimelineCommand { get; set; }

        public RelayCommand<TimelineTab> EditTimelineTabCommand { get; set; }

        public RelayCommand<TimelineBase> EditTimelineCommand { get; set; }

        public RelayCommand<Tweet> FavoriteCommand { get; set; }

        public RelayCommand<Tweet> RetweetCommand { get; set; }

        public RelayCommand<string> PinAuthCommand { get; set; }

        public RelayCommand BeginAuthCommand { get; set; }

        public RelayCommand<Tweet> QuoteCommand { get; set; }

        public RelayCommand<TweetDetailParameter> TweetDetailCommand { get; set; }

        public RelayCommand<Tweet> ReplyCommand { get; set; }

        public RelayCommand<Tweet> DescriptionDommand { get; set; }

        public RelayCommand<UserDetailParameter> UserDetailCommand { get; set; }

        public RelayCommand<DirectMessageDetailParameter> DirectMessageDetailCommand { get; set; }

        public RelayCommand<SearchDetailParameter> SearchCommand { get; set; }

        public RelayCommand<string> BrowseCommand { get; set; }

        public RelayCommand NextTabCommand { get; set; }

        public RelayCommand PrevTabCommand { get; set; }

        public RelayCommand<string> SelectSuggestCommand { get; set; }

        public RelayCommand<TwitterAccount> ToggleAccountActivityCommand { get; set; }

        public RelayCommand<SendDirectMessage> SendDirectMessageCommand { get; set; }

        public RelayCommand<double> ChangeTimelineWidthCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        public RelayCommand<string> AddSuggestPostText { get; set; }

        public RelayCommand<string> ChangeUIBrushImageCommand { get; set; }

        public RelayCommand ResetThemeSettingCommand { get; set; }

        public RelayCommand PurchaseApplicationThemeCommand { get; set; }

        public RelayCommand<string> AddMuteAccountCommand { get; set; }

        public RelayCommand<string> CopyClipBoardCommand { get; set; }
        
        #endregion


        #region ModelCallbackMethod

        public async void CallRowAction(RowAction action)
        {
            switch (action.ActionType)
            {
                case RowActionType.Favorite:
                    FavoriteCommand.Execute(action.Parameter as Tweet);
                    break;
                case RowActionType.Retweet:
                    RetweetCommand.Execute(action.Parameter as Tweet);
                    break;
                case RowActionType.Quote:
                    QuoteCommand.Execute(action.Parameter as Tweet);
                    break;
                case RowActionType.Reply:
                    ReplyCommand.Execute(action.Parameter as Tweet);
                    break;
                case RowActionType.TweetDetail:
                    var tweetParameter = action.Parameter as TweetDetailParameter;
                    
                    TweetDetailCommand.Execute(tweetParameter);
                    break;
                case RowActionType.UserDetail:
                    var userParameter = action.Parameter as UserDetailParameter;
                    
                    UserDetailCommand.Execute(userParameter);
                    break;
                case RowActionType.Search:
                    var searchParameter = action.Parameter as SearchDetailParameter;
                    SearchCommand.Execute(searchParameter);
                    break;
                case RowActionType.Browse:
                    BrowseCommand.Execute(action.Parameter.ToString());
                    break;
                case RowActionType.Description:
                    DescriptionDommand.Execute(action.Parameter as Tweet);
                    break;
                case RowActionType.Delete:
                    var tweet = action.Parameter as Tweet;
                    var client=AccountList.Where(q => q.UserInfomation.screen_name == tweet.user.screen_name).Select(q=>q.TwitterClient).Single();
                    await client.DestroyStatusAsync(tweet);
                    break;
                case RowActionType.DirectMessage:
                    var dmParameter = action.Parameter as DirectMessageDetailParameter;
                    
                    DirectMessageDetailCommand.Execute(dmParameter);
                    break;
                case RowActionType.Share:
                    break;
                case RowActionType.SavePreviewImage:
                    Uri uri = action.Parameter as Uri;
                    FolderPicker picker = new FolderPicker();
                    picker.FileTypeFilter.Add(".png");
                    picker.FileTypeFilter.Add(".jpg");
                    picker.FileTypeFilter.Add(".bmp");
                    picker.FileTypeFilter.Add(".gif");
                    var folder=await picker.PickSingleFolderAsync();
                    if (folder != null)
                    {
                        HttpClient hClient = new HttpClient();
                        var bytes = await hClient.GetByteArrayAsync(uri);
                        var file =await folder.CreateFileAsync(DateTime.Now.ToString("yyyMMhhmmss")+".png");
                        
                        DataWriter writer = new DataWriter(await file.OpenAsync(FileAccessMode.ReadWrite));
                        writer.WriteBytes(bytes);
                        await writer.StoreAsync();
                        await writer.FlushAsync();
                        writer.DetachStream();

                        await NotificationAsync(new NotificationMessage
                        {
                            Message = "画像を保存しました",
                            TweetMessage = Tweet.ZeroTweet
                        });
                    }
                    

                    break;
            }
        }

        public async void CallTimelineAction(TimelineAction action)
        {
            switch (action.ActionType)
            {
                case TimelineActionType.Edit:
                    
                    EditTimelineCommand.Execute(action.Parameter as TimelineBase);
                    
                    break;
                case TimelineActionType.Delete:
                    DeleteTimelineCommand.Execute(action.Parameter as TimelineBase);
                    break;
                case TimelineActionType.NewNotification:
                    await NotificationAsync((action.Parameter as NotificationMessage));
                    break;
            }
        }

        private async Task NotificationAsync(NotificationMessage str)
        {
    
            await SharedDispatcher.RunAsync(() =>
            {
                if (Setting.IsNotificationEnable)
                {
                    NotifyMessage = str;
                    if (Setting.IsToastNotification)
                    {
                        Messenger.Default.Send<bool>(true, "ToastNotification");
                    }
                    if (Setting.IsInternalNotification)
                    {
                        Messenger.Default.Send<bool>(true, "InternalNotification");
                    }
                }
            });

        }

        public void CallTabAction(TabAction action)
        {
            switch (action.ActionType)
            {
                case TabActionType.Edit:
                    EditTimelineTabCommand.Execute(action.Parameter as TimelineTab);
                    break;
                case TabActionType.Delete:
                    DeleteTimelineTabCommand.Execute(action.Parameter as TimelineTab);
                    break;
            }
        }
        #endregion


        public async Task DispatcherRunAsync(DispatchedHandler handler)
        {
            await SharedDispatcher.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, handler);
        }


        public NeuroniaViewModel(CoreDispatcher dispatcher, ConsumerData consumerData)
            :base(Messenger.Default)
        {
            SharedDispatcher.Dispatcher = dispatcher;
            twitterUIComponent = new TwitterUIComponent();
            accountList = new ObservableCollection<TwitterAccount>();
            TimelineListTab = new ObservableCollection<TimelineTab>();
            NowTimelineList = new ObservableCollection<TimelineBase>();
            SearchDetail = new SearchDetail();
            Setting = new SettingData();
            TweetDetail = new TweetDetail(new TimelineRow(Tweet.ZeroTweet,"",Setting,CallRowAction));
            DMDetail = new DirectMessageDetail();
            UserDetail = new UserDetail();
            NotifyMessage = new NotificationMessage();
            IsFirstNavigate = true;
            this.consumerData = consumerData;
            this.connectionStatusStr = "";
            Authorizer = new TwitterAuthorizer();
            restTimerCounter = 0;
            LicenseInfo = CurrentApp.LicenseInformation;
           
            RestTimer = new DispatcherTimer();
            RestTimer.Interval = TimeSpan.FromMinutes(1);
            RestTimer.Tick += (s, e) => Task.Run(async() =>
            {
                await SharedDispatcher.RunAsync(() =>
                {
                    NowTime = DateTime.Now;
                    NowTimeStr = NowTime.ToString("HH:mm");
                });
                
                if (restTimerCounter%2 == 0)
                {
                    foreach (var tab in TimelineListTab)
                    {
                        foreach (var t in tab.TimelineList)
                        {
                            await t.RestUpdate();
                        }
                    }
                }
                if (restTimerCounter%10 == 0)
                {
                    TwitterUIComponent.MentionSuggestSourceList.Clear();
                    TwitterUIComponent.HashSuggestSourceList.Clear();
                    foreach (var tab in TimelineListTab)
                    {
                        foreach (var t in tab.TimelineList)
                        {
                            foreach (var ac in (await t.GetTimelineAccountListAsync()))
                            {
                                TwitterUIComponent.MentionSuggestSourceList.Add(ac);
                            }
                            foreach (var ac in (await t.GetTimelineHashTagListAsync()))
                            {
                                TwitterUIComponent.HashSuggestSourceList.Add(ac);
                            }
                        }
                    }
                }
                restTimerCounter++;
                if (restTimerCounter > 100)
                {
                    restTimerCounter = 0;
                }
                
            });
            RestTimer.Start();
            CommandInitialize();
            timelineWidth = 320;
            IsFirstLaunch = true;
            if (LicenseInfo.ProductLicenses["ApplicationTheme"].IsActive)
            {
                IsPurchase = true;
            }
            else
            {
                IsPurchase = false;
            }
            
        }

        private void CommandInitialize()
        {
            AddAccountCommand = new RelayCommand<TwitterAccount>(account =>
            {
               
                account.OnTweetBegin += (status) => MessengerInstance.Send<PostStatusBase>(status,"OnTweetBegin");
                account.OnTweetFailed += async(status) =>
                {
                    await NotificationAsync(new NotificationMessage{
                        TweetMessage=Tweet.ZeroTweet,
                        Message="ツイートの送信に失敗しました"
                    });
                    MessengerInstance.Send<PostStatusBase>(status, "OnTweetFailed");
                };
                account.OnTweetCompleted += (status) => MessengerInstance.Send<PostStatusBase>(status, "OnTweetCompleted");
                account.OnHttpGetError += async(e) =>
                {
                    await NotificationAsync(new NotificationMessage
                    {
                        TweetMessage = Tweet.ZeroTweet,
                        Message = e.Message
                    });
                   
                    MessengerInstance.Send<HttpRequestException>(e, "OnHttpGetError");
                };
                account.OnHttpPostError +=async (e) =>
                {
                    await NotificationAsync(new NotificationMessage
                    {
                        TweetMessage = Tweet.ZeroTweet,
                        Message = e.Message
                    });
                    MessengerInstance.Send<HttpRequestException>(e, "OnHttpPostError");
                };
                account.OnUserStreamHttpError += async(e) =>
                {
                    await NotificationAsync(new NotificationMessage
                    {
                        TweetMessage = Tweet.ZeroTweet,
                        Message = e.Message
                    });
                    MessengerInstance.Send<HttpRequestException>(e, "OnUserStreamHttpError");
                };
                account.OnFollowStreamHttpError += async(e) =>
                {
                    await NotificationAsync(new NotificationMessage
                    {
                        TweetMessage = Tweet.ZeroTweet,
                        Message = e.Message
                    });
                    MessengerInstance.Send<HttpRequestException>(e, "OnFollowStreamHttpError");
                };
                account.ChangeUserStreamEvent += async(state) =>
                {
                    switch (state)
                    {
                        case StreamState.Connect:
                           // await NotificationAsync("Stream Connected");
                            await SharedDispatcher.RunAsync(() =>
                            {
                                ConnectionStatusStr = "Stream Connected";
                            });
                            break;
                        case StreamState.TryConnect:
                           // await NotificationAsync("Stream TryConnected");
                            await SharedDispatcher.RunAsync(() =>
                            {
                                ConnectionStatusStr = "Stream TryConnected";
                            });
                            break;
                        case StreamState.DisConnect:
                           // await NotificationAsync("Stream DisConnect");
                            await SharedDispatcher.RunAsync(() =>
                            {
                                ConnectionStatusStr = "Stream DisConnected";
                            });
                            break;
                    }
                };

                this.AccountList.Add(account);
               
            });

            DeleteAccountCommand = new RelayCommand<TwitterAccount>(account =>
            {
                this.AccountList.Remove(account);
                
            });

            PostStatusCommand = new RelayCommand(async() =>
            {
                if (!AccountList.Where(q => q.IsActive == true).Select(q => q).Any())
                {
                        Tweet tweet = Tweet.ZeroTweet;
                        await NotificationAsync(new NotificationMessage{
                            TweetMessage = Tweet.ZeroTweet,
                            Message="アカウントが選択されていません"
                        });
                        
                }
                else
                {
                    PostStatusBase status = TwitterUIComponent.GetPostStatus();
                    status.Status += " " + Setting.Footer;
                    DeletePostImageCommand.Execute(null);
                    TwitterUIComponent.ResetPostText();

                    await ActionSelectedAccountAsync(async (client) =>
                    {
                        if (status is PostStatus)
                        {
                            await client.UpdateStatusAsync(status as PostStatus);
                        }
                        else if (status is PostStatusWithReply)
                        {
                            await client.UpdateStatusAsync(status as PostStatusWithReply);
                        }
                        else if (status is PostStatusMedia)
                        {
                            await client.UpdateStatusWithMediaAsync(status as PostStatusMedia);
                        }
                        else if (status is PostStatusMediaWithReply)
                        {
                            await client.UpdateStatusWithMediaAsync(status as PostStatusMediaWithReply);
                        }

                    });
                }
                
                
               

            });


            AddTimelineTabCommand = new RelayCommand<TimelineTab>(tab =>
            {
                AddTimelineTab(tab);
                
            });

            DeleteTimelineTabCommand = new RelayCommand<TimelineTab>(tab =>
            {
                foreach (TimelineBase timeline in tab.TimelineList)
                {
                    timeline.DeleteTimeLine();
                }
                TimelineListTab.Remove(tab);
                if (TimelineListTab.Count > 0)
                {
                    var t = TimelineListTab.Take(1).Single();
                    ChangeTabAsync(t);
                }
                MessengerInstance.Send<TimelineTab>(tab,"DeleteTimelineTab");
                
            });

            AddTimelineCommand = new RelayCommand<TimelineBase>(timeline =>
            {
                GetNowTab().TimelineList.Add(timeline);
                timeline.AddTimeLine();
                ResetTimeline();
                MessengerInstance.Send<TimelineBase>(timeline,"AddTimeline");
 
            });

            DeleteTimelineCommand = new RelayCommand<TimelineBase>(timeline =>
            {
                GetNowTab().TimelineList.Remove(timeline);
                timeline.DeleteTimeLine();
                ResetTimeline();
                MessengerInstance.Send<TimelineBase>(timeline,"DeleteTimeline");
            });

            ChangeTabCommand = new RelayCommand<TimelineTab>(tab =>
            {
                ChangeTabAsync(tab);
                MessengerInstance.Send<TimelineTab>(tab,"ChangeTab");
                
            });

            EditTimelineTabCommand = new RelayCommand<TimelineTab>(tab =>
            {
                this.nowEditTimelineTab = tab;
                MessengerInstance.Send<TimelineTab>(tab,"EditTimelineTab");
            });

            EditTimelineCommand = new RelayCommand<TimelineBase>(timeline =>
            {
                this.nowEditTimeline = timeline;
                MessengerInstance.Send<TimelineBase>(timeline, "EditTimeline");
            });

            FavoriteCommand = new RelayCommand<Tweet>(async tweet =>
            {
                await ActionSelectedAccountWithUiAsync(async client =>
                {

                    if (tweet.favorited == false)
                    {
                        tweet.favorited = true;
                        await client.CreateFavoriteAsync(tweet);

                    }
                    else
                    {
                        tweet.favorited = false;
                        await client.DestroyFavoriteAsync(tweet);

                    }

                });

            });

            RetweetCommand = new RelayCommand<Tweet>(async tweet =>
            {
                await ActionSelectedAccountWithUiAsync(async client =>
                {
                    if (tweet.retweeted == false)
                    {
                        tweet.retweeted = true;
                        await client.CreateRetweetAsync(tweet);

                    }
                    else
                    {
                        tweet.retweeted = false;
                        //await client.DestroyRetweetAsync(tweet);
                    }
                });
            });


            QuoteCommand = new RelayCommand<Tweet>(tweet => this.TwitterUIComponent.SetPostText(" RT @" + tweet.user.screen_name + " " + tweet.text));

            TweetDetailCommand = new RelayCommand<TweetDetailParameter>(async tweet =>
            {
                this.MessengerInstance.Send<TweetDetail>(TweetDetail, "ShowTweetDetail");
                this.TweetDetail.OwnerAccount = GetAccount(tweet.OwnerScreenName);
                this.TweetDetail.Set(new TimelineRow(tweet.tweet,"",Setting,CallRowAction),
                    (await GetAccount(tweet.OwnerScreenName).TwitterClient.GetConversationAsync(tweet.tweet)).Select(q=>new TimelineRow(q,tweet.OwnerScreenName,Setting,CallRowAction)).ToList());
                
                
            });

            ReplyCommand = new RelayCommand<Tweet>(tweet =>
            {
                this.TwitterUIComponent.AddPostText("@" + tweet.user.screen_name + " ", tweet);
                Messenger.Default.Send<int>(TwitterUIComponent.PostText.Length,"SetPostTextCursor");
            });

            DescriptionDommand = new RelayCommand<Tweet>(tweet =>
            {
                
                this.TwitterUIComponent.SetPostText("(@"+tweet.user.screen_name+")");
                Messenger.Default.Send<int>(0, "SetPostTextCursor");
            });

            UserDetailCommand = new RelayCommand<UserDetailParameter>(async screen_name =>
            {
                this.MessengerInstance.Send<UserDetail>(UserDetail, "ShowUserDetail");
                this.UserDetail.OwnerAccount = GetAccount(screen_name.OwnerScreenName);
                this.UserDetail.Set(await GetAccount(screen_name.OwnerScreenName).TwitterClient.GetAccountInformationAsync(screen_name.ScreenName), (await GetAccount(screen_name.OwnerScreenName).TwitterClient.GetUserTimeLineAsync(screen_name.ScreenName,100)).Select(q => new TimelineRow(q,screen_name.OwnerScreenName,Setting, CallRowAction)).ToList());
            });

            SearchCommand = new RelayCommand<SearchDetailParameter>(async searchWord =>
            {
                this.MessengerInstance.Send<SearchDetail>(SearchDetail, "ShowSearchDetail");
                this.SearchDetail.OwnerAccount = GetAccount(searchWord.OwnerScreenName);
                this.SearchDetail.Set(searchWord.SearchWord, (await GetAccountFirst().TwitterClient.GetSearchAsync(searchWord.SearchWord,100)).statuses.Select(q => new TimelineRow(q,searchWord.OwnerScreenName, Setting,CallRowAction)).ToList());
                
            });

            BrowseCommand = new RelayCommand<string>(url => this.MessengerInstance.Send<string>(url,"BrowsUrl"));

            BeginAuthCommand = new RelayCommand(async () =>
            {
                string requestUrl = await Authorizer.BeginAuthorizedAsync(this.ConsumerData);
                this.MessengerInstance.Send<string>(requestUrl,"GetAuthorizedUrl");
            });
            PinAuthCommand = new RelayCommand<string>(async pin =>
            {
                TokenResponse<AccessToken> res=null;
                try
                {
                    res = await Authorizer.PinAuthorizedAsync(pin);
                
                var name=res.ExtraData["screen_name"].ElementAt(0);
                var account=new TwitterAccount(ConsumerData,new AccessTokenData(res.Token.Key,res.Token.Secret),name);
                await account.InitializeAsync();
                account.IsActive = true;
                AddAccountCommand.Execute(account);
                var tab = new TimelineTab(name+" - MainTab",CallTabAction,CallTimelineAction,CallRowAction);
                AddTimelineTab(tab);

                AddTimelineCommand.Execute(new HomeTimeline(account,"Home",tab.TabTitle,Setting,CallTimelineAction,CallRowAction));
                
                AddTimelineCommand.Execute(new MentionTimeline(account,"Mention",tab.TabTitle,Setting,CallTimelineAction,CallRowAction)
                {
                    IsNewNotification = true
                });
                
                
                var tab2 = new TimelineTab(name + " - SubTab", CallTabAction, CallTimelineAction, CallRowAction);
                AddTimelineTab(tab2);

                AddTimelineCommand.Execute(new NotificationTimeline(account, "Notification", tab2.TabTitle,Setting, CallTimelineAction, CallRowAction)
                {
                    IsNewNotification = true
                });
                AddTimelineCommand.Execute(new UserTimeline(account, "@" + name, tab2.TabTitle, name,Setting, CallTimelineAction, CallRowAction));

                ChangeTabAsync(tab);
                
                    this.MessengerInstance.Send<bool>(true,"AuthorizedCompleted");
                }
                catch (Exception e)
                {
                    this.MessengerInstance.Send<bool>(false, "AuthorizedCompleted");
                }

                
            });

            SetPostImageCommand = new RelayCommand<PostMedia>(media =>
            {
                TwitterUIComponent.SetPostMedia(media);
            });

            DeletePostImageCommand = new RelayCommand(() =>
            {
                TwitterUIComponent.ResetPostMedia();
                MessengerInstance.Send<string>("","DeletePostImage");
            });


            

            SelectSuggestCommand = new RelayCommand<string>(item =>
            {
                if (item.StartsWith("@")&&TwitterUIComponent.PostText.Contains("@"))
                {
                    var ss = TwitterUIComponent.PostText;
                    int index = ss.LastIndexOf("@", System.StringComparison.Ordinal);
                    string s = ss.Substring(0, index);
                    TwitterUIComponent.PostText = s + item + " ";
                }
                else if (item.StartsWith("#") && TwitterUIComponent.PostText.Contains("#"))
                {
                    var ss = TwitterUIComponent.PostText;
                    int index = ss.LastIndexOf("#", System.StringComparison.Ordinal);
                    string s = ss.Substring(0, index);
                    TwitterUIComponent.PostText = s + item + " ";
                }
            });

            ToggleAccountActivityCommand = new RelayCommand<TwitterAccount>(account => account.ToggleActivity());

            DirectMessageDetailCommand = new RelayCommand<DirectMessageDetailParameter>(async dm =>
            {
                
                MessengerInstance.Send<DirectMessage>(dm.Message,"ShowDirectMessageDetail");
                this.directMesssageDetail.OwnerAccount=GetAccount(dm.OwnerScreenName);
                this.directMesssageDetail.DMessage = dm.Message;
                this.directMesssageDetail.Conversations.Clear();
                var dms=(await GetAccount(dm.OwnerScreenName).TwitterClient.GetDirectMessages());
                var conv=dms.Where(q => (q.sender_screen_name == dm.Message.sender_screen_name
                    &&q.recipient_screen_name==dm.Message.recipient_screen_name)
                    ||(q.sender_screen_name==dm.Message.recipient_screen_name
                    &&q.recipient_screen_name==dm.Message.sender_screen_name)
                    ).Select(q => q).ToList();
                foreach (var c in conv)
                {
                    this.directMesssageDetail.Conversations.Add(new DirectMessageRow(c,dm.OwnerScreenName,Setting,CallRowAction));
                }
            });

            SendDirectMessageCommand = new RelayCommand<SendDirectMessage>(async message =>
            {
                await GetAccount(message.SenderScreenName).TwitterClient.PostDirectMessageNew(message.RecipientScreenName,message.Message);

            });

            ChangeTimelineWidthCommand=new RelayCommand<double>(size =>
            {
                this.TimelineWidth = size;
                foreach (var timeline in NowTimelineList)
                {
                    timeline.TimelineWidth = size;
                }
            });

            ExitCommand=new RelayCommand(async() =>
            {
                await SaveSettingDataAsync();
                await SaveTwitterDataAsync();
                
                Application.Current.Exit();
            });

            AddSuggestPostText=new RelayCommand<string>(str =>
            {
                TwitterUIComponent.SetPostText(TwitterUIComponent.PostText.Substring(0,TwitterUIComponent.PostText.Count() - 1));
                TwitterUIComponent.AddPostText(str + " ");
            });

            ChangeUIBrushImageCommand=new RelayCommand<string>(str =>
            {
                 MessengerInstance.Send<string>(str, "ChangeUIBrushImage");
                
                
            });

            ResetThemeSettingCommand=new RelayCommand(() =>
            {
                Setting.SettingInitialize();
            });
            
            PurchaseApplicationThemeCommand=new RelayCommand(async() =>
            {
                if (!LicenseInfo.ProductLicenses["ApplicationTheme"].IsActive)
                {
                   var result= await CurrentApp.RequestProductPurchaseAsync("ApplicationTheme");
                   
                    if (result.Status == ProductPurchaseStatus.Succeeded)
                    {
                        MessageDialog dialog=new MessageDialog("Thank you for purchase Neuronia CusomAppTheme! Enjoy Neuronia and Twitter Life!","Thank You!");
                        await dialog.ShowAsync();
                        IsPurchase = LicenseInfo.ProductLicenses["ApplicationTheme"].IsActive;
                    }
                }
                
            });

            AddMuteAccountCommand=new RelayCommand<string>(async(screenName) =>
            {
                Setting.MuteAccountList.Add(screenName);
                await NotificationAsync(new NotificationMessage{
                    TweetMessage = Tweet.ZeroTweet,
                    Message="Mute Complate @" + screenName
                });
            });

            CopyClipBoardCommand=new RelayCommand<string>(async str =>
            {
                var package = new DataPackage();
                package.SetText(str);
                Clipboard.SetContent(package);
                await NotificationAsync(new NotificationMessage{
                    TweetMessage = Tweet.ZeroTweet,
                    Message="Copy Completed! "+str
                });
            });

            NextTabCommand=new RelayCommand(() =>
            {
                var num = TimelineListTab.TakeWhile(q => q.IsNowTab).Select(q => q).Count() - 1;
                if (num != TimelineListTab.Count-1)
                {
                    ChangeTabAsync(TimelineListTab.ElementAt(++num));
                }
                
            });

            PrevTabCommand=new RelayCommand(() =>
            {
                var num = TimelineListTab.TakeWhile(q => q.IsNowTab).Select(q => q).Count()+1;
                if (num != 0)
                {
                    ChangeTabAsync(TimelineListTab.ElementAt(--num));
                }
                
            });
        }

        private void AddTimelineTab(TimelineTab tab)
        {
            var i = TimelineListTab.Count(q => q.TabTitle == tab.TabTitle);
            if (i == 0)
            {
                TimelineListTab.Add(tab);
                ChangeTabAsync(tab);
            }
        }

        private async Task ActionSelectedAccountWithUiAsync(Func<TwitterClient, Task> callBack)
        {
            await SharedDispatcher.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                foreach (TwitterAccount account in AccountList)
                {
                    if (account.IsActive)
                    {
                        await callBack(account.TwitterClient);
                    }
                }
            });
        }

        private async Task ActionSelectedAccountAsync(Func<TwitterClient, Task> callBack)
        {
            
                foreach (TwitterAccount account in AccountList)
                {
                    if (account.IsActive)
                    {
                        await callBack(account.TwitterClient);
                    }
                }
            
        }

        private void ChangeTabAsync(TimelineTab tab)
        {
            
                foreach (var t in TimelineListTab)
                {
                    t.IsNowTab = false;
                }
                tab.IsNowTab = true;
                
                    NowTimelineList.Clear();
                    foreach (var t in tab.TimelineList)
                    {
                        NowTimelineList.Add(t);
                    }
             
            
        }

        private void ResetTimeline()
        {
            NowTimelineList.Clear();
            foreach(var timeline in TimelineListTab.Where(q => q.IsNowTab == true).Select(q => q).Single().TimelineList)
            {
                NowTimelineList.Add(timeline);
            }
            
        }

        public TimelineTab GetNowTab()
        {
            if (TimelineListTab.Count == 0)
            {
                return null;
            }
            else
            {
                return TimelineListTab.Where(q => q.IsNowTab == true).Select(q => q).Single();
            }
            
        }

        public TwitterAccount GetAccountFirst()
        {
            return AccountList.Select(q => q).First();
        }

        public TwitterAccount GetAccount(string screenName)
        {
            return AccountList.Where(q => q.UserInfomation.screen_name == screenName).Select(q => q).Single();
        }

        public async Task SaveTwitterDataAsync()
        {
            TwitterData data = new TwitterData();
            data.AccountList = new List<TwitterAccount>(this.AccountList);
            data.TimelineTabList = new List<TimelineTab>(this.TimelineListTab);
            await SaveData.SaveTwitterDataAsync(data);
          
        }

        public async Task LoadTwitterDataAsync(Action<bool> loadCompleted)
        {
            if (SaveData.IsTwitterDataInitializeCompleted())
            {
                var data = await SaveData.LoadTwitterDataAsync();

                foreach (TwitterAccount account in data.AccountList)
                {
                    await account.InitializeAsync();
                    AddAccountCommand.Execute(account);
                }

                var nowTabCount = data.TimelineTabList.Where(q => q.IsNowTab).Select(q => q).Count();
                if (nowTabCount != 0)
                {
                    var nowTab = data.TimelineTabList.Where(q => q.IsNowTab).Select(q => q).Single();
                    foreach (TimelineTab tab in data.TimelineTabList)
                    {
                        tab.Initialize(CallTabAction, CallTimelineAction, CallRowAction);
                        AddTimelineTabCommand.Execute(tab);
                        foreach (TimelineBase timeline in tab.TimelineList)
                        {
                            timeline.Initialize(
                                AccountList.Single(
                                    q => q.UserInfomation.id_str == timeline.Account.UserInfomation.id_str), Setting,
                                CallTimelineAction, CallRowAction);
                            timeline.AddTimeLine();

                        }
                        ResetTimeline();
                        if (tab.IsNowTab)
                        {
                            ChangeTabCommand.Execute(tab);
                        }
                    }
                    ChangeTabCommand.Execute(nowTab);
                }

                IsFirstLaunch = false;
                loadCompleted(true);
            }
            else
            {

                IsFirstLaunch = true;

                loadCompleted(false);
            }
        }

        public async Task SaveSettingDataAsync()
        {
            
            await SaveData.SaveSettingAsync(Setting);
            
        }

        public async Task LoadSettingDataAsync(Action<bool> loadCompleted)
        {
            if (SaveData.IsSettingDataInitializeCompleted())
            {
                Setting = await SaveData.LoadSettingAsync();
                IsFirstLaunch = false;
                loadCompleted(true);
            }
            else
            {
                IsFirstLaunch = true;
                loadCompleted(false);
            }
        }

    }
}