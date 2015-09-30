using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Search
{
    [DataContract]
    public class SearchMetadata
    {

        [DataMember]
        public double completed_in { get; set; }
        [DataMember]
        public long max_id { get; set; }
        [DataMember]
        public string max_id_str { get; set; }
        [DataMember]
        public string next_results { get; set; }
        [DataMember]
        public string query { get; set; }
        [DataMember]
        public string refresh_url { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public long since_id { get; set; }
        [DataMember]
        public string since_id_str { get; set; }
    }
}
