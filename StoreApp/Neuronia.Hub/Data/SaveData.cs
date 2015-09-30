using Windows.Storage.Streams;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Tab;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Neuronia.Hub.Data
{
    public static class SaveData
    {
        
       

        public static bool IsTwitterDataInitializeCompleted()
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey("TwitterDataInitializeCompleted");
            
        }

        public static bool IsSettingDataInitializeCompleted()
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey("SettingDataInitializeCompleted");

        }

        public static bool IsFirstVersion()
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey("Initialize");
          
        }

        public static async Task SaveSettingAsync(SettingData data)
        {
            ApplicationData.Current.LocalSettings.Values["SettingDataInitializeCompleted"] = true;
            await DataSerializer.DataContractSerializeAsync("SettingData", typeof(SettingData), data);
            await WriteBytesStorageFileAsync("MainBackground",data.AppTheme.MainBackground.UIImage);
            await WriteBytesStorageFileAsync("AppBarTopBackground", data.AppTheme.TopAppBarBackground.UIImage);
            await WriteBytesStorageFileAsync("AppBarBottomBackground", data.AppTheme.BottomAppBarBackground.UIImage);
            await WriteBytesStorageFileAsync("TweetBarBottomBackground", data.AppTheme.BottomTweetBarBackground.UIImage);
            await WriteBytesStorageFileAsync("SettingsFlyoutBackground", data.AppTheme.SettingsFlyoutBackground.UIImage);
            
        }

        public static async Task<SettingData> LoadSettingAsync()
        {
            SettingData resultData=new SettingData();
            
                resultData = await DataSerializer.DataContractDeselializeAsync("SettingData", typeof(SettingData)) as SettingData;
                resultData.AppTheme.MainBackground.UIImage = await ReadBytesStorageFileAsync("MainBackground");
                resultData.AppTheme.TopAppBarBackground.UIImage = await ReadBytesStorageFileAsync("AppBarTopBackground");
                resultData.AppTheme.BottomAppBarBackground.UIImage = await ReadBytesStorageFileAsync("AppBarBottomBackground");
                resultData.AppTheme.BottomTweetBarBackground.UIImage = await ReadBytesStorageFileAsync("TweetBarBottomBackground");
                resultData.AppTheme.SettingsFlyoutBackground.UIImage = await ReadBytesStorageFileAsync("SettingsFlyoutBackground");
                resultData.SettingConstInitialize();
            return resultData;
            
        }

        public static async Task SaveTwitterDataAsync(TwitterData data)
        {
            ApplicationData.Current.LocalSettings.Values["TwitterDataInitializeCompleted"] = true;
            await DataSerializer.DataContractSerializeAsync("TwitterData", typeof (TwitterData), data);
        }

        public static async Task<TwitterData> LoadTwitterDataAsync()
        {
            return await DataSerializer.DataContractDeselializeAsync("TwitterData", typeof (TwitterData)) as TwitterData;
        }

        private static async Task WriteBytesStorageFileAsync(string name, byte[] data)
        {

            StorageFile file =await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));
                writer.WriteBytes(data);
                await writer.StoreAsync();
                await writer.FlushAsync();
                writer.DetachStream();
            }
            
        }

        
        

        private static async Task<byte[]> ReadBytesStorageFileAsync(string name)
        {
            var file =await ApplicationData.Current.LocalFolder.GetFileAsync(name);
            var readStream=await file.OpenAsync(FileAccessMode.ReadWrite);
            var size = readStream.Size;
            byte[] buffer=new byte[size];
            DataReader reader=new DataReader(readStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)size);
            reader.ReadBytes(buffer);
            return buffer;
        }

        

    }
}
