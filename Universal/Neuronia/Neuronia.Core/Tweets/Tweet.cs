using Neuronia.Core.Common;
using Neuronia.Core.Tweets.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Tweets
{
  
    [DataContract]
    public class Tweet : INotifyPropertyChanged
    {

        private Tweet _retweeted_status;
        [DataMember]
        public Tweet retweeted_status
        {
            get { return _retweeted_status; }
            set { _retweeted_status = value; ModelPropertyChanged("retweeted_status"); }
        }
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

        
        private long _id;
        [DataMember]
        public long id
        {
            get { return _id; }
            set { _id = value; ModelPropertyChanged("id"); }
        }
        private string _id_str;
        [DataMember]
        public string id_str
        {
            get { return _id_str; }
            set { _id_str = value;
                 ModelPropertyChanged("id_str"); }
        }
        private string _text;
        [DataMember]
        public string text
        {
            get { return _text; }
            set { _text = value; ModelPropertyChanged("text"); }
        }
        private string _source;
        [DataMember]
        public string source
        {
            get { return _source; }
            set { _source = value; ModelPropertyChanged("source"); }
        }
        private bool _truncated;
        [DataMember]
        public bool truncated
        {
            get { return _truncated; }
            set { _truncated = value; ModelPropertyChanged("truncated"); }
        }
        private object _in_reply_to_status_id;
        [DataMember]
        public object in_reply_to_status_id
        {
            get { return _in_reply_to_status_id; }
            set { _in_reply_to_status_id = value; ModelPropertyChanged("in_reply_to_status_id"); }
        }
        private object _in_reply_to_status_id_str;
        [DataMember]
        public object in_reply_to_status_id_str
        {
            get { return _in_reply_to_status_id_str; }
            set { _in_reply_to_status_id_str = value; ModelPropertyChanged("in_reply_to_status_id_str"); }
        }
        private object _in_reply_to_user_id;
        [DataMember]
        public object in_reply_to_user_id
        {
            get { return _in_reply_to_user_id; }
            set { _in_reply_to_user_id = value; ModelPropertyChanged("in_reply_to_user_id"); }
        }
        private object _in_reply_to_user_id_str;
        [DataMember]
        public object in_reply_to_user_id_str
        {
            get { return _in_reply_to_user_id_str; }
            set { _in_reply_to_user_id_str = value; ModelPropertyChanged("in_reply_to_user_id_str"); }
        }
        private object _in_reply_to_screen_name;
        [DataMember]
        public object in_reply_to_screen_name
        {
            get { return _in_reply_to_screen_name; }
            set { _in_reply_to_screen_name = value; ModelPropertyChanged("in_reply_to_screen_name"); }
        }
        private User _user;
        [DataMember]
        public User user
        {
            get { return _user; }
            set { _user = value; ModelPropertyChanged("user"); }
        }
        private object _geo;
        [DataMember]
        public object geo
        {
            get { return _geo; }
            set { _geo = value; ModelPropertyChanged("geo"); }
        }
        private object _coordinates;
        [DataMember]
        public object coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; ModelPropertyChanged("coordinates"); }
        }
        private object _place;
        [DataMember]
        public object place
        {
            get { return _place; }
            set { _place = value; ModelPropertyChanged("place"); }
        }
        private object _contributors;
        [DataMember]
        public object contributors
        {
            get { return _contributors; }
            set { _contributors = value; ModelPropertyChanged("contributors"); }
        }
        private int _retweet_count;
        [DataMember]
        public int retweet_count
        {
            get { return _retweet_count; }
            set { _retweet_count = value; ModelPropertyChanged("retweet_count"); }
        }
        private int _favorite_count;
        [DataMember]
        public int favorite_count
        {
            get { return _favorite_count; }
            set { _favorite_count = value; ModelPropertyChanged("favorite_count"); }
        }
        private TweetEntities _entities;
        [DataMember]
        public TweetEntities entities
        {
            get { return _entities; }
            set { _entities = value; ModelPropertyChanged("entities"); }
        }
        private bool _favorited;
        [DataMember]
        public bool favorited
        {
            get { return _favorited; }
            set { _favorited = value; ModelPropertyChanged("favorited"); }
        }
        private bool _retweeted;
        [DataMember]
        public bool retweeted
        {
            get { return _retweeted; }
            set { _retweeted = value; ModelPropertyChanged("retweeted"); }
        }
        private string _filter_level;
        [DataMember]
        public string filter_level
        {
            get { return _filter_level; }
            set { _filter_level = value; ModelPropertyChanged("filter_level"); }
        }
        private string _lang;
        [DataMember]
        public string lang
        {
            get { return _lang; }
            set { _lang = value; ModelPropertyChanged("lang"); }
        }

        

        public Uri Getlink()
        {
             return new Uri("https://twitter.com/"+user.screen_name+"/status/" + id_str);
        }
        
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }


        public static Tweet ZeroTweet
        {
            get
            {
                return new Tweet
                {
                    text = string.Empty
                };
            }
            
        }
    }


}
