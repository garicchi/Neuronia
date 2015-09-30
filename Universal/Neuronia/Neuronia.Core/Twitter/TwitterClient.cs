using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Neuronia.Core.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Tweets.Errors;
using Neuronia.Core.Tweets;
using Neuronia.Core.Data;
using Neuronia.Core.Tweets.List;
using Neuronia.Core.Tweets.Search;
using System.Runtime.Serialization;

using AsyncOAuth;
using Neuronia.Core.Post;
using Neuronia.Core.Tweets.DirectMessage;
using Newtonsoft.Json.Linq;

namespace Neuronia.Core.Twitter
{
    [DataContract]
    public class TwitterClient : TwitterClientBase
    {

        public event Action<HttpRequestException> OnHttpGetError;

        public event Action<HttpRequestException> OnHttpPostError;

        public event Action<PostStatusBase> OnTweetBegin;

        public event Action<PostStatusBase> OnTweetFailed;

        public event Action<PostStatusBase> OnTweetCompleted;

        public TwitterClient(ConsumerData consumerData,AccessTokenData token)
            :base(consumerData,token)
        {
            

        }

        public override void Initialize()
        {

            base.Initialize();
            OnHttpGetError += ex => { };
            OnHttpPostError += (ex) => { };
            OnTweetBegin += (e) => { };
            OnTweetFailed += (e) => { };
            OnTweetCompleted += (e) => { };
        }
       

        private async Task<T> GetHttpRequestAsync<T>(string url)
        {
            try
            {
                var json = await HttpClient.GetStringAsync(url);

                JObject obj = JObject.Parse(json);

                JToken token = null;
                if (obj.TryGetValue("error",out token))
                {
                    var error = await json.JsonDeseliazeAsync<ErrorMessage>();

                    return default(T);
                }
                else
                {
                    json = json.ReplaceSpecialCharactor();
                    var tweets = await json.JsonDeseliazeAsync<T>();
                    return tweets;
                }

            }
            catch (HttpRequestException e)
            {
                OnHttpGetError(e);
                return default(T);
            }
            catch (Exception e)
            {
                return default(T);
            }
           
        }

        private async Task<bool> PostHttpRequestAsync(string url, HttpContent content)
        {

            try
            {
                var response = await HttpClient.PostAsync(url, content);
                var json = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(json);

                JToken token = null;
                if (obj.TryGetValue("error", out token))
                {
                    var error = await "".JsonDeseliazeAsync<ErrorMessage>();
                }
                return true;
            }
            catch (HttpRequestException e)
            {
                OnHttpPostError(e);
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        

        

        public async Task<List<Tweet>> GetHomeTimelineAsync(int count=10)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.HomeTimeLineUrl + "?count=" + count );
        }

