using Neuronia.Core.Tweets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Twitter;
using Neuronia.Core.Data;
using AsyncOAuth;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Neuronia.Core.Tweets.Delete;

namespace Neuronia.Core.TwitterStream
{
    public class FollowStream:TwitterStreamBase
    {

        public event Func<Tweet, Task> GetStreamRetweet;
        public event Func<Tweet, Task> GetStreamRetweeted; 
        public event Func<Tweet, Task> GetStreamMention;
        public event Func<Tweet, Task> GetStreamMe;

        public event Func<TweetDelete, Task> GetStreamDelete;
        public FollowStream(ConsumerData consumer,AccessTokenData token,User user)
            :base(consumer,token,user,TwitterUrl.FilterStreamingUrl+"?follow="+user.id_str)
        {
            GetStreamMe += async (e) => { };
            GetStreamMention += async (e) => { };
            GetStreamRetweet += async (e) => { };
            GetStreamDelete += async (e) => { };
            GetStreamRetweeted += async (e) => { };
        }

        protected override async Task StreamProcess(string s)
        {
            JObject obj = null;
            await Task.Run(() =>
            {
                obj = JObject.Parse(s);
            });
            if (obj["delete"]!=null)
            {
               await GetStreamDelete(await obj["delete"].ToString().JsonDeseliazeAsync<TweetDelete>());
            }else if (obj["disconnect"] != null)
            {

            }
            else
            {

                Tweet tweet = await s.JsonDeseliazeAsync<Tweet>();
                if (tweet.retweeted_status == null && tweet.text.Contains("@" + UserInformation.screen_name))
                {
                    await GetStreamMention(tweet);
                }
                else if (tweet.retweeted_status != null&&tweet.user.screen_name==UserInformation.screen_name)
                {
                    await GetStreamRetweet(tweet);
                }
                else if (tweet.retweeted_status != null &&
                         tweet.retweeted_status.user.screen_name == UserInformation.screen_name)
                {
                    await GetStreamRetweeted(tweet);
                }
                else
                {
                    await GetStreamMe(tweet);
                }
                
                

            }
            
        }
        
    }
}
