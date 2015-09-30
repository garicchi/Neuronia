using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Entities
{
    [DataContract]
    public class TweetEntities
    {
        public TweetEntities()
        {
            hashtags=new List<HashTag>();
            urls=new List<TweetUrl>();
            media=new List<TweetMedia>();
            symbols=new List<object>();
        }

        [DataMember]
        public List<HashTag> hashtags { get; set; }


        [DataMember]
        public List<object> symbols { get; set; }


        [DataMember]
        public List<TweetUrl> urls { get; set; }


        [DataMember]
        public List<UserMention> user_mentions { get; set; }


        [DataMember]
        public List<TweetMedia> media { get; set; }


    }
}
