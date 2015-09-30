using AsyncOAuth;
using Neuronia.Core.Data;
using Neuronia.Core.Tweets;
using Neuronia.Core.Twitter;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;

namespace Neuronia.Core.TwitterStream
{
    public class TrackStream:TwitterStreamBase
    {
        public event Func<Tweet, Task> GetStreamTrack;
        string trackKeyword;

        public string TrackKeyword
        {
            get { return trackKeyword; }
            set { trackKeyword = value; }
        }
        public TrackStream(ConsumerData consumerData,AccessTokenData token,User user,string trackKeyword)
            : base(consumerData,token,user, TwitterUrl.FilterStreamingUrl + "?track=" + Uri.EscapeDataString(trackKeyword))
        {
            this.trackKeyword = trackKeyword;
            GetStreamTrack += async(e) => { };
        }

        protected override async Task StreamProcess(string s)
        {
            JObject obj = null;
            await Task.Run(() =>
            {
                obj = JObject.Parse(s);
            });

            var isDisconnect = obj["disconnect"];
            if (isDisconnect != null)
            {

            }
            else
            {

                Tweet tweet = await s.JsonDeseliazeAsync<Tweet>();

                await GetStreamTrack(tweet);

            }
        }
    }
}
