using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Data
{
    public static class TwitterUrl
    {
        public static string HomeTimeLineUrl = "https://api.twitter.com/1.1/statuses/home_timeline.json";
        public static string MentionTimeLineUrl = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
        public static string PostUrl = "https://api.twitter.com/1.1/statuses/update.json";
        public static string CreateFavoriteUrl = "https://api.twitter.com/1.1/favorites/create.json";
        public static string DestroyFavoriteUrl = "https://api.twitter.com/1.1/favorites/destroy.json";
        public static string CreateRetweetUrl = "https://api.twitter.com/1.1/statuses/retweet/";
        public static string GetUserInfoUrl = "https://api.twitter.com/1.1/users/show.json";
        public static string UserStreamingUrl = "https://userstream.twitter.com/1.1/user.json";
        public static string FilterStreamingUrl = "https://stream.twitter.com/1.1/statuses/filter.json";
        public static string GetFriendsUrl = "https://api.twitter.com/1.1/friends/list.json";
        public static string GetListListUrl = "https://api.twitter.com/1.1/lists/list.json";
        public static string GetListUrl = "https://api.twitter.com/1.1/lists/statuses.json";
        public static string GetUserTimelineUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        public static string GetSearchUrl = "https://api.twitter.com/1.1/search/tweets.json";
        public static string GetUserUrl = "https://api.twitter.com/1.1/users/show.json";
        public static string PostStatusWithMediaUrl = "https://api.twitter.com/1.1/statuses/update_with_media.json";
        public static string GetStatusUrl = "https://api.twitter.com/1.1/statuses/show.json";
        public static string GetDirectMessagesUrl = "https://api.twitter.com/1.1/direct_messages.json";
        public static string GetDirectMessageShowUrl = "https://api.twitter.com/1.1/direct_messages/show.json";
        public static string PostDirectMessageNewUrl = "https://api.twitter.com/1.1/direct_messages/new.json";
        public static string PostDirectMessageDestroyUrl = "https://api.twitter.com/1.1/direct_messages/destroy.json";
        public static string GetDirectMessageSentUrl = "https://api.twitter.com/1.1/direct_messages/sent.json";

        public static string GetRequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        public static string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        public static string GetAccessTokenUrl = "https://api.twitter.com/oauth/access_token";

        public static string DestroyStatusUrl = "https://api.twitter.com/1.1/statuses/destroy/";
    }
}
