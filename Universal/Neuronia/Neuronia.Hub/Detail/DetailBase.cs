using Neuronia.Core.Common;
using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail
{
    public class DetailBase:ModelBase
    {

        private TwitterAccount ownerAccount;

        public TwitterAccount OwnerAccount
        {
            get { return ownerAccount; }
            set { ownerAccount = value; ModelPropertyChanged("OwnerAccount"); }
        }


        
    }
}
