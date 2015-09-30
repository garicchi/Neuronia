using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using Neuronia.Hub.Timeline;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Row;
using Neuronia.Core.Common;

namespace Neuronia.Hub.Tab
{
    [DataContract]
    public class TimelineTab:ModelBase
    {
        [DataMember]
        public int NotificationCount { get; set; }
        
        string tabTitle;

        [DataMember]
        public string TabTitle
        {
            get { return tabTitle; }
            set { tabTitle = value; ModelPropertyChanged("TabTitle"); }
        }
        
        private bool isNowTab;
        [DataMember]
        public bool IsNowTab
        {
            get { return isNowTab; }
            set { isNowTab = value; ModelPropertyChanged("IsNowTab"); }
        }

        [DataMember]
        public ObservableCollection<TimelineBase> TimelineList { get; set; }

        [IgnoreDataMember]
        Action<RowAction> rowActionCallback;

        [IgnoreDataMember]
        Action<TimelineAction> timelineActionCallback;

        [IgnoreDataMember]
        Action<TabAction> tabActionCallback;

        [IgnoreDataMember]
        public DelegateCommand EditTimelineTabCommand { get; set; }

        [IgnoreDataMember]
        public DelegateCommand DeleteTimelineTabCommand { get; set; }

        public TimelineTab(string tabTitle, Action<TabAction> tabActionCallback, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            Initialize(tabActionCallback,timelineActionCallback,rowActionCallback);
            this.TabTitle = tabTitle;
            TimelineList = new ObservableCollection<TimelineBase>();
            NotificationCount = 0;
            IsNowTab = false;
        }

        public void Initialize(Action<TabAction> tabActionCallback, Action<TimelineAction> timelineActionCallback, Action<RowAction> rowActionCallback)
        {
            this.tabActionCallback = tabActionCallback;
            this.timelineActionCallback = timelineActionCallback;
            this.rowActionCallback = rowActionCallback;
            
            EditTimelineTabCommand = new DelegateCommand(() =>
            {
                this.tabActionCallback(new TabAction(TabActionType.Edit,this));
            });
            DeleteTimelineTabCommand = new DelegateCommand(() =>
            {
                this.tabActionCallback(new TabAction(TabActionType.Delete, this));
            });
        }

        
    }
}
