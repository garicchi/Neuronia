using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.List
{
    [DataContract]
    public class TwitterList
    {

        [DataMember]
        public string slug { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string created_at { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public int subscriber_count { get; set; }
        [DataMember]
        public string id_str { get; set; }
        [DataMember]
        public int member_count { get; set; }
        [DataMember]
        public string mode { get; set; }
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string full_name { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public User user { get; set; }
        [DataMember]
        public bool following { get; set; }
    }
}
