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
    public class User : INotifyPropertyChanged
    {
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
            set { _id_str = value; ModelPropertyChanged("id_str"); }
        }
        private string _name;
        [DataMember]
        public string name
        {
            get { return _name; }
            set { _name = value; ModelPropertyChanged("name"); }
        }
        private string _screen_name;
        [DataMember]
        public string screen_name
        {
            get { return _screen_name; }
            set { _screen_name = value; ModelPropertyChanged("screen_name"); }
        }
        private string _location;
        [DataMember]
        public string location
        {
            get { return _location; }
            set { _location = value; ModelPropertyChanged("location"); }
        }
        private string _url;
        [DataMember]
        public string url
        {
            get { return _url; }
            set { _url = value; ModelPropertyChanged("url"); }
        }
        private string _description;
        [DataMember]
        public string description
        {
            get { return _description; }
            set { _description = value; ModelPropertyChanged("description"); }
        }
        private bool _protected;
        [DataMember]
        public bool @protected
        {
            get { return _protected; }
            set { _protected = value; ModelPropertyChanged("@protected"); }
        }
        private int _followers_count;
        [DataMember]
        public int followers_count
        {
            get { return _followers_count; }
            set { _followers_count = value; ModelPropertyChanged("followers_count"); }
        }
        private int _friends_count;
        [DataMember]
        public int friends_count
        {
            get { return _friends_count; }
            set { _friends_count = value; ModelPropertyChanged("friends_count"); }
        }
        private int _listed_count;
        [DataMember]
        public int listed_count
        {
            get { return _listed_count; }
            set { _listed_count = value; ModelPropertyChanged("listed_count"); }
        }
        private string _created_at;
        [DataMember]
        public string created_at
        {
            get { return _created_at; }
            set
            {
                _created_at = value;


                ModelPropertyChanged("created_at");
            }
        }



        private int _favourites_count;
        [DataMember]
        public int favourites_count
        {
            get { return _favourites_count; }
            set { _favourites_count = value; ModelPropertyChanged("favourites_count"); }
        }
        private object _utc_offset;
        [DataMember]
        public object utc_offset
        {
            get { return _utc_offset; }
            set { _utc_offset = value; ModelPropertyChanged("utc_offset"); }
        }
        private string _time_zone;
        [DataMember]
        public string time_zone
        {
            get { return _time_zone; }
            set { _time_zone = value; ModelPropertyChanged("time_zone"); }
        }
        private bool _geo_enabled;
        [DataMember]
        public bool geo_enabled
        {
            get { return _geo_enabled; }
            set { _geo_enabled = value; ModelPropertyChanged("geo_enabled"); }
        }
        private bool _verified;
        [DataMember]
        public bool verified
        {
            get { return _verified; }
            set { _verified = value; ModelPropertyChanged("verified"); }
        }
        private int _statuses_count;
        [DataMember]
        public int statuses_count
        {
            get { return _statuses_count; }
            set { _statuses_count = value; ModelPropertyChanged("statuses_count"); }
        }
        private string _lang;
        [DataMember]
        public string lang
        {
            get { return _lang; }
            set { _lang = value; ModelPropertyChanged("lang"); }
        }
        private bool _contributors_enabled;
        [DataMember]
        public bool contributors_enabled
        {
            get { return _contributors_enabled; }
            set { _contributors_enabled = value; ModelPropertyChanged("contributors_enabled"); }
        }
        private bool _is_translator;
        [DataMember]
        public bool is_translator
        {
            get { return _is_translator; }
            set { _is_translator = value; ModelPropertyChanged("is_translator"); }
        }
        private string _profile_background_color;
        [DataMember]
        public string profile_background_color
        {
            get { return _profile_background_color; }
            set { _profile_background_color = value; ModelPropertyChanged("profile_background_color"); }
        }
        private string _profile_background_image_url;
        [DataMember]
        public string profile_background_image_url
        {
            get { return _profile_background_image_url; }
            set { _profile_background_image_url = value; ModelPropertyChanged("profile_background_image_url"); }
        }
        private string _profile_background_image_url_https;
        [DataMember]
        public string profile_background_image_url_https
        {
            get { return _profile_background_image_url_https; }
            set { _profile_background_image_url_https = value; ModelPropertyChanged("profile_background_image_url_https"); }
        }
        private bool _profile_background_tile;
        [DataMember]
        public bool profile_background_tile
        {
            get { return _profile_background_tile; }
            set { _profile_background_tile = value; ModelPropertyChanged("profile_background_tile"); }
        }
        private string _profile_image_url;
        [DataMember]
        public string profile_image_url
        {
            get { return _profile_image_url; }
            set { _profile_image_url = value; ModelPropertyChanged("profile_image_url"); }
        }
        private string _profile_image_url_https;
        [DataMember]
        public string profile_image_url_https
        {
            get { return _profile_image_url_https; }
            set { _profile_image_url_https = value; ModelPropertyChanged("profile_image_url_https"); }
        }
        private string _profile_link_color;
        [DataMember]
        public string profile_link_color
        {
            get { return _profile_link_color; }
            set { _profile_link_color = value; ModelPropertyChanged("profile_link_color"); }
        }
        private string _profile_sidebar_border_color;
        [DataMember]
        public string profile_sidebar_border_color
        {
            get { return _profile_sidebar_border_color; }
            set { _profile_sidebar_border_color = value; ModelPropertyChanged("profile_sidebar_border_color"); }
        }
        private string _profile_sidebar_fill_color;
        [DataMember]
        public string profile_sidebar_fill_color
        {
            get { return _profile_sidebar_fill_color; }
            set { _profile_sidebar_fill_color = value; ModelPropertyChanged("profile_sidebar_fill_color"); }
        }
        private string _profile_text_color;
        [DataMember]
        public string profile_text_color
        {
            get { return _profile_text_color; }
            set { _profile_text_color = value; ModelPropertyChanged("profile_text_color"); }
        }
        private bool _profile_use_background_image;
        [DataMember]
        public bool profile_use_background_image
        {
            get { return _profile_use_background_image; }
            set { _profile_use_background_image = value; ModelPropertyChanged("profile_use_background_image"); }
        }
        private bool _default_profile;
        [DataMember]
        public bool default_profile
        {
            get { return _default_profile; }
            set { _default_profile = value; ModelPropertyChanged("default_profile"); }
        }
        private bool _default_profile_image;
        [DataMember]
        public bool default_profile_image
        {
            get { return _default_profile_image; }
            set { _default_profile_image = value; ModelPropertyChanged("default_profile_image"); }
        }
        private object _following;
        [DataMember]
        public object following
        {
            get { return _following; }
            set { _following = value; ModelPropertyChanged("following"); }
        }
        private object _follow_request_sent;
        [DataMember]
        public object follow_request_sent
        {
            get { return _follow_request_sent; }
            set { _follow_request_sent = value; ModelPropertyChanged("follow_request_sent"); }
        }
        private object _notifications;
        [DataMember]
        public object notifications
        {
            get { return _notifications; }
            set { _notifications = value; ModelPropertyChanged("notifications"); }
        }

        public Uri Getlink()
        {
            return new Uri("https://twitter.com/"+screen_name);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
