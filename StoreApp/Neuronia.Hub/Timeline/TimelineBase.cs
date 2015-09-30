using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Common;
using Neuronia.Core.Twitter;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.Stream;
using Neuronia.Hub.Common;
using Windows.UI.Core;
using Neuronia.Hub.Data;
using Neuronia.Utility;
using Neuronia.Hub.Row;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;
using Neuronia.Core.Tweets.Delete;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Post;
using System.Net.Http;
using Neuronia.Core.TwitterStream;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Neuronia.Hub.Timeline
{
    [DataContract]
    [KnownType(typeof(HomeTimeline))]
    [KnownType(typeof(MentionTimeline))]
    [KnownType(typeof(NotificationTimeline))]
    [KnownType(typeof(ListTimeline))]
    [KnownType(typeof(UserTimeline))]
    [KnownType(typeof(SearchTimeline))]
    [KnownType(typeof(DirectMessageTimeline))]
    [KnownType(typeof(ImageTimeline))]
    [KnownType(typeof(LinkTimeline))]
    public class TimelineBase : ModelBase
    {

        private ObservableCollection<RowBase> timeLine;

        [DataMember]
        public ObservableCollection<RowBase> TimeLine
        {
            get { return timeLine; }
            set { timeLine = value; }
        }

        private string listTitle;

        [DataMember]
        public string ListTitle
        {
            get { return listTitle; }
            set { this.listTitle = value; ModelPropertyChanged("ListTitle"); }
        }

        private TwitterAccount account;

        [DataMember]
        public TwitterAccount Account
        {
            get { return account; }
            set { account = value; ModelPropertyChanged("Account"); }
        }



        private TimelineType timelineType;
        [DataMember]
        public TimelineType TimelineType
        {
            get { return timelineType; }
            set { timelineType = value; ModelPropertyChanged("TimelineType"); }
        }

        private bool isTimelineFiltering;
        [DataMember]
        public bool IsTimelineFiltering
        {
            get { return isTimelineFiltering; }
            set { isTimelineFiltering = value; ModelPropertyChanged("IsTimelineFiltering"); }
        }

        private bool isNewNotification;
        [DataMember]
        public bool IsNewNotification
        {
            get { return isNewNotification; }
            set { isNewNotification = value; ModelPropertyChanged("IsNewNotification"); }
        }

        private string extractionAccountScreenNameStr;
        [DataMember]
        public string ExtractionAccountScreenNameStr
        {
            get { return extractionAccountScreenNameStr; }
            set
            {
                extractionAccountScreenNameStr = value;
                ExtractionAccountScreenNameList = SplitFilterStr(value);
                ModelPropertyChanged("ExtractionAccountScreenNameStr");
            }
        }

        private string extractionWordStr;
        [DataMember]
        public string ExtractionWordStr
        {
            get { return extractionWordStr; }
            set
            {
                extractionWordStr = value; ModelPropertyChanged("ExtractionWordStr");
                ExtractionWordList = SplitFilterStr(value);
            }
        }

        private string excludeAccountScreenNameStr;
        [DataMember]
        public string ExcludeAccountScreenNameStr
        {
            get { return excludeAccountScreenNameStr; }
            set
            {
                excludeAccountScreenNameStr = value; ModelPropertyChanged("ExcludeAccountScreenNameStr");
                ExcludeAccountScreenNameList = SplitFilterStr(value);
            }
        }

        private string excludeWordStr;
        [DataMember]
        public string ExcludeWordStr
        {
            get { return excludeWordStr; }
            set
            {
                excludeWordStr = value; ModelPropertyChanged("ExcludeWordStr");
                ExcludeWordList = SplitFilterStr(value);
            }
        }

        public List<string> ExtractionAccountScreenNameList { get; set; }

        public List<string> ExtractionWordList { get; set; }


        public List<string> ExcludeAccountScreenNameList { get; set; }


        public List<string> ExcludeWordList { get; set; }


        public int RowMax { get; set; }
        [DataMember]
        public string TabName { get; set; }

        private double timelineWidth;

        bool isNowLoading;

        private int liveTileCounter;

        public bool IsNowLoading
        {
            get { return isNowLoading; }
            set { this.isNowLoading = value; ModelPropertyChanged("IsNowLoading"); }
        }

        [DataMember]
        public double TimelineWidth
        {
            get { return timelineWidth; }
            set { this.timelineWidth = value; ModelPropertyChanged("TimelineWidth"); }
        }

        [IgnoreDataMember]
        protected Action<RowAction> rowActionCallback;
        [IgnoreDataMember]
        protected Action<TimelineAction> timelineActionCallback;

        [IgnoreDataMember]
        public RelayCommand EditCommand { get; set; }

        [IgnoreDataMember]
        public RelayCommand DeleteCommand { get; set; }

        private SettingData setting;
        [DataMember]
        public SettingData Setting
        {
            get { return setting; }
            set { setting = value; ModelPropertyChanged("Setting"); }
        }


        public TimelineBase(TwitterAccount account, string listTitle, string tabName, TimelineType type, SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {

            this.ListTitle = listTitle;
            timeLine = new ObservableCollection<RowBase>();
            IsTimelineFiltering = false;
            IsNewNotification = false;
            this.TimelineType = type;
            this.TabName = tabName;
            ExtractionAccountScreenNameStr = string.Empty;
            ExcludeAccountScreenNameStr = string.Empty;
            ExtractionWordStr = string.Empty;
            ExcludeWordStr = string.Empty;
            this.Setting = setting;
            IsNowLoading = false;
            liveTileCounter = 0;

        }

        public virtual void Initialize(TwitterAccount account, SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            this.account = account;
            this.account.UserStreamClient.GetStreamHome += this.GetStreamTweet;
            this.account.UserStreamClient.GetStreamFavorited += this.GetStreamFavorited;
            this.account.UserStreamClient.GetStreamIsFavorited += this.GetStreamIsFavorited;
            this.account.UserStreamClient.GetStreamSystemMessage += this.GetStreamSystemMessage;
            this.account.UserStreamClient.GetStreamDelete += this.GetStreamDelete;
            this.account.UserStreamClient.GetStreamDirectMessage += GetStreamDirectMessage;
            this.account.UserStreamClient.GetStreamFollow+=GetStreamFollow;
            this.account.UserStreamClient.GetStreamIsFollowed += GetStreamIsFollow;
            this.account.FollowStreamClient.GetStreamRetweet += GetStreamRetweet;
            this.account.FollowStreamClient.GetStreamRetweeted += GetStreamRetweeted;
            this.account.FollowStreamClient.GetStreamMe += GetStreamMe;
            this.account.FollowStreamClient.GetStreamMention += GetStreamMention;
            this.account.FollowStreamClient.GetStreamDelete += GetStreamDelete;
            this.account.OnTweetFailed += OnTweetFailed;
            this.account.OnUserStreamHttpError += OnUserStreamHttpError;
            this.account.ChangeUserStreamEvent += ChangeUserStreamEvent;
            this.timelineWidth = 320;
            this.RowMax = 2000;
            this.timelineActionCallback = timelineActionCallback;
            this.rowActionCallback = rowActionCallback;
            this.Setting = setting;
            
           


            EditCommand = new RelayCommand(() =>
            {
                this.timelineActionCallback(new TimelineAction(TimelineActionType.Edit, this));
            });
            DeleteCommand = new RelayCommand(() =>
            {
                this.timelineActionCallback(new TimelineAction(TimelineActionType.Delete, this));
            });

            foreach (var t in TimeLine)
            {
                t.InitializeBase(rowActionCallback);
                if (t is TimelineRow)
                {
                    (t as TimelineRow).Initialize(rowActionCallback);
                }
                else if (t is DirectMessageRow)
                {
                    (t as DirectMessageRow).Initialize(rowActionCallback);
                }
                else if (t is NotificationRow)
                {
                    (t as NotificationRow).Initialize(rowActionCallback);
                }
            }


        }

        protected virtual async Task GetStreamFollow(TwitterEvent e)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        protected virtual async Task GetStreamIsFollow(TwitterEvent e)
        {
            await CheckAndDeleteMaxRowAsync();
        }


        protected virtual void OnTweetFailed(PostStatusBase status)
        {

        }

        protected virtual void OnUserStreamHttpError(HttpRequestException e)
        {

        }

        protected virtual void ChangeUserStreamEvent(StreamState state)
        {

        }
        public void AddTimeLine()
        {
            this.Account.AddTimeLine();

        }

        public void DeleteTimeLine()
        {
            this.Account.DeleteTimeline();
        }

        public async Task CheckAndDeleteMaxRowAsync()
        {
           /* if (TimeLine.Count > 40)
            {
                await SharedDispatcher.RunAsync(() =>
                {
                    TimeLine.RemoveAt(TimeLine.Count - 1);
                });

            }
            else
            {

            }
            * */

        }

        private List<string> SplitFilterStr(string str)
        {
            List<string> resultList = new List<string>();
            if (str != string.Empty)
            {
                foreach (var s in str.Split(','))
                {
                    resultList.Add(s);
                }
            }
            return resultList;
        }



        protected void ChangeLoadState(bool state)
        {
            IsNowLoading = state;
        }

        protected async Task<bool> IsAllFilterClearAsync(Tweet tweet)
        {
            bool result = false;


            if (await isExtractAccountFilterClear(tweet) &&
               await isExcludeAccountFilterClear(tweet) &&
               await isExtractWordFilterClear(tweet) &&
               await isExcludeWordFilterClear(tweet) &&
                await isMuteAccountFilerClear(tweet))
            {
                result = true;
            }
            else
            {
                result = false;
            }


            return result;
        }


        protected Task<bool> isCompetition(List<RowBase> tweetList, Tweet tweet)
        {
            return Task.Run(() =>
            {
                bool result = false;
                foreach (var t in tweetList)
                {
                    if (t.Tweet.id_str == tweet.id_str)
                    {
                        result = true;
                    }
                }
                return result;
            });

        }

        private Task<bool> isExtractAccountFilterClear(Tweet tweet)
        {
            return Task.Run(() =>
            {
                bool result = false;

                if (ExtractionAccountScreenNameList.Count == 0) { return true; }
                foreach (var item in ExtractionAccountScreenNameList)
                {
                    if (item == tweet.user.screen_name)
                    {
                        result = true;
                    }
                }

                return result;
            });

        }

        private Task<bool> isExtractWordFilterClear(Tweet tweet)
        {
            return Task.Run(() =>
            {
                bool result = false;

                if (ExtractionWordList.Count == 0) { return true; }
                foreach (var item in ExtractionWordList)
                {
                    if (tweet.text.Contains(item))
                    {
                        result = true;
                    }
                }
                return result;
            });

        }

        private Task<bool> isExcludeWordFilterClear(Tweet tweet)
        {
            return Task.Run(() =>
            {
                bool result = true;

                if (ExcludeWordList.Count == 0) { return true; }
                foreach (var item in ExcludeWordList)
                {
                    if (tweet.text.Contains(item))
                    {
                        result = false;
                    }
                }
                return result;
            });

        }

        private Task<bool> isExcludeAccountFilterClear(Tweet tweet)
        {
            return Task.Run(() =>
            {
                bool result = true;

                if (ExcludeAccountScreenNameList.Count == 0) { return true; }
                foreach (var item in ExcludeAccountScreenNameList)
                {
                    if (item == tweet.user.screen_name)
                    {
                        result = false;
                    }
                }
                return result;
            });


        }

        public Task<bool> isMuteAccountFilerClear(Tweet tweet)
        {
            return Task.Run(() =>
            {
                if (tweet.text!=null&&tweet.user.screen_name != null)
                {
                    return Setting.MuteAccountList.All(muteAccount => tweet.user.screen_name != muteAccount);
                }
                else
                {
                    return true;
                }
            });

        }

        private async Task LiveTileImageUpdateAsync(Tweet tweet)
        {
            if (tweet.entities != null && tweet.entities.media != null && tweet.entities.media.Count > 0)
            {
                await SharedDispatcher.RunAsync(() =>
                {
                    
                    //テンプレートを取得してXMLを編集
                    XmlDocument doc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage05);
                    doc.GetElementsByTagName("text")[0].InnerText = "@" + tweet.user.screen_name;
                    doc.GetElementsByTagName("text")[1].InnerText = tweet.text;
                    doc.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").NodeValue = tweet.entities.media.First().media_url;
                    doc.GetElementsByTagName("image")[1].Attributes.GetNamedItem("src").NodeValue = tweet.user.profile_image_url;

                    TileNotification notification = new TileNotification(doc);

                    
                    notification.ExpirationTime = DateTimeOffset.UtcNow.AddDays(2);
                    //一意のタグを指定
                    notification.Tag ="wide"+ liveTileCounter.ToString();
                    //通知を送信
                    TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
                    updater.EnableNotificationQueueForWide310x150(true);
                    
                    updater.Update(notification);
                    

                    XmlDocument docLarge = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndTextOverlay03);
                    docLarge.GetElementsByTagName("text")[0].InnerText = "@" + tweet.user.screen_name;
                    docLarge.GetElementsByTagName("text")[1].InnerText = tweet.text;
                    docLarge.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").NodeValue = tweet.entities.media.First().media_url;
                    
                    TileNotification notificationLarge = new TileNotification(docLarge);


                    notificationLarge.ExpirationTime = DateTimeOffset.UtcNow.AddDays(2);
                    //一意のタグを指定
                    notificationLarge.Tag ="large"+ liveTileCounter.ToString();

                    //通知を送信
                    TileUpdater updaterLarge = TileUpdateManager.CreateTileUpdaterForApplication();
                    
                    updaterLarge.EnableNotificationQueueForSquare310x310(true);
                    
                    updaterLarge.Update(notificationLarge);
                    if (liveTileCounter > 6)
                    {
                        liveTileCounter = 0;
                    }
                    liveTileCounter++;
                });



            }
        }

        protected async Task InsertRestInTimeLineAsync(List<RowBase> tweetList)
        {
            if (tweetList != null)
            {
                List<RowBase> tempTweetList = new List<RowBase>();
                RowBase[] tweetListCopy = null;
                await Task.Run(async () =>
                {
                    tweetList.Reverse();
                    tweetListCopy = new RowBase[TimeLine.Count];
                    TimeLine.CopyTo(tweetListCopy, 0);


                    foreach (var tweet in tweetList)
                    {
                        if (await IsAllFilterClearAsync(tweet.Tweet))
                        {
                            if (await isCompetition(tweetListCopy.ToList(), tweet.Tweet) == false)
                            {
                                tempTweetList.Add(tweet);
                            }
                        }
                    }
                });
                await SharedDispatcher.RunAsync(() =>
                    {
                        foreach (var tweet in tempTweetList)
                        {

                            TimeLine.Insert(0, tweet);

                        }
                    });
                //TimeLine.OrderBy(q => q.Tweet.created_at_time).Select(q => q);


            }
        }

        public Task<List<string>> GetTimelineAccountListAsync()
        {
            return Task.Run(() =>
            {
                List<string> list = new List<string>();
                foreach (var t in TimeLine)
                {
                    string name = t.Tweet.user.screen_name;
                    if (!list.Contains(name))
                    {
                        list.Add(name);
                    }
                }
                return list;
            });

        }

        public Task<List<string>> GetTimelineHashTagListAsync()
        {
            return Task.Run(() =>
            {
                List<string> list = new List<string>();
                foreach (var t in TimeLine)
                {
                    if (t.Tweet.entities != null && t.Tweet.entities.hashtags.Count > 0)
                    {
                        string name = t.Tweet.entities.hashtags.First().text;
                        if (!list.Contains(name))
                        {
                            list.Add(name);
                        }
                    }
                }
                return list;
            });

        }


        protected async Task InsertStreamInTimeLineAsync(RowBase row)
        {

            if (await IsAllFilterClearAsync(row.Tweet))
            {
                if (IsNewNotification == true)
                {
                    if (row is NotificationRow)
                    {
                        var r=row as NotificationRow;
                        timelineActionCallback(new TimelineAction(TimelineActionType.NewNotification, new NotificationMessage
                        {
                            Message = r.Message,
                            TweetMessage = row.Tweet
                        }));
                    }
                    else
                    {
                        timelineActionCallback(new TimelineAction(TimelineActionType.NewNotification, new NotificationMessage
                        {
                            Message = "Notification",
                            TweetMessage = row.Tweet
                        }));
                    }
                    
                    
                }
                await SharedDispatcher.RunAsync(() =>
                {
                    TimeLine.Insert(0, row);

                });

                await LiveTileImageUpdateAsync(row.Tweet);
            }

        }






        public virtual async Task GetStreamDelete(TweetDelete delete)
        {
            RowBase row = null;
            await Task.Run(() =>
            {
                foreach (var t in TimeLine)
                {
                    if (t is TimelineRow)
                    {
                        TimelineRow tr = (t as TimelineRow);
                        if (tr.Tweet.id_str == delete.status.id_str)
                        {
                            row = tr;
                        }
                    }
                    else if (t is DirectMessageRow)
                    {
                        DirectMessageRow tr = (t as DirectMessageRow);
                        if (tr.DirectMessage.id_str == delete.status.id_str)
                        {
                            row = tr;
                        }
                    }
                }

            });
            await SharedDispatcher.RunAsync(() =>
            {
                if (row != null)
                {
                    TimeLine.Remove(row);
                }
            });
        }

        public virtual async Task RestUpdate()
        {

        }

        public virtual async Task GetStreamMention(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }
        public virtual async Task GetStreamMe(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }
        public virtual async Task GetStreamRetweet(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }
        public virtual async Task GetStreamRetweeted(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamTweet(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamIsFavorited(TwitterEvent e)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamSystemMessage(string message)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamMentions(Tweet tweet)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamFavorited(TwitterEvent e)
        {
            await CheckAndDeleteMaxRowAsync();
        }

        public virtual async Task GetStreamDirectMessage(DirectMessage e)
        {
            await CheckAndDeleteMaxRowAsync();
        }



    }
}
