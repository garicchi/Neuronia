using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Extentions
{
    public static class StringExtention
    {
        public static int LengthInTextElements(this string str){
            return (new StringInfo(str)).LengthInTextElements;
        }

        public static string SubStringByTextElements(this string str,int startIndex,int length)
        {
            string resultStr = "";
            for (int i = startIndex; i <startIndex+length; i++)
            {
                resultStr += StringInfo.GetNextTextElement(str,i);
            }
            return resultStr;
        }

        public static string ReplaceSpecialCharactor(this string str)
        {
            return str.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }

        public static async Task<T> JsonDeseliazeAsync<T>(this string str)
        {
            //return await JsonConvert.DeserializeObjectAsync<T>(str);

            
            return await Task.Run(() =>
            {
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                
                var tabList = serializer.ReadObject(stream);
                return (T)tabList;
            });
             
        }
    }
}
