using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.DirectMessage
{
    [DataContract]
    public class Sender
    {
        [DataMember]
        public bool contributors_enabled { get; set; }
        [DataMember]
        public string created_at { get; set; }
        [DataMember]
        public bool default_profile { get; set; }
        [DataMember]
        public bool default_profile_image { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int favourites_count { get; set; }
        [DataMember]
        public bool follow_request_sent { get; set; }
        [DataMember]
        public int followers_count { get; set; }
        [DataMember]
        public bool following { get; set; }
        [DataMember]
        public int friends_count { get; set; }
        [DataMember]
        public bool geo_enabled { get; set; }
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string id_str { get; set; }
        [DataMember]
        public bool is_translator { get; set; }
        [DataMember]
        public string lang { get; set; }
        [DataMember]
        public int listed_count { get; set; }
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public bool notifications { get; set; }
        [DataMember]
        public string profile_background_color { get; set; }
        [DataMember]
        public string profile_background_image_url { get; set; }
        [DataMember]
        public string profile_background_image_url_https { get; set; }
        [DataMember]
        public bool profile_background_tile { get; set; }
        [DataMember]
        public string profile_image_url { get; set; }
        [DataMember]
        public string profile_image_url_https { get; set; }
        [DataMember]
        public string profile_link_color { get; set; }
        [DataMember]
        public string profile_sidebar_border_color { get; set; }
        [DataMember]
        public string profile_sidebar_fill_color { get; set; }
        [DataMember]
        public string profile_text_color { get; set; }
        [DataMember]
        public bool profile_use_background_image { get; set; }
        [DataMember]
        public bool @protected { get; set; }
        [DataMember]
        public string screen_name { get; set; }
        [DataMember]
        public bool show_all_inline_media { get; set; }
        [DataMember]
        public int statuses_count { get; set; }
        [DataMember]
        public string time_zone { get; set; }
        [DataMember]
        public object url { get; set; }
        [DataMember]
        public long utc_offset { get; set; }
        [DataMember]
        public bool verified { get; set; }
    }
}
