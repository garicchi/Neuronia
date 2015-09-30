using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia
{
    public sealed partial class NeuroniaRose : UserControl
    {
        public NeuroniaRose()
        {
            this.InitializeComponent();

            var story = Resources["roseOpenStoryboard"] as Storyboard;
            story.Begin();
        }

        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var story = Resources["roseOpenStoryboard"] as Storyboard;
            story.Begin();
        }
    }
}
