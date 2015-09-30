using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Common
{
    public static class StringToTimeManager
    {
        public static DateTime Convert(string time)
        {
            if (time == null) { return DateTime.Now; }
            string[] strs = time.Split(' ');
            string[] times = strs[3].Split(':');
            int month = 1;
            switch (strs[1])
            {
                case "Jan":
                    month = 1;
                    break;
                case "Feb":
                    month = 2;
                    break;
                case "Mar":
                    month = 3;
                    break;
                case "Apr":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "Jun":
                    month = 6;
                    break;
                case "Jul":
                    month = 7;
                    break;
                case "Aug":
                    month = 8;
                    break;
                case "Sep":
                    month = 9;
                    break;
                case "Oct":
                    month = 10;
                    break;
                case "Nov":
                    month = 11;
                    break;
                case "Dec":
                    month = 12;
                    break;
            }
            DateTime resultSpan = new DateTime(int.Parse(strs[5]), month, int.Parse(strs[2]), int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]), DateTimeKind.Utc);
            return resultSpan;
        }
    }
}
