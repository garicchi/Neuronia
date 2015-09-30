using Neuronia.Core.Tweets.ImageSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 
    s.ImageSizes
{
    [DataContract]
    public class Sizes
    {
        [DataMember]
        public Medium medium { get; set; }
        [DataMember]
        public Thumb thumb { get; set; }
        [DataMember]
        public Small small { get; set; }
        [DataMember]
        public Large large { get; set; }
    }
}
