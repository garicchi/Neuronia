using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail.Parameter
{
    public class DetailParameterBase
    {


        public string OwnerScreenName { get; set; }
        public DetailParameterBase(string ownerScreenName)
        {
            this.OwnerScreenName = ownerScreenName;
        }
    }
}
