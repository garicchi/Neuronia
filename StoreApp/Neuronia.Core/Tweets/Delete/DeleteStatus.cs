using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.Delete
{
    public class DeleteStatus
    {
        public long id { get; set; }
        public long user_id { get; set; }
        public string id_str { get; set; }
        public string user_id_str { get; set; }
    }
}
