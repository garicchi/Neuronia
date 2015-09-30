using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace KeyBindManager.Control
{
    public class ReturnTextBox:TextBox
    {
        public event KeyEventHandler EnterDown;
        public ReturnTextBox()
        {
            this.AcceptsReturn = true;
        }
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == VirtualKey.Enter)
            {
                EnterDown(this, e);
            }

        }
    }
}
