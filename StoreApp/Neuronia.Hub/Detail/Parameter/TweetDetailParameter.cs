using Neuronia.Core.Tweets;
using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail.Parameter
{
    public class TweetDetailParameter:DetailParameterBase
    {
        public Tweet tweet { get; set; }
        public TweetDetailParameter(string accountScreenName,Tweet tweet)
            :base(accountScreenName)
        {
            this.tweet = tweet;
        }
    }
}
