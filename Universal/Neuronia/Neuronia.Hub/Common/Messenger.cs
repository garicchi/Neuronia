﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Common
{
    public static class Messenger
    {
        private static Dictionary<string, Action<object>> _notifiDic = new Dictionary<string, Action<object>>();

        public static void Register(string key, Action<object> onNotify)
        {
            _notifiDic.Add(key, onNotify);
        }

        public static void UnRegister(string key)
        {
            _notifiDic.Remove(key);
        }

        public static bool ContainsKey(string key)
        {
            return _notifiDic.ContainsKey(key);
        }

        public static void Clear()
        {
            _notifiDic.Clear();
        }

        public static void Notify(string key, object obj)
        {
            if (ContainsKey(key))
            {
                _notifiDic[key](obj);
            }
            else
            {

            }
        }
    }
}
