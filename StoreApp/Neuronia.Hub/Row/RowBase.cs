using Windows.UI;
using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Tweets;
using Windows.UI.Xaml.Media;
using Neuronia.Hub.Common;
using Neuronia.Hub.Data;

namespace Neuronia.Hub.Row
{
    [DataContract]
    [KnownType(typeof(TimelineRow))]
    [KnownType(typeof(DirectMessageRow))]
    [KnownType(typeof(NotificationRow))]
    public class RowBase:ModelBase
    {
        private string ownerScreenName;
        [DataMember]
        public string OwnerScreenName
        {
            get { return ownerScreenName; }
            set { ownerScreenName = value; ModelPropertyChanged("OwnerScreenName"); }
        }

        private RowType rowType;
        [DataMember]
        public RowType RowType
        {
            get { return rowType; }
            set { rowType = value; ModelPropertyChanged("RowType"); }
        }

        private Tweet tweet;
        [DataMember]
        public Tweet Tweet
        {
            get { return tweet; }
            set { tweet = value; ModelPropertyChanged("Tweet"); }
        }

        private Color barColorBrush;
        [DataMember]
        public Color BarColorBrush
        {
            get { return barColorBrush; }
            set { barColorBrush = value; ModelPropertyChanged("BarColorBrush"); }
        }

        [IgnoreDataMember]
        protected Action<RowAction> rowActionCallback;

        private SettingData setting;

        [DataMember]
	    public SettingData Setting
	    {
		    get { return setting;}
            set { setting = value; ModelPropertyChanged("Setting"); }
	    }

        
	


        public RowBase(Tweet tweet,string ownerScreenName,SettingData setting,Action<RowAction> rowActionCallBack,RowType type)
        {
            this.ownerScreenName = ownerScreenName;
            this.RowType = type;
            this.Tweet = tweet;
            this.setting = setting;
            
            InitializeBase(rowActionCallBack);
        }

        public void InitializeBase(Action<RowAction> rowActionCallBack)
        {
            this.rowActionCallback = rowActionCallBack;
        }
    }
}
