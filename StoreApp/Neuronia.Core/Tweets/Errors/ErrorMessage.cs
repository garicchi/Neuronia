using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Errors
{
    [DataContract]
    public class ErrorMessage
    {
        [DataMember]
        public List<Error> errors { get; set; }
    }
    
}
