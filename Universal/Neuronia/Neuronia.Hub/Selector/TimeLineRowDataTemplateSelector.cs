using Neuronia.Hub.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Neuronia.Hub.Selector
{
    public class TimelineRowDataTemplateSelector : DataTemplateSelector
    {
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            var row = item as RowBase;

            if (element != null && row != null)
            {
                if (row is DirectMessageRow)
                {
                    var drow = (row as DirectMessageRow);
                    return Application.Current.Resources["DirectMessageRowDataTemplate"] as DataTemplate;
                }
                else if (row is NotificationRow)
                {
                    var drow = (row as NotificationRow);
                    return Application.Current.Resources["NotificationRowDataTemplate"] as DataTemplate;
                }
                else if (row is TimelineRow)
                {
                    if ((row as TimelineRow).Tweet.retweeted_status == null && row.OwnerScreenName == (row as TimelineRow).Tweet.user.screen_name)
                    {
                        return Application.Current.Resources["MyTweetRowDataTemplate"] as DataTemplate;
                    }
                    else if ((row as TimelineRow).Tweet.retweeted_status == null)
                    {

                        return Application.Current.Resources["TweetRowDataTemplate"] as DataTemplate;
                    }
                    else
                    {
                        return Application.Current.Resources["ReTweetRowDataTemplate"] as DataTemplate;
                    }
                }
                
            }

            return null;


        }
    }
}
