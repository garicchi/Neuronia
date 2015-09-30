using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Stream
{
    [DataContract]
    public class TwitterEvent
    {

        [DataMember]
        public User target { get; set; }
        [DataMember]
        public User source { get; set; }
        [DataMember]
        public string @event { get; set; }
        [DataMember]
        public Tweet target_object { get; set; }
        [DataMember]
        public string created_at { get; set; }
    }
}
