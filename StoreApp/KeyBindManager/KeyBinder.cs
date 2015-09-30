using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindManager
{
    public class KeyBinder
    {
        public IList<KeyBind> KeyBindList { get; set; }
        public KeyBinder()
        {
            KeyBindList = new List<KeyBind>();
        }
    }
}
