using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace KeyBindManager
{
    public class KeyManager
    {
        protected UIElement element;
        public KeyBinder KeyBinder { get; set; }

        public IDictionary<string,Action<KeyBind>> CommandList { get; set; }
        public KeyManager(UIElement element, KeyBinder keyBinder)
        {
            this.element = element;
            element.KeyDown += element_KeyDown;
            element.KeyUp += element_KeyUp;
            KeyBinder = keyBinder;

            CommandList = new Dictionary<string,Action<KeyBind>>();
            
        }
        private void element_KeyDown(object s, KeyRoutedEventArgs e)
        {

           foreach (KeyBind bind in KeyBinder.KeyBindList)
            {
                if (bind.CheckKeyDown(e.Key))
                {
                    if(CommandList.ContainsKey(bind.CommandCode))
                    {
                        CommandList[bind.CommandCode](bind);
                    }
                }
            }
        }

        private void element_KeyUp(object s, KeyRoutedEventArgs e)
        {
            foreach (KeyBind bind in KeyBinder.KeyBindList)
            {
                bind.CheckKeyUp(e.Key);
            }
        }


    }
}
