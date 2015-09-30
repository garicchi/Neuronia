using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Common
{
    public class NotificationMessage:ModelBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; ModelPropertyChanged("Message"); }
        }

        private Tweet tweet;

        public Tweet TweetMessage
        {
            get { return tweet; }
            set { tweet = value; ModelPropertyChanged("TweetMessage"); }
        }
        
        
        public NotificationMessage()
        {
            Message = string.Empty;
        }
    }
}
