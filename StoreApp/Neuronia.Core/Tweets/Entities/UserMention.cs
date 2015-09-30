using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Entities
{
    [DataContract]
    public class UserMention : EntitieBase
    {
        [DataMember]
        public string screen_name { get; set; }



        [DataMember]
        public string name { get; set; }


        [DataMember]
        public long id { get; set; }



        [DataMember]
        public string id_str { get; set; }

    }
}
