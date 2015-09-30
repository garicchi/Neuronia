using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Tab;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;

namespace Neuronia.Hub.Data
{
    public static class DataSerializer
    {

        public static async Task DataContractSerializeAsync(string fileName, Type type, object writeObject)
        {
            DataContractSerializer accountSerializer = new DataContractSerializer(type);
            var accountFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,CreationCollisionOption.ReplaceExisting);
            
            Stream accountStream = await accountFile.OpenStreamForWriteAsync();
            accountSerializer.WriteObject(accountStream, writeObject);
            accountStream.Dispose();
             
        }

        public static async Task<object> DataContractDeselializeAsync(string fileName, Type type)
        {
            var tabFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            
            
            DataContractSerializer serializer = new DataContractSerializer(type);
            Stream stream = await tabFile.OpenStreamForReadAsync();
            var tabList = serializer.ReadObject(stream);
            stream.Dispose();
            return tabList;
           
        }

        public static object DataContractDeselialize(string fileName, Type type)
        {
            var tabFile = ApplicationData.Current.LocalFolder.GetFileAsync(fileName).AsTask<StorageFile>().Result;
            DataContractSerializer serializer = new DataContractSerializer(type);
            Stream stream = tabFile.OpenStreamForReadAsync().Result;
            
            var tabList = serializer.ReadObject(stream);
            stream.Dispose();
            return tabList;
        }

        public static void DataContractSerialize(string fileName, Type type, object writeObject)
        {
            DataContractSerializer accountSerializer = new DataContractSerializer(type);
            StorageFile accountFile = ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).AsTask<StorageFile>().Result;
            Stream accountStream = accountFile.OpenStreamForWriteAsync().Result;
            accountSerializer.WriteObject(accountStream, writeObject);
            accountStream.Dispose();
        }

        
        
    }
}
