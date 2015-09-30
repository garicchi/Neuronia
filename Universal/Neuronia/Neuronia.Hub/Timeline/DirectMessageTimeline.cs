using Windows.UI.Xaml.Controls;
using Neuronia.Core.Tweets.DirectMessage;
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
    public class DirectMessageTimeline:TimelineBase
    {
        public DirectMessageTimeline(TwitterAccount account, string listTitle, string tabName,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.DirectMessage,setting, timelineActionCallback, rowActionCallback)
        {
            Initialize(account,setting, timelineActionCallback, rowActionCallback);
        }

        public override async void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);

            ChangeLoadState(true);
            
            if (TimeLine.Count > 0)
            {
                string sinceId = (TimeLine.First() as DirectMessageRow).DirectMessage.id_str;
                var dms = await Account.TwitterClient.GetDirectMessages(sinceId);
                var sents = await Account.TwitterClient.GetDirectMessageSent(sinceId);
                dms.AddRange(sents);
                dms.Sort();
                dms.Reverse();

                var dmRows = dms.Select(d => new DirectMessageRow(d, Account.UserInfomation.screen_name, Setting,rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(dmRows);
            }
            else
            {
                var dms = await Account.TwitterClient.GetDirectMessages();
                var sents = await Account.TwitterClient.GetDirectMessageSent();
                dms.AddRange(sents);
                dms.Sort();
                dms.Reverse();
                var dmRows = dms.Select(d => new DirectMessageRow(d, Account.UserInfomation.screen_name, Setting,rowActionCallback)).Cast<RowBase>().ToList();
                await InsertRestInTimeLineAsync(dmRows);
            }

            ChangeLoadState(false);
        }

        public override async Task GetStreamDirectMessage(DirectMessage e)
        {
            await base.GetStreamDirectMessage(e);
            await InsertStreamInTimeLineAsync(new DirectMessageRow(e, Account.UserInfomation.screen_name,Setting, rowActionCallback));
        }
    }
}
