using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail.Parameter
{
    public class DirectMessageDetailParameter:DetailParameterBase
    {
        DirectMessage message;

        public DirectMessage Message
        {
            get { return message; }
            set { message = value; }
        }
        public DirectMessageDetailParameter(string accountScreenName,DirectMessage message)
            :base(accountScreenName)
        {
            this.message = message;
        }
    }
}
