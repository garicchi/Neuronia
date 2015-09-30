using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Data
{
    [DataContract]
    public class AccessTokenData
    {
        [DataMember]
        public string AccessToken { get; set; }
        [DataMember]
        public string TokenSecret { get; set; }
        public AccessTokenData(string accessToken,string tokenSecret)
        {
            AccessToken = accessToken;
            TokenSecret = tokenSecret;
        }
    }
}
