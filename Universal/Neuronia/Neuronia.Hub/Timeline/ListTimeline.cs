using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.List;
using Neuronia.Core.Twitter;
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
    public class ListTimeline:TimelineBase
    {
        [DataMember]
        public TwitterList TwitterList { get; set; }
        public ListTimeline(TwitterAccount account, string listTitle, string tabName,TwitterList list,SettingData setting,Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.List,setting,timelineActionCallback,rowActionCallback)
        {
            this.TwitterList = list;
            Initialize(account,setting, timelineActionCallback, rowActionCallback);
        }

        public override async void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);
            if (TimeLine.Count > 0)
            {
                var t = TimeLine.First() as TimelineRow;
                
                var rows = (await Account.TwitterClient.GetListAsync(TwitterList,t.Tweet.id_str)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            else
            {
                var rows = (await Account.TwitterClient.GetListAsync(TwitterList)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            
        }

        public override async Task RestUpdate()
        {
            await base.RestUpdate();
            if (TimeLine.Count > 0)
            {
                var t = TimeLine.First() as TimelineRow;

                var rows = (await Account.TwitterClient.GetListAsync(TwitterList, t.Tweet.id_str)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            else
            {
                var rows = (await Account.TwitterClient.GetListAsync(TwitterList)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
        }

        public override async Task GetStreamTweet(Tweet tweet)
        {
            await base.GetStreamTweet(tweet);
            
        }
        
    }
}
