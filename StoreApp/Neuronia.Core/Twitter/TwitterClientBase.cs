using AsyncOAuth;
using Neuronia.Core.Common;
using Neuronia.Core.Data;
using Neuronia.Core.Tweets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Twitter
{
    [DataContract]
    public abstract class TwitterClientBase:ModelBase
    {
        ConsumerData consumerData;
        [DataMember]
        public ConsumerData ConsumerData
        {
            get { return consumerData; }
            set { consumerData = value; }
        }

        AccessTokenData accessToken;
        [DataMember]
        public AccessTokenData AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }


        HttpClient httpClient;
        [IgnoreDataMember]
        public HttpClient HttpClient
        {
            get { return httpClient; }
            set { httpClient = value; }
        }
        public TwitterClientBase(ConsumerData consumerData,AccessTokenData token)
        {
            this.consumerData = consumerData;

            this.accessToken = token;
            httpClient = OAuthUtility.CreateOAuthClient(ConsumerData.ConsumerKey, ConsumerData.ConsumerSecret,new AccessToken(AccessToken.AccessToken,AccessToken.TokenSecret));

            httpClient.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
        }

        public virtual void Initialize()
        {
            httpClient = OAuthUtility.CreateOAuthClient(ConsumerData.ConsumerKey, ConsumerData.ConsumerSecret, new AccessToken(AccessToken.AccessToken, AccessToken.TokenSecret));

            httpClient.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
            

        }
    }
}
