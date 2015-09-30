using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Neuronia.Core.Extentions
{
 /*   public static class AsyncOAuthExtention
    {
        public static async Task<TokenResponse<RequestToken>> GetRequestTokenProxy(this OAuthAuthorizer authorizer,string url)
        {
            TokenResponse<RequestToken> response=null;
            try
            {
                response = await authorizer.GetRequestToken(url);
            }
            catch (Exception e)
            {
                var credentical =PickCredentical().Result;
                SetProxy(credentical);
                response =authorizer.GetRequestToken(url).Result;
                
            }
            return response;
        }

        public static async Task<TokenResponse<AccessToken>> GetAccessTokenProxy(this OAuthAuthorizer authorizer, string url, RequestToken token,string pin)
        {
            TokenResponse<AccessToken> response;
            try
            {
                response =await authorizer.GetAccessToken(url,token,pin);
            }
            catch (Exception e)
            {
                var credentical = PickCredentical().Result;
                SetProxy(credentical);
                response = authorizer.GetAccessToken(url,token,pin).Result;
            }
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsyncProxy(this HttpClient client,string url,HttpContent content)
        {
            HttpResponseMessage message=null;
            try
            {
                message = await client.PostAsync(url,content);
            }
            catch (Exception e)
            {
                var credentical = PickCredentical().Result;
                SetProxy(credentical);
                message = client.PostAsync(url,content).Result;
            }
            return message;
        }

        public static async Task<string> GetStringAsyncProxy(this HttpClient client, string url,Func<Task<NetworkCredential>> pickCredential)
        {
            
            string str = null;
            try
            {
                str = await client.GetStringAsync(url);
            }
            catch (Exception e)
            {
                NetworkCredential credential = pickCredential().Result;
                SetProxy(credential);
                str = client.GetStringAsync(url).Result;
            }
            return str;
        }


        private static async Task<NetworkCredential> PickCredentical()
        {
            CredentialPickerOptions credPickerOptions = new CredentialPickerOptions();
            credPickerOptions.Message = "認証情報を入力してください";
            credPickerOptions.Caption = "プロキシー認証";
            credPickerOptions.TargetName = "target";
            credPickerOptions.AuthenticationProtocol = AuthenticationProtocol.Basic;

            var picker = await CredentialPicker.PickAsync(credPickerOptions);
            return new NetworkCredential(picker.CredentialUserName, picker.CredentialPassword);
        }
        
        private static void SetProxy(NetworkCredential credentical)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = WebRequest.DefaultWebProxy;
            handler.Proxy.Credentials = credentical;
        }
    }
*/
}
