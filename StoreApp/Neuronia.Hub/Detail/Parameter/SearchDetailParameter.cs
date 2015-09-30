using Neuronia.Core.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail.Parameter
{
    public class SearchDetailParameter:DetailParameterBase
    {
        public string SearchWord { get; set; }
        public SearchDetailParameter(string accountScreenName,string searchWord)
            :base(accountScreenName)
        {
            this.SearchWord = searchWord;
        }
    }
}