        public async Task<List<Tweet>> GetHomeTimelineOldAsync(string maxId,int count = 200)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.HomeTimeLineUrl + "?count=" + count+"&max_id="+maxId);
        }

        public async Task<List<Tweet>> GetHomeTimelineNewAsync(string sinceId, int count = 200)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.HomeTimeLineUrl + "?count=" + count+"&since_id="+sinceId);
        }

        public async Task<List<Tweet>> GetMentionTimelineAsync(int count=10)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.MentionTimeLineUrl + "?count=" + count);

        }
        public async Task<List<Tweet>> GetMentionTimelineOldAsync(string maxId,int count = 200)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.MentionTimeLineUrl + "?count=" + count+"&max_id="+maxId);

        }
        public async Task<List<Tweet>> GetMentionTimelineNewAsync(string since_id,int count = 200)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.MentionTimeLineUrl + "?count=" + count+"&since_id="+since_id);

        }

        public async Task<List<string>> GetFriendsAsync(User user,int cursor=-1)
        {
            UserList list = await GetHttpRequestAsync<UserList>(TwitterUrl.GetFriendsUrl + "?cursor=" + cursor + "&screen_name=" + user.screen_name + "&skip_status=true&include_user_entities=false");

            List<string> friends = new List<string>();
            foreach (var t in list.users)
            {
                friends.Add(t.screen_name);
            }
            return friends;

        }

        public async Task<List<DirectMessage>> GetDirectMessages(int count=200)
        {
            var dms=await GetHttpRequestAsync<List<DirectMessage>>(TwitterUrl.GetDirectMessagesUrl+"?count="+count);
            return dms;
        }

        public async Task<List<DirectMessage>> GetDirectMessages(string since_id,int count = 200)
        {
            var dms = await GetHttpRequestAsync<List<DirectMessage>>(TwitterUrl.GetDirectMessagesUrl+"?since_id="+since_id+"&count="+count);
            return dms;
        }

        public async Task<DirectMessage> GetDirectMessageShow(string id)
        {
            var dms = await GetHttpRequestAsync<DirectMessage>(TwitterUrl.GetDirectMessageShowUrl+"?id="+id);
            return dms;
        }

        public async Task<List<DirectMessage>> GetDirectMessageSent(int count=200)
        {
            var dms = await GetHttpRequestAsync<List<DirectMessage>>(TwitterUrl.GetDirectMessageSentUrl + "?count="+count);
            return dms;
        }

        public async Task<List<DirectMessage>> GetDirectMessageSent(string since_id,int count = 200)
        {
            var dms = await GetHttpRequestAsync<List<DirectMessage>>(TwitterUrl.GetDirectMessageSentUrl + "?since_id="+since_id+"&count=" + count);
            return dms;
        }

        public async Task PostDirectMessageNew(string screen_name,string text)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("screen_name", screen_name),new KeyValuePair<string,string>("text",text) });
            await PostHttpRequestAsync(TwitterUrl.PostDirectMessageNewUrl,content);
        }

        public async Task PostDirectMessageDestroy(string id)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("id",id) });
            await PostHttpRequestAsync(TwitterUrl.PostDirectMessageDestroyUrl, content);
        }


        public async Task<List<Tweet>> GetConversationAsync(Tweet tweet)
        {
            List<Tweet> list = new List<Tweet>();

            Tweet tempTweet = tweet;
            while (true)
            {
                if (tempTweet.in_reply_to_status_id_str == null)
                {
                    break;
                }
                else
                {
                    Tweet t = await GetStatusAsync(tempTweet.in_reply_to_status_id_str.ToString());
                    if (t.text == null)
                    {
                        break;
                    }
                    list.Add(t);
                    tempTweet = t;
                }
            }
            return list;
        }

        public async Task<User> GetAccountInformationAsync(string screen_name)
        {

            User ac = await GetHttpRequestAsync<User>(TwitterUrl.GetUserInfoUrl + "?screen_name=" + screen_name);

            return ac;
        }


        private async Task UpdateStatusBaseAsync(PostStatusBase status,HttpContent content)
        {
            string url = string.Empty;
            if (status is PostStatusMedia || status is PostStatusMediaWithReply)
            {
                url = TwitterUrl.PostStatusWithMediaUrl;
            }
            else
            {
                url = TwitterUrl.PostUrl;
            }
            OnTweetBegin(status);
            var isComplete=await PostHttpRequestAsync(url, content);
            if (isComplete)
            {
                OnTweetCompleted(status);
            }
            else
            {
                OnTweetFailed(status);
            }
        }

        public async Task UpdateStatusAsync(PostStatus status)
        {
            
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("status", status.Status) });
            await UpdateStatusBaseAsync(status,content);
        }

        public async Task UpdateStatusAsync(PostStatusWithReply status)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("status",status.Status), new KeyValuePair<string, string>("in_reply_to_status_id",status.InReplyToStatusId) });
            await UpdateStatusBaseAsync(status, content);
        }

        public async Task UpdateStatusWithMediaAsync(PostStatusMedia status)
        {

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(status.Status), "\"status\"");
            content.Add(new ByteArrayContent(status.Media.Data), "media[]", "\"" + status.Media.FileName + "\"");
            await UpdateStatusBaseAsync(status, content);
        }

        public async Task UpdateStatusWithMediaAsync(PostStatusMediaWithReply status)
        {

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(status.Status), "\"status\"");
            content.Add(new StringContent(status.InReplyToStatusId), "\"in_reply_to_status_id\"");
            content.Add(new ByteArrayContent(status.Media.Data), "media[]", "\"" + status.Media.FileName + "\"");

            await UpdateStatusBaseAsync(status, content);
        }

        

        public async Task CreateFavoriteAsync(Tweet tweet)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("id", tweet.id_str) });
            await PostHttpRequestAsync(TwitterUrl.CreateFavoriteUrl, content);


        }

        public async Task DestroyFavoriteAsync(Tweet tweet)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("id", tweet.id_str) });

            await PostHttpRequestAsync(TwitterUrl.DestroyFavoriteUrl, content);

        }

        public async Task DestroyStatusAsync(Tweet tweet)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("id", tweet.id_str) });

            await PostHttpRequestAsync(TwitterUrl.DestroyStatusUrl+tweet.id_str+".json",content);
        }

        public async Task CreateRetweetAsync(Tweet tweet)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("id", tweet.id_str) });

            await PostHttpRequestAsync(TwitterUrl.CreateRetweetUrl + tweet.id_str + ".json", content);

        }

        public async Task<List<Tweet>> GetListAsync(TwitterList list,int count=10,int page=0)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.GetListUrl + "?list_id=" + list.id_str + "&count=" + count + "&page=" + page);

        }

        public async Task<List<Tweet>> GetListAsync(TwitterList list,string sinceId, int count = 10, int page = 0)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.GetListUrl + "?list_id=" + list.id_str +"&since_id="+sinceId+ "&count=" + count + "&page=" + page);

        }

        public async Task<List<TwitterList>> GetListListAsync(User user)
        {
            return await GetHttpRequestAsync<List<TwitterList>>(TwitterUrl.GetListListUrl + "?user_id=" +user.id_str);

        }

        public async Task<TwitterSearch> GetSearchAsync(string searchWord,int count=10)
        {

            searchWord = Uri.EscapeDataString(searchWord);
            var t = await GetHttpRequestAsync<TwitterSearch>(TwitterUrl.GetSearchUrl + "?q=" + searchWord + "&count=" + count);
            return t;
        }

        public async Task<List<Tweet>> GetUserTimeLineAsync(string screenName,int count=10)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.GetUserTimelineUrl + "?screen_name=" + screenName + "&count=" + count);

        }

        public async Task<List<Tweet>> GetUserTimeLineAsync(string screenName,string sinceId, int count = 10)
        {
            return await GetHttpRequestAsync<List<Tweet>>(TwitterUrl.GetUserTimelineUrl + "?screen_name=" + screenName +"&since_id="+sinceId+ "&count=" + count);

        }

        public async Task<User> GetUserAsync(long user_id)
        {
            return await GetHttpRequestAsync<User>(TwitterUrl.GetUserUrl + "?user_id=" + user_id.ToString());
        }

        public async Task<User> GetUserAsync(string screen_name)
        {
            return await GetHttpRequestAsync<User>(TwitterUrl.GetUserUrl + "?screen_name=" + screen_name);
        }

        public async Task<Tweet> GetStatusAsync(string id)
        {
            return await GetHttpRequestAsync<Tweet>(TwitterUrl.GetStatusUrl + "?id=" + id);
        }

       

    }
}

