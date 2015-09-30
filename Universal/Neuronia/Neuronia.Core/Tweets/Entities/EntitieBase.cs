using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Entities
{
    [DataContract]
    public class EntitieBase
    {
        [DataMember]
        public List<int> indices { get; set; }
    }
}
