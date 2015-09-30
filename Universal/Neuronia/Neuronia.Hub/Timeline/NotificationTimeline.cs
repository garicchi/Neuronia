using Neuronia.Core.Data;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Tweets.Stream;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;
using Neuronia.Hub.Row;
using Windows.UI.Core;

namespace Neuronia.Hub.Timeline
{
    [DataContract]
    public class NotificationTimeline:TimelineBase
    {
        public NotificationTimeline(TwitterAccount account, string listTitle, string tabName,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.Notification,setting,timelineActionCallback, rowActionCallback)
        {
            Initialize(account, setting,timelineActionCallback, rowActionCallback);   
        }
        public override void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<Row.RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);
        }
        public override async Task GetStreamSystemMessage(string message)
        {
            await base.GetStreamSystemMessage(message);
            await InsertStreamInTimeLineAsync(new NotificationRow(new Tweet
            {
                user = new User
                {
                    name = "システム",
                    screen_name = "system"
                },
                text =message
            }, "システムメッセージ", Setting,NotificationType.System, this.Account.UserInfomation.screen_name, rowActionCallback));
            
        }
       

        public override async Task GetStreamIsFavorited(TwitterEvent e)
        {
            await base.GetStreamIsFavorited(e);
            Tweet tweet = e.target_object;
            tweet.user = e.source;
            await InsertStreamInTimeLineAsync(new NotificationRow(tweet, "ふぁぼられました", Setting,NotificationType.Favorite, this.Account.UserInfomation.screen_name, rowActionCallback));
            
        }

        public override async Task GetStreamFavorited(TwitterEvent e)
        {
            await base.GetStreamFavorited(e);
     //       await InsertStreamInTimeLineAsync(new NotificationRow(e.target_object, "ふぁぼりました @" + e.source.screen_name,Setting, NotificationType.Favorite, this.Account.UserInfomation.screen_name, rowActionCallback));
           
        }

        public override async Task GetStreamRetweet(Tweet tweet)
        {
            await base.GetStreamRetweet(tweet);
           // await InsertStreamInTimeLineAsync(new NotificationRow(tweet.retweeted_status, "リツイートしました @" + tweet.user.screen_name, Setting, NotificationType.Retweet, this.Account.UserInfomation.screen_name, rowActionCallback));
           
        }

        public override async Task GetStreamRetweeted(Tweet tweet)
        {
            await base.GetStreamRetweeted(tweet);
            Tweet t = tweet.retweeted_status;
            t.user = tweet.user;
            await InsertStreamInTimeLineAsync(new NotificationRow(t, "リツイートされました", Setting, NotificationType.Retweet, this.Account.UserInfomation.screen_name, rowActionCallback));
           
        }

        protected override async Task GetStreamFollow(TwitterEvent e)
        {
            await base.GetStreamFollow(e);
            await InsertStreamInTimeLineAsync(new NotificationRow(e.target_object, "フォローしました @" + e.source.screen_name, Setting, NotificationType.Follow, this.Account.UserInfomation.screen_name, rowActionCallback));
           
        }

        protected override async Task GetStreamIsFollow(TwitterEvent e)
        {
            await base.GetStreamIsFollow(e);
            await InsertStreamInTimeLineAsync(new NotificationRow(e.target_object, "フォローされました @" + e.source.screen_name, Setting, NotificationType.Follow, this.Account.UserInfomation.screen_name, rowActionCallback));
           
        }

        public override async Task GetStreamDirectMessage(DirectMessage e)
        {
            Tweet tweet = new Tweet()
            {
                user = new User
                {
                    screen_name = e.sender.screen_name,
                    name = e.sender.name,
                    profile_image_url =e.sender.profile_image_url
                },
                text = e.text
                
            };
            await InsertStreamInTimeLineAsync(new NotificationRow(tweet, "DMを受信しました @" + e.sender.screen_name, Setting, NotificationType.DirectMessage, this.Account.UserInfomation.screen_name, rowActionCallback));
           
            await base.GetStreamDirectMessage(e);
        }

        protected override async void ChangeUserStreamEvent(Core.TwitterStream.StreamState state)
        {
            await InsertStreamInTimeLineAsync(new NotificationRow(new Tweet
            {
                user=new User{
                    name="システム",screen_name = "system"
                },
                text = "ストリームコネクション　" + state.ToString()
            },"システムメッセージ",Setting,NotificationType.System,this.Account.UserInfomation.screen_name,rowActionCallback));
        }

        protected override async void OnTweetFailed(Core.Post.PostStatusBase status)
        {
            await InsertStreamInTimeLineAsync(new NotificationRow(new Tweet
            {
                user = new User
                {
                    name = "システム",
                    screen_name = "system"
                },
                text = "ツイートの送信に失敗しました"
            }, "システムメッセージ",Setting, NotificationType.System, this.Account.UserInfomation.screen_name, rowActionCallback));
        }

        

        

        protected override async void OnUserStreamHttpError(System.Net.Http.HttpRequestException e)
        {
            await InsertStreamInTimeLineAsync(new NotificationRow(new Tweet
            {
                user = new User
                {
                    name = "システム",
                    screen_name = "system"
                },
                text = "ストリームコネクションに失敗しました"
            }, "システムメッセージ",Setting, NotificationType.System, this.Account.UserInfomation.screen_name, rowActionCallback));
        }
      

    }
}
