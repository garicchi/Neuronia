using Neuronia.Core.Tweets;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Neuronia.Hub.Timeline
{
    [DataContract]
    public class MentionTimeline:TimelineBase
    {

        public MentionTimeline(TwitterAccount account, string listTitle, string tabName,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.Mention,setting,timelineActionCallback,rowActionCallback)
        {

            Initialize(account,setting, timelineActionCallback, rowActionCallback);
        }

        public override async void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);
            ChangeLoadState(true);

            if (TimeLine.Count > 0)
            {
                string sinceId = (TimeLine.First() as TimelineRow).Tweet.id_str;

                var rows = (await Account.TwitterClient.GetMentionTimelineNewAsync(sinceId)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            else
            {
                var rows = (await Account.TwitterClient.GetMentionTimelineAsync(200)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name,Setting, rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            ChangeLoadState(false);
        }
        
        


        public override async Task GetStreamMention(Tweet tweet)
        {
            await base.GetStreamMentions(tweet);
            await InsertStreamInTimeLineAsync(new TimelineRow(tweet,Account.UserInfomation.screen_name,Setting,rowActionCallback));
            
        }
        
    }
}
