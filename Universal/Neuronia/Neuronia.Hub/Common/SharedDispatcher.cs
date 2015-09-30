using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Neuronia.Hub.Common
{
    public static class SharedDispatcher
    {
        public static CoreDispatcher Dispatcher;

        public static async Task RunAsync(Action action)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                action();
            });
           
        }
    }
}
