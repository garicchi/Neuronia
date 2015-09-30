using Neuronia.Core.Data;
using Neuronia.Core.Tweets;
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
    public class SearchTimeline:TimelineBase
    {
        
        private string searchWord;
        [DataMember]
        public string SearchWord
        {
            get { return searchWord; }
            set { searchWord = value; ModelPropertyChanged("SearchWord"); }
        }

        TrackStream trackStream;

        public TrackStream TrackStream
        {
            get { return trackStream; }
            set { trackStream = value; }
        }
        public SearchTimeline(TwitterAccount account, string listTitle, string tabName, string searchWord,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
            : base(account, listTitle, tabName, TimelineType.Search,setting,timelineActionCallback,rowActionCallback)
        {
            this.SearchWord = searchWord;
            
            Initialize(account,setting,timelineActionCallback,rowActionCallback);
        }
        public override async void Initialize(TwitterAccount account,SettingData setting, Action<TimelineAction> timelineActionCallback, Action<Row.RowAction> rowActionCallback)
        {
            base.Initialize(account,setting, timelineActionCallback, rowActionCallback);

            
            await InsertRestInTimeLineAsync((await Account.TwitterClient.GetSearchAsync(SearchWord)).statuses.Select(q=>new TimelineRow(q,Account.UserInfomation.screen_name,Setting,rowActionCallback)).Cast<RowBase>().ToList());

            trackStream = new TrackStream(account.TwitterClient.ConsumerData, account.TwitterClient.AccessToken, account.UserInfomation, searchWord);
            
            trackStream.ConnectStreamAsync();
            trackStream.GetStreamTrack += async(tweet) =>
            {
                await InsertStreamInTimeLineAsync(new TimelineRow(tweet,Account.UserInfomation.screen_name,Setting,rowActionCallback));
            };
        }
        


        private string EscapeWord()
        {
            return Uri.EscapeDataString(this.SearchWord);
        }
    }
}
