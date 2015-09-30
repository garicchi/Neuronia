using System.IO;
using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Post
{
    public class PostMedia:ModelBase
    {

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; ModelPropertyChanged("FileName"); }
        }

        private byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; ModelPropertyChanged("Data"); }
        }


        
        public PostMedia()
        {
            Data=new byte[]{};
        }


        public void Reset()
        {
            FileName = "";
            Data = new byte[] { };
            
        }
    }
}
