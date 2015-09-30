using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Post
{
    public class PostStatusBase:ModelBase
    {
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; ModelPropertyChanged("Status"); }
        }
        public PostStatusBase(string status)
        {
            this.status = status;
        }


    }
}
