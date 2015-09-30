using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.ImageSizes
{
    [DataContract]
    public class Large
    {

        [DataMember]
        public int w { get; set; }
        [DataMember]
        public int h { get; set; }
        [DataMember]
        public string resize { get; set; }
    }
}
