using Neuronia.Core.Tweets.ImageSizes;
using s.ImageSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Entities
{
    [DataContract]
    public class TweetMedia : EntitieBase
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string id_str { get; set; }

        [DataMember]
        public string media_url { get; set; }
        [DataMember]
        public string media_url_https { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string display_url { get; set; }
        [DataMember]
        public string expanded_url { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public Sizes sizes { get; set; }
    }
}
