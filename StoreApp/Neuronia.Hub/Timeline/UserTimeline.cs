using Neuronia.Core.Twitter;
using Neuronia.Core.TwitterStream;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Neuronia.Hub.Timeline
{
    [DataContract]
    public class UserTimeline:TimelineBase
    {
        private FollowStream followStream;
        [IgnoreDataMember]
        public FollowStream FollowStream
        {
            get { return followStream; }
            set { followStream = value; }
        }

        private string userScreenName;
        [DataMember]
        public string UserScreenName
        {
            get { return userScreenName; }
            set { userScreenName = value; ModelPropertyChanged("UserScreenName"); }
        }
        public UserTimeline(TwitterAccount account, string listTitle, string tabName, string userScreenName,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.User,setting,timelineActionCallback,rowActionCallback)
        {
            this.UserScreenName = userScreenName;
            Initialize(account,setting, timelineActionCallback, rowActionCallback);
        }
        public override async void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);
            followStream = new FollowStream(Account.TwitterClient.ConsumerData,Account.TwitterClient.AccessToken,await Account.TwitterClient.GetUserAsync(UserScreenName));
            followStream.GetStreamMe += async(tweet) =>
            {
                await InsertStreamInTimeLineAsync(new TimelineRow(tweet,Account.UserInfomation.screen_name,Setting,rowActionCallback));
            };
            followStream.GetStreamRetweet += async (tweet) =>
            {
                await InsertStreamInTimeLineAsync(new TimelineRow(tweet, Account.UserInfomation.screen_name,Setting, rowActionCallback));
            };
            if (TimeLine.Count > 0)
            {
                var t=TimeLine.First() as TimelineRow;
                await InsertRestInTimeLineAsync((await Account.TwitterClient.GetUserTimeLineAsync(UserScreenName,t.Tweet.id_str)).Select(q=>new TimelineRow(q,Account.UserInfomation.screen_name,Setting,rowActionCallback)).Cast<RowBase>().ToList());
            }
            else
            {
                await InsertRestInTimeLineAsync((await Account.TwitterClient.GetUserTimeLineAsync(UserScreenName)).Select(q => new TimelineRow(q, Account.UserInfomation.screen_name, Setting,rowActionCallback)).Cast<RowBase>().ToList());
            }
        }

        public override async Task RestUpdate()
        {
            await base.RestUpdate();
            if (TimeLine.Count > 0)
            {
                var t = TimeLine.First() as TimelineRow;
                await InsertRestInTimeLineAsync((await Account.TwitterClient.GetUserTimeLineAsync(UserScreenName, t.Tweet.id_str)).Select(q => new TimelineRow(q, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList());
            }
            else
            {
                await InsertRestInTimeLineAsync((await Account.TwitterClient.GetUserTimeLineAsync(UserScreenName)).Select(q => new TimelineRow(q, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList());
            }
        }
       

        
    }
}
