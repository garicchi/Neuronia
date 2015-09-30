using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Post
{
    public class PostStatusWithReply:PostStatusBase
    {
        private string inReplyToStatusId;

        public string InReplyToStatusId
        {
            get { return inReplyToStatusId; }
            set { inReplyToStatusId = value; ModelPropertyChanged("InReplyToStatusId"); }
        }
        public PostStatusWithReply(string status,string inReplyStatusId)
            :base(status)
        {
            this.inReplyToStatusId = inReplyStatusId;
        }
    }
}
