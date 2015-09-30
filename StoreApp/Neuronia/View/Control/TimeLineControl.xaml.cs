using KeyBindManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Neuronia.Common;
using Neuronia.Hub.Timeline;
using Neuronia.Hub.Row;
using Neuronia.Input;
using System.Diagnostics;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia.View
{
    public sealed partial class TimeLineControl : UserControl
    {
        KeyManager keyManager;
        public TimeLineControl()
        {
            this.InitializeComponent();
            
            keyManager = new KeyManager(listViewTimeline, KeyBindings.TweetKeyBinder);
            keyManager.CommandList.Add("Favorite", (bind) =>
            {
                if (listViewTimeline.SelectedIndex != -1)
                {
                    //(DataContext as TimelineBase).ViewModel.FavoriteCommand.Execute((listViewTimeline.SelectedItem as TimelineRow).Tweet);
                }
            });
            keyManager.CommandList.Add("Retweet", (bind) =>
            {
                if (listViewTimeline.SelectedIndex != -1)
                {
                    //(DataContext as TimelineBase).ViewModel.RetweetCommand.Execute((listViewTimeline.SelectedItem as TimelineRow).Tweet);
                }
            });
            keyManager.CommandList.Add("Detail", (bind) =>
            {
                if (listViewTimeline.SelectedIndex != -1)
                {
                   // (DataContext as TimelineBase).ViewModel.TweetDetailCommand.Execute((listViewTimeline.SelectedItem as TimelineRow).Tweet);
                }
            });
            keyManager.CommandList.Add("Reply", (bind) =>
            {
                if (listViewTimeline.SelectedIndex != -1)
                {
                  //  (DataContext as TimelineBase).ViewModel.ReplyCommand.Execute((listViewTimeline.SelectedItem as TimelineRow).Tweet);
                }
            });
        }

        private void btn_timlineClose_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void listViewTimeline_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBoxCompressionHandling(sender as ListView);
        }

        private void ListBoxCompressionHandling(ListView targetlistbox)
        {
            VisualStateGroup vgroup = new VisualStateGroup();
            var border=VisualTreeHelper.GetChild(targetlistbox,0);
            var scrollViewer=VisualTreeHelper.GetChild(border,0);
            // ListBox の初めに定義されている ScrollViewerを取り出す 
            ScrollViewer ListViewScrollViewer = (ScrollViewer)scrollViewer;

            // Visual State はコントロールテンプレートの常に最上位に定義されている 
            FrameworkElement element = (FrameworkElement)VisualTreeHelper.GetChild(ListViewScrollViewer, 0);
            // Visual State を取り出しその中から 縦横Compression のVisualStateを取り出す 
            foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(element))
                if (group.Name == "VerticalCompression") vgroup = group;

            //縦横Compressionの状態が変わった時のイベントハンドラ 
            vgroup.CurrentStateChanging += (s,e) =>
            {
                switch (e.NewState.Name) 
                { 
                    case "CompressionTop":
                        break; 
                    case "CompressionBottom":
                        break; 
                    case "NoVerticalCompression": 
                        break; 
                    default: 
                        break; 
                } 


            };
        } 

        

        

        
    }
}
