using Neuronia.Core.Twitter;
using Neuronia.Hub.Tab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Data
{
    [DataContract]
    public class TwitterData
    {
        [DataMember]
        public List<TwitterAccount> AccountList { get; set; }
        [DataMember]
        public List<TimelineTab> TimelineTabList { get; set; }

        public TwitterData()
        {
            AccountList = new List<TwitterAccount>();
            TimelineTabList = new List<TimelineTab>();
        }
    }
}
