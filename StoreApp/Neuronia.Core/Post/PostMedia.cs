using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Neuronia.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

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
