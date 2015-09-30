using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Row
{
    public class RowAction
    {
        public object Parameter { get; set; }

        public RowActionType ActionType { get; set; }
        public RowAction(RowActionType actionType,object parameter)
        {
            this.ActionType = actionType;
            this.Parameter = parameter;
        }
    }
}
