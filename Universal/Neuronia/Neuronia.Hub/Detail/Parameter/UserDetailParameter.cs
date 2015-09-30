using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail.Parameter
{
    public class UserDetailParameter:DetailParameterBase
    {
        public string ScreenName{get;set;}
        public UserDetailParameter(string accountScreenName,string screen_name)
            :base(accountScreenName)
        {
            this.ScreenName = screen_name;
        }
    }
}
