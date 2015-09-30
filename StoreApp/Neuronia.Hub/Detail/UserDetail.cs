using GalaSoft.MvvmLight;
using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail
{
    public class UserDetail:DetailBase
    {
        User userInformation;

        public User UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; ModelPropertyChanged("UserInformation"); }
        }

        public ObservableCollection<TimelineRow> TimeLine { get; set; }

        public UserDetail()
        {
            TimeLine = new ObservableCollection<TimelineRow>();
            UserInformation = new User();
        }

        public void Set(User user, List<TimelineRow> row)
        {
            this.UserInformation = user;
            TimeLine.Clear();
            foreach (var i in row)
            {
                TimeLine.Add(i);
            }
        }
    }
}
