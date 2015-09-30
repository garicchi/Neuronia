using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Disconnects
{
    [DataContract]
    public class Disconnect
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public string stream_name { get; set; }
        [DataMember]
        public string reason { get; set; }
    }
}
