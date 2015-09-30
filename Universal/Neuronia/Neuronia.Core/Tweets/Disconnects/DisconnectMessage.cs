using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Disconnects
{
    [DataContract]
    public class DisconnectMessage
    {
        [DataMember]
        public Disconnect disconnect { get; set; }
    }
}
