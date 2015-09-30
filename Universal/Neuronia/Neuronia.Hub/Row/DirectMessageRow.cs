using GalaSoft.MvvmLight.Command;
using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Hub.Data;
using Neuronia.Hub.Detail.Parameter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Row
{
    [DataContract]
    public class DirectMessageRow:RowBase
    {
        private DirectMessage directMessage;
        [DataMember]
        public DirectMessage DirectMessage
        {
            get { return directMessage; }
            set { directMessage = value; ModelPropertyChanged("DirectMessage"); }
        }

        

        public DelegateCommand DirectMessageCommand { get; set; }
         public DirectMessageRow(DirectMessage dm,string ownerScreenName,SettingData setting,Action<RowAction> actionCallback)
             :base(new Tweet(), ownerScreenName,setting,actionCallback,RowType.DirectMessage)
        {
            this.directMessage = dm;
            Initialize(rowActionCallback);
            
        }

         public void Initialize(Action<RowAction> rowActionCallBack)
         {
             CommandInitialize();
         }

         private void CommandInitialize()
         {
             DirectMessageCommand = new DelegateCommand(() =>
             {
                 rowActionCallback(new RowAction(RowActionType.DirectMessage, new DirectMessageDetailParameter(this.OwnerScreenName,this.DirectMessage)));
             });
         }


         
    }
}
