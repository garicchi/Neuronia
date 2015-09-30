using Neuronia.Core.Common;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail
{
    public class DirectMessageDetail:DetailBase
    {
        private DirectMessage dMessage;

        public DirectMessage DMessage
        {
            get { return dMessage; }
            set { dMessage = value; ModelPropertyChanged("DMessage"); }
        }

        private ObservableCollection<DirectMessageRow> conversations;

        public ObservableCollection<DirectMessageRow> Conversations
        {
            get { return conversations; }
            set { conversations = value; ModelPropertyChanged("Conversations"); }
        }


        private string sendDirectMessage;

        public string SendDirectMessage
        {
            get { return sendDirectMessage; }
            set { sendDirectMessage = value; ModelPropertyChanged("SendDirectMessage"); }
        }
        public DirectMessageDetail()
        {
            this.dMessage = new DirectMessage();
            Conversations = new ObservableCollection<DirectMessageRow>();
        }
    }
}
