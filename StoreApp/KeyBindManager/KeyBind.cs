using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace KeyBindManager
{
    public class KeyBind
    {
        public VirtualKey OptionKey { get; set; }

        public VirtualKey MainKey { get; set; }

        public string CommandCode { get; set; }

        public bool IsMainKeyOnly { get; set; }

        public bool IsOptionKeyDown { get; set; }

        public bool IsCallBackOn { get; set; }

        public KeyBind(VirtualKey optionKey, VirtualKey mainKey,string commandCode)
        {
            this.OptionKey = optionKey;
            this.MainKey = mainKey;
            IsMainKeyOnly = false;
            IsOptionKeyDown = false;
            IsCallBackOn = false;
            
            this.CommandCode = commandCode;

        }

        public KeyBind(VirtualKey mainKey,string commandCode)
        {
            this.MainKey = mainKey;
            IsMainKeyOnly = true;
            IsOptionKeyDown = false;
            IsCallBackOn = false;
            
            this.CommandCode = commandCode;
        }

        public bool CheckKeyDown(VirtualKey key)
        {

            if (IsMainKeyOnly == false)
            {
                if (IsOptionKeyDown == false && OptionKey == key)
                {
                    IsOptionKeyDown = true;

                }


                if (IsOptionKeyDown && key == MainKey && IsCallBackOn == false)
                {
                    IsCallBackOn = true;
                    return true;

                }
                else
                {
                    return false;
                }

               
            }
            else
            {
                if (key == MainKey && IsCallBackOn == false)
                {
                    IsCallBackOn = true;
                    return true;

                }
                else
                {
                    return false;
                }
            }

        }

        public void CheckKeyUp(VirtualKey key)
        {

            if (IsMainKeyOnly == false)
            {
                if (OptionKey == key)
                {
                    IsOptionKeyDown = false;

                }
            }
            IsCallBackOn = false;
        }


    }
}
