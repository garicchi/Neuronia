using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Neuronia.View
{
    public sealed partial class TwitterSuggestTextBox : UserControl
    {
        private SuggestModel model;
        char mention = '@';
        char hash = '#';


        public string TextTweet
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty,value); }
        }

        
        public static readonly DependencyProperty TextProperty =
  DependencyProperty.Register(
    "TextTweet", typeof(string), typeof(TwitterSuggestTextBox),
    new PropertyMetadata(null,OnSourceChanged)
  );

        // 依存関係プロパティに値がセットされたときに呼び出されるメソッド
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var i = d as TwitterSuggestTextBox;
            i.textBoxTweet.Text = (string)e.NewValue;
           // i.model.Text = (string)e.NewValue;
            
        }

        

        public Visibility IsMaxCountVisibly
        {
            get { return model.IsMaxCountVisibly; }
            set { this.model.IsMaxCountVisibly = value; }
        }

        public ObservableCollection<string> MentionSuggestList
        {
            get { return model.MentionSuggestSourceList; }
            set { this.model.MentionSuggestSourceList = value; }
        }

        public ObservableCollection<string> HashTagSuggestList
        {
            get { return model.HashSuggestSourceList; }
            set { this.model.HashSuggestSourceList = value; }
        }

        public TwitterSuggestTextBox()
        {
            this.InitializeComponent();
            model = new SuggestModel();
            
            this.DataContext = model;
            
        }

        private async void textBoxTweet_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = textBoxTweet.Text;
            ObservableCollection<string> resultSuggest = new ObservableCollection<string>();
            
                
                
                int mIndex = str.LastIndexOf(mention);
                int hIndex = str.LastIndexOf(hash);
                
                if (mIndex > hIndex)
                {
                    var s = str.Split(mention);
                    string name = s[s.Count() - 1];

                    int i = 0;
                    foreach (var n in model.MentionSuggestSourceList.Where(q => q.StartsWith(name)).Select(q => q))
                    {
                        resultSuggest.Add(mention+n);
                        if (i > 10)
                        {
                            break;
                        }
                        i++;
                    }
                }
                else if(mIndex<hIndex)
                {
                    var s = str.Split(hash);
                    string name = s[s.Count() - 1];
                    int i = 0;
                    foreach (var n in model.HashSuggestSourceList.Where(q => q.StartsWith(name)).Select(q => q))
                    {
                        resultSuggest.Add(hash+n);
                        if (i > 10)
                        {
                            break;
                        }
                        i++;
                    }
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    model.SuggestList.Clear();
                    foreach (var r in resultSuggest)
                    {
                        model.SuggestList.Add(r);
                        
                    }
                    model.MaxCountText = (140 - str.Length).ToString();
                    if (int.Parse(model.MaxCountText) >= 0)
                    {
                        model.MaxCountBrush = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        model.MaxCountBrush = new SolidColorBrush(Colors.Red);
                    }
                    this.TextTweet = str;
                    
                });
                
        }

        private void listViewSuggest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string item=e.AddedItems[0].ToString();
                
                if (item.StartsWith(mention.ToString()))
                {
                    var ss=textBoxTweet.Text;
                    int index=ss.LastIndexOf(mention);
                    string s=ss.Substring(0,index);
                    textBoxTweet.Text = s + item;
                }
                else if (item.StartsWith(hash.ToString()))
                {
                    var ss = textBoxTweet.Text;
                    int index = ss.LastIndexOf(hash);
                    string s = ss.Substring(0, index);
                    textBoxTweet.Text = s + item;
                }
                
            }
        }
    }

    public class SuggestModel:INotifyPropertyChanged
    {
        public SuggestModel()
        {
            suggestList = new ObservableCollection<string>();
            mentionSuggestSourceList = new ObservableCollection<string>();
            hashSuggestSourceList = new ObservableCollection<string>();
            isMaxCountVisibly = Visibility.Visible;
            maxCountText = "140";
            MaxCountBrush = new SolidColorBrush(Colors.Black);
        }
        string text;

        public string Text
        {
            get { return text; }
            set { text = value; ModelPropertyChanged("Text"); }
        }

        string maxCountText;

        public string MaxCountText
        {
            get { return maxCountText; }
            set { maxCountText = value; ModelPropertyChanged("MaxCountText"); }
        }

        Visibility isMaxCountVisibly;

        public Visibility IsMaxCountVisibly
        {
            get { return isMaxCountVisibly; }
            set { isMaxCountVisibly = value; ModelPropertyChanged("IsMaxCountVisibly"); }
        }

        SolidColorBrush maxCountBrush;

        public SolidColorBrush MaxCountBrush
        {
            get { return maxCountBrush; }
            set { maxCountBrush = value; ModelPropertyChanged("MaxCountBrush"); }
        }
        
        ObservableCollection<string> mentionSuggestSourceList;

        public ObservableCollection<string> MentionSuggestSourceList
        {
            get { return mentionSuggestSourceList; }
            set { mentionSuggestSourceList = value; }
        }

        ObservableCollection<string> hashSuggestSourceList;

        public ObservableCollection<string> HashSuggestSourceList
        {
            get { return hashSuggestSourceList; }
            set { hashSuggestSourceList = value; }
        }

        ObservableCollection<string> suggestList;

        public ObservableCollection<string> SuggestList
        {
            get { return suggestList; }
            set { suggestList = value; }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
