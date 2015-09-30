using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Tab
{
    public class TabAction
    {
        public object Parameter { get; set; }

        public TabActionType ActionType { get; set; }
        public TabAction(TabActionType type,object parameter)
        {
            this.ActionType = type;
            this.Parameter = parameter;
        }
    }
}
