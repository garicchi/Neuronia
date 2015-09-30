using KeyBindManager;
using KeyBindManager.Control;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;


namespace Neuronia.Input
{
    public class TweetKeyManager : KeyManager
    {
        public TweetKeyManager(ReturnTextBox element, KeyBinder binder)
            : base(element, binder)
        {
            element.EnterDown += element_EnterDown;
        }

        private void element_EnterDown(object s, KeyRoutedEventArgs e)
        {
            foreach (KeyBind bind in KeyBinder.KeyBindList)
            {
                if (bind.CheckKeyDown(e.Key))
                {
                    if (CommandList.ContainsKey(bind.CommandCode))
                    {
                        CommandList[bind.CommandCode](bind);
                    }
                }
            }
        }
    }
    

    


}
