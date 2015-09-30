using Neuronia.Core.Tweets;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Data;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Timeline
{
    [DataContract]
    public class ImageTimeline : TimelineBase
    {

        public ImageTimeline(TwitterAccount account, string listTitle, string tabName, SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.Home, setting, timelineActionCallback, rowActionCallback)
        {
            Initialize(account, setting, timelineActionCallback, rowActionCallback);
        }

        public override async void Initialize(TwitterAccount account, SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            base.Initialize(account, setting, timelineActionCallback, rowActionCallback);

            ChangeLoadState(true);

            if (TimeLine.Count > 0)
            {
                string sinceId = (TimeLine.First() as TimelineRow).Tweet.id_str;

                var rows = (await Account.TwitterClient.GetHomeTimelineNewAsync(sinceId)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name, Setting, rowActionCallback)).Cast<RowBase>().ToList();
                rows = rows.Where(q =>q.Tweet.entities.media!=null&& q.Tweet.entities.media.Count > 0).Select(q => q).ToList();
                await InsertRestInTimeLineAsync(rows);
            }
            else
            {
                var rows = (await Account.TwitterClient.GetHomeTimelineAsync(200)).Select(r => new TimelineRow(r, Account.UserInfomation.screen_name, Setting, rowActionCallback)).Cast<RowBase>().ToList();
                rows = rows.Where(q => q.Tweet.entities.media != null && q.Tweet.entities.media.Count > 0).Select(q => q).ToList();
                await InsertRestInTimeLineAsync(rows);
            }

            ChangeLoadState(false);
        }


        public override async Task GetStreamTweet(Tweet tweet)
        {
            await base.GetStreamTweet(tweet);
            if (tweet.entities.media!=null&&tweet.entities.media.Count > 0)
            {
                await InsertStreamInTimeLineAsync(new TimelineRow(tweet, Account.UserInfomation.screen_name, Setting,
                        rowActionCallback));
            }

        }




    }
}
