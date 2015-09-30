using GalaSoft.MvvmLight;
using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using Neuronia.Hub.Common;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail
{
    public class TweetDetail:DetailBase
    {
        private TimelineRow row;

        public TimelineRow Row
        {
            get { return row; }
            set { row = value; ModelPropertyChanged("Row"); }
        }
        public ObservableCollection<TimelineRow> ReplyList { get; set; }

        public TweetDetail(TimelineRow row)
        {
            Row = row;
            ReplyList = new ObservableCollection<TimelineRow>();
            
        }

        public void Set(TimelineRow row,List<TimelineRow> replyList)
        {
            Row = row;
            ReplyList.Clear();
            foreach (var i in replyList)
            {

                ReplyList.Add(i);
            }
        }
    
    }
}
