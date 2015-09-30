using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Data
{
    [DataContract]
    public class ConsumerData
    {
        [DataMember]
        public string ConsumerKey { get; set; }
        [DataMember]
        public string ConsumerSecret { get; set; }
        public ConsumerData(string key,string secret)
        {
            this.ConsumerKey = key;
            this.ConsumerSecret = secret;
        }
    }
}
