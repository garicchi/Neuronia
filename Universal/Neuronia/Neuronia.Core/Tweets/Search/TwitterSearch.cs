using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Search
{
    [DataContract]
    public class TwitterSearch
    {

        [DataMember]
        public List<Tweet> statuses { get; set; }
        [DataMember]
        public SearchMetadata search_metadata { get; set; }
    }
}
