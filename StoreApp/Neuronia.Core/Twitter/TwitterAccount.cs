using AsyncOAuth;
using Neuronia.Core.Common;
using Neuronia.Core.Data;
using Neuronia.Core.Post;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.Stream;
using Neuronia.Core.TwitterStream;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Twitter
{
    [DataContract]
    public class TwitterAccount : ModelBase
    {
        public event Action<HttpRequestException> OnHttpGetError;

        public event Action<HttpRequestException> OnHttpPostError;

        public event Action<StreamState> ChangeUserStreamEvent;

        public event Action<StreamState> ChangeFollowStreamEvent;

        public event Action<HttpRequestException> OnUserStreamHttpError;

        public event Action<HttpRequestException> OnFollowStreamHttpError;

        public event Action<PostStatusBase> OnTweetBegin;

        public event Action<PostStatusBase> OnTweetFailed;

        public event Action<PostStatusBase> OnTweetCompleted;


        private User userInfomation;
        [DataMember]
        public User UserInfomation
        {
            get { return userInfomation; }
            set { userInfomation = value; }
        }

        private TwitterClient twitterClient;
        [DataMember]
        public TwitterClient TwitterClient
        {
            get { return twitterClient; }
            set { twitterClient = value; ModelPropertyChanged("TwitterClient"); }
        }

        private bool isActive;
        [DataMember]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; ModelPropertyChanged("IsActive"); }
        }

        private ObservableCollection<string> friendsScreenNameList;
        [DataMember]
        public ObservableCollection<string> FriendsScreenNameList
        {
            get { return friendsScreenNameList; }
            set { friendsScreenNameList = value; }
        }
        [DataMember]
        private int receivingTimelineCounter;


        private UserStream userStreamClient;
        [IgnoreDataMember]
        public UserStream UserStreamClient
        {
            get { return userStreamClient; }
            set { userStreamClient = value; }
        }

        private FollowStream followStreamClient;
        [IgnoreDataMember]
        public FollowStream FollowStreamClient
        {
            get { return followStreamClient; }
            set { followStreamClient = value; }
        }

        private string screenName;
        [DataMember]
        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        

        public void ToggleActivity()
        {
            IsActive = !IsActive;
        }

        public void AddTimeLine()
        {

            if (receivingTimelineCounter == 0)
            {
                receivingTimelineCounter++;


                UserStreamClient.ConnectStreamAsync();
                FollowStreamClient.ConnectStreamAsync();
            }
            else
            {
                receivingTimelineCounter++;
            }
        }

        public void DeleteTimeline()
        {
            receivingTimelineCounter--;
            if (receivingTimelineCounter == 0)
            {
                UserStreamClient.DisConnect();
                FollowStreamClient.DisConnect();
            }
        }

        
        

        public TwitterAccount(ConsumerData consumerData, AccessTokenData accessToken,string screenName)
        {
            IsActive = false;
            
            friendsScreenNameList = new ObservableCollection<string>();
            TwitterClient = new TwitterClient(consumerData,accessToken);

            ScreenName = screenName;


        }


        public async Task InitializeAsync()
        {
            TwitterClient.Initialize();
            this.UserInfomation =await TwitterClient.GetAccountInformationAsync(ScreenName);
            userStreamClient = new UserStream(TwitterClient.ConsumerData, TwitterClient.AccessToken, UserInfomation);
            followStreamClient = new FollowStream(TwitterClient.ConsumerData, TwitterClient.AccessToken, UserInfomation);

            userStreamClient.Initialize();

            followStreamClient.Initialize();
            receivingTimelineCounter = 0;

            OnHttpGetError += (e) => { };
            OnHttpPostError += (e) => { };
            OnTweetBegin += (e) => { };
            OnTweetFailed += (e) => { };
            OnTweetCompleted += (e) => { };
            ChangeUserStreamEvent += (e) => { };
            ChangeFollowStreamEvent += (e) => { };
            OnUserStreamHttpError += (e) => { };
            OnFollowStreamHttpError += (e) => { };
            

            twitterClient.OnHttpGetError += (e) =>
            {
                OnHttpGetError(e);
            };
            twitterClient.OnHttpPostError += (e) =>
            {
                OnHttpPostError(e);
            };
            twitterClient.OnTweetBegin += (e) =>
            {
                OnTweetBegin(e);
            };
            twitterClient.OnTweetFailed += (e) =>
            {
                OnTweetFailed(e);
            };
            twitterClient.OnTweetCompleted += (e) =>
            {
                OnTweetCompleted(e);
            };
            userStreamClient.ChangeStreamEvent += (e) =>
            {
                ChangeUserStreamEvent(e);
            };
            followStreamClient.ChangeStreamEvent += (e) =>
            {
                ChangeFollowStreamEvent(e);
            };

            userStreamClient.OnStreamError += (e) =>
            {
                OnUserStreamHttpError(e);
            };
            followStreamClient.OnStreamError += (e) =>
            {
                OnFollowStreamHttpError(e);
            };

            
        }

        


    }

}