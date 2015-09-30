using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.DirectMessage
{
    public class SendDirectMessage
    {
        public string SenderScreenName { get; set; }

        public string RecipientScreenName { get; set; }

        public string Message { get; set; }
    }
}
