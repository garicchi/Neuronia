using GalaSoft.MvvmLight;
using Neuronia.Core.Common;
using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Detail
{
    public class SearchDetail:DetailBase
    {
        string searchWord;

        public string SearchWord
        {
            get { return searchWord; }
            set { searchWord = value; ModelPropertyChanged("SearchWord"); }
        }
        public ObservableCollection<TimelineRow> TimeLine { get; set; }

        public SearchDetail()
        {
            TimeLine = new ObservableCollection<TimelineRow>();
        }

        public void Set(string searchWord, List<TimelineRow> row)
        {
            this.SearchWord = searchWord;
            TimeLine.Clear();
            foreach (var i in row)
            {
                this.TimeLine.Add(i);
            }
        }
    }
}
