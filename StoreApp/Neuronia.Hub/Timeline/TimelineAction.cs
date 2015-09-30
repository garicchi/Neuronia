using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Timeline
{
    public class TimelineAction
    {
        public object Parameter { get; set; }

        public TimelineActionType ActionType { get; set; }
        public TimelineAction(TimelineActionType type,object parameter)
        {
            this.ActionType = type;
            this.Parameter = parameter;
        }
    }
}
