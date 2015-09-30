using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.Stream;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Twitter;
using Neuronia.Core.Data;
using Newtonsoft.Json.Linq;
using AsyncOAuth;
using Neuronia.Core.Tweets.Delete;
using Neuronia.Core.Tweets.DirectMessage;
using System.Diagnostics;

namespace Neuronia.Core.TwitterStream
{
    public class UserStream:TwitterStreamBase
    {
        
#region StreamDelegate
        public event Func<Tweet, Task> GetStreamHome;

        public event Func<Tweet, Task> GetStreamMentions;

        public event Func<Tweet, Task> GetStreamMe;


        public event Func<DirectMessage, Task> GetStreamDirectMessage;

        public event Func<TweetDelete, Task> GetStreamDelete;

        public event Func<Tweet, Task> GetStreamRetweet;

        public event Func<TwitterEvent, Task> GetStreamDeauthorizes;

        public event Func<TwitterEvent, Task> GetStreamBlock;


        public event Func<TwitterEvent, Task> GetStreamUnBlock;

        public event Func<TwitterEvent, Task> GetStreamFavorited;

        public event Func<TwitterEvent, Task> GetStreamIsFavorited;

        public event Func<TwitterEvent, Task> GetStreamUnFavorited;

        public event Func<TwitterEvent, Task> GetStreamIsUnFavorited;

        public event Func<TwitterEvent, Task> GetStreamFollow;

        public event Func<TwitterEvent, Task> GetStreamIsFollowed;

        public event Func<TwitterEvent, Task> GetStreamUnFollow;

        public event Func<TwitterEvent, Task> GetStreamCreateList;

        public event Func<TwitterEvent, Task> GetStreamDeleteList;

        public event Func<TwitterEvent, Task> GetStreamEditList;

        public event Func<TwitterEvent, Task> GetStreamAddList;

        public event Func<TwitterEvent, Task> GetStreamIsAddedList;

        public event Func<TwitterEvent, Task> GetStreamRemoveList;

        public event Func<TwitterEvent, Task> GetStreamIsRemovedList;

        public event Func<string, Task> GetStreamSystemMessage;
#endregion
        

        public UserStream(ConsumerData consumerData,AccessTokenData token,User user)
            :base(consumerData,token,user,TwitterUrl.UserStreamingUrl)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            GetStreamHome += async (s) => { };
            GetStreamDirectMessage += async (s) => { };
            GetStreamDelete += async (s) => { };
            GetStreamRetweet += async (s) => { };
            GetStreamDeauthorizes += async (s) => { };
            GetStreamBlock += async (s) => { };
            GetStreamUnBlock += async (s) => { };
            GetStreamFavorited += async (s) => { };
            GetStreamIsFavorited += async (s) => { };
            GetStreamUnFavorited += async (s) => { };
            GetStreamIsUnFavorited += async (s) => { };
            GetStreamFollow += async (s) => { };
            GetStreamIsFollowed += async (s) => { };
            GetStreamUnFollow += async (s) => { };
            GetStreamCreateList += async (s) => { };
            GetStreamDeleteList += async (s) => { };
            GetStreamEditList += async (s) => { };
            GetStreamAddList += async (s) => { };
            GetStreamIsAddedList += async (s) => { };
            GetStreamRemoveList += async (s) => { };
            GetStreamIsRemovedList += async (s) => { };
            GetStreamSystemMessage += async (s) => { };
        }

        protected override async Task StreamProcess(string s)
        {
            try
            {

                JObject obj = null;
                await Task.Run(() =>
                {
                    obj = JObject.Parse(s);
                });



                if (obj["friends"] != null)
                {
                    var e = await obj["friends"].ToString().JsonDeseliazeAsync<List<string>>();

                    await GetStreamSystemMessage("ストリームコネクションを確立しました");
                }
                
                if (obj["event"] != null)
                {
                    var isEvent = obj["event"];
                    var e = await s.JsonDeseliazeAsync<TwitterEvent>();
                    if (e != null)
                    {
                        if (isEvent.ToString() == "access_revoked")
                        {
                            await GetStreamDeauthorizes(e);
                        }
                        if (isEvent.ToString() == "block")
                        {
                            await GetStreamBlock(e);
                        }
                        if (isEvent.ToString() == "unblock")
                        {
                            await GetStreamUnBlock(e);
                        }
                        if (isEvent.ToString() == "favorite" && e.source.id_str == UserInformation.id_str)
                        {
                            await GetStreamFavorited(e);
                        }
                        if (isEvent.ToString() == "favorite" && e.target.id_str == UserInformation.id_str)
                        {
                            await GetStreamIsFavorited(e);
                        }

                        if (isEvent.ToString() == "unfavorite" && e.source.id_str == UserInformation.id_str)
                        {
                            await GetStreamUnFavorited(e);
                        }
                        if (isEvent.ToString() == "unfavorite" && e.target.id_str == UserInformation.id_str)
                        {
                            await GetStreamIsUnFavorited(e);
                        }


                        if (isEvent.ToString() == "follow" && e.source.id_str == UserInformation.id_str)
                        {
                            await GetStreamFollow(e);
                        }
                        if (isEvent.ToString() == "follow" && e.target.id_str == UserInformation.id_str)
                        {
                            await GetStreamIsFollowed(e);
                        }

                        if (isEvent.ToString() == "unfollow")
                        {
                            await GetStreamUnFollow(e);
                        }



                    }
                }
                else
                {
                    var dm = obj["direct_message"];
                    var del = obj["delete"];
                    var retweet = obj["retweeted_status"];
                    if (dm != null)
                    {
             
                        var message = await dm.ToString().JsonDeseliazeAsync<DirectMessage>();

                        await GetStreamDirectMessage(message);
                    }
                    else if (del != null)
                    {
                        var message = await del.ToString().JsonDeseliazeAsync<TweetDelete>();

                        await GetStreamDelete(message);
                    }
                    else if (retweet != null)
                    {
                        var message = await s.JsonDeseliazeAsync<Tweet>();

                        await GetStreamHome(message);
                    }
                    else if (obj["text"] != null)
                    {
                        var tweet = await s.JsonDeseliazeAsync<Tweet>();

                        await GetStreamHome(tweet);
                    }
                }
            }
            catch (OverflowException e)
            {

            }
        }

        
    }
}
