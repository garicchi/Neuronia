using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Neuronia.Core.Common
{
   [DataContract]
    public class ModelBase:INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        //モデルの既定とクラス
        public ModelBase()
        {
        }

        //プロパティ変更通知時にnullで例外が発生するのを防ぐために
        //こちらを利用する
        protected void ModelPropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }

        

       /* public IEnumerator<ModelBase> GetEnumerator()
        {
            //throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            //throw new NotImplementedException();
        }

        IEnumerator<ModelBase> IEnumerable<ModelBase>.GetEnumerator()
        {
            //throw new NotImplementedException();
        }*/
        
        
    }
}
