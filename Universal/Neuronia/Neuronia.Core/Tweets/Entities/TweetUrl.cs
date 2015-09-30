using Neuronia.Core.Tweets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Entities
{
    [DataContract]
    public class TweetUrl : EntitieBase
    {
        [DataMember]
        public string url { get; set; }



        [DataMember]
        public string expanded_url { get; set; }



        [DataMember]
        public string display_url { get; set; }

    }
}
