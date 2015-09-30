using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.List
{
    [DataContract]
    public class UserList
    {

        [DataMember]
        public List<User> users { get; set; }
        [DataMember]
        public long next_cursor { get; set; }
        [DataMember]
        public string next_cursor_str { get; set; }
        [DataMember]
        public long previous_cursor { get; set; }
        [DataMember]
        public string previous_cursor_str { get; set; }
    }
}
