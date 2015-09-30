using Neuronia.Core.Common;
using Neuronia.Core.Tweets.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets.DirectMessage
{
    [DataContract]
    public class DirectMessage:ModelBase,IComparable
    {

        private string _created_at;
        [DataMember]
        public string created_at
        {
            get { return _created_at; }
            set
            {
                _created_at = value;
                created_at_time = StringToTimeManager.Convert(value);
                ModelPropertyChanged("created_at");
            }
        }

        DateTime _created_at_time;
        [DataMember]
        public DateTime created_at_time
        {
            get { return _created_at_time; }
            set { this._created_at_time = value; }
        }

        
        [DataMember]
        private TweetEntities _entities;

        public TweetEntities entities
        {
            get { return _entities; }
            set { _entities = value; ModelPropertyChanged("created_at"); }
        }
        
        private long _id;
        [DataMember]
        public long id
        {
            get { return _id; }
            set { _id = value; ModelPropertyChanged("created_at"); }
        }
        
        private string _id_str;
        [DataMember]
        public string id_str
        {
            get { return _id_str; }
            set { _id_str = value; ModelPropertyChanged("created_at"); }
        }

        private Recipient _recipient;
        [DataMember]
        public Recipient recipient
        {
            get { return _recipient; }
            set { _recipient = value; ModelPropertyChanged("created_at"); }
        }

        private long _recipient_id;
        [DataMember]
        public long recipient_id
        {
            get { return _recipient_id; }
            set { _recipient_id = value; ModelPropertyChanged("created_at"); }
        }

        private string _recipient_screen_name;
        [DataMember]
        public string recipient_screen_name
        {
            get { return _recipient_screen_name; }
            set { _recipient_screen_name = value; ModelPropertyChanged("created_at"); }
        }

        private Sender _sender;
        [DataMember]
        public Sender sender
        {
            get { return _sender; }
            set { _sender = value; ModelPropertyChanged("created_at"); }
        }

        private long _sender_id;
        [DataMember]
        public long sender_id
        {
            get { return _sender_id; }
            set { _sender_id = value; ModelPropertyChanged("created_at"); }
        }

        private string _sender_screen_name;
        [DataMember]
        public string sender_screen_name
        {
            get { return _sender_screen_name; }
            set { _sender_screen_name = value; ModelPropertyChanged("created_at"); }
        }

        private string _text;
        [DataMember]
        public string text
        {
            get { return _text; }
            set { _text = value; ModelPropertyChanged("created_at"); }
        }


        public int CompareTo(object obj)
        {
           
            return this.created_at_time.CompareTo((obj as DirectMessage).created_at_time);
            
        }
    }
}
