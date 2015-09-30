using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Common;
using Neuronia.Core.Data;

namespace Neuronia.Core.Twitter
{
    public class TwitterAuthorizer:ModelBase
    {
        
        public RequestToken requestToken { get; set; }

        public OAuthAuthorizer authorizer { get; set; }

        
        public TwitterAuthorizer()
        {
            
        }

        public async Task<string> BeginAuthorizedAsync(ConsumerData data)
        {
            authorizer = new OAuthAuthorizer(data.ConsumerKey,data.ConsumerSecret);

             var tokenResponse = await authorizer.GetRequestToken(TwitterUrl.GetRequestTokenUrl);
             requestToken = tokenResponse.Token;
            
            var pinRequestUrl = authorizer.BuildAuthorizeUrl(TwitterUrl.AuthorizeUrl, requestToken);
            return pinRequestUrl;
        }

        public async Task<TokenResponse<AccessToken>> PinAuthorizedAsync(string pin)
        {
            var res= await authorizer.GetAccessToken(TwitterUrl.GetAccessTokenUrl, requestToken, pin);
            
            return res;
        }

        
    }

    
}
