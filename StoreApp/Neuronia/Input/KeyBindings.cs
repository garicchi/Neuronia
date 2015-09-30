using KeyBindManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Neuronia.Input
{
    public static class KeyBindings
    {
        public static KeyBinder TweetKeyBinder = new KeyBinder()
        {
            KeyBindList = new List<KeyBind>()
            {
                new KeyBind(VirtualKey.F,"Favorite"),
                new KeyBind(VirtualKey.R,"Reply"),
                new KeyBind(VirtualKey.V,"Retweet"),
                new KeyBind(VirtualKey.D,"Detail"),
                new KeyBind(VirtualKey.U,"User")
                
            }
        };
        public static KeyBinder PostTextKeyBinder = new KeyBinder()
        {
            KeyBindList = new List<KeyBind>()
            {
                new KeyBind(VirtualKey.Control,VirtualKey.Enter,"PostTweet"),
                new KeyBind(VirtualKey.Control,VirtualKey.K,"DownTab"),
                new KeyBind(VirtualKey.Control,VirtualKey.J,"UpTab"),
                
            }
        };
        public static KeyBinder PageTextKeyBinder = new KeyBinder()
        {
            KeyBindList = new List<KeyBind>()
            {
                new KeyBind(VirtualKey.Control,VirtualKey.Enter,"PostTweet"),
                new KeyBind(VirtualKey.Control,VirtualKey.K,"DownTab"),
                new KeyBind(VirtualKey.Control,VirtualKey.J,"UpTab"),
                
            }
        }; 
    }
}
