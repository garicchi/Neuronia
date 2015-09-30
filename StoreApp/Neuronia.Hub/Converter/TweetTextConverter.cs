using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Neuronia.Core.Extentions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using Windows.UI;
using Neuronia.Core.Tweets.Entities;
using Neuronia.Core.Tweets;
using System.Diagnostics;
using Windows.UI.Xaml.Controls.Primitives;

namespace Neuronia.Hub.Converter
{
    public class TweetTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string str)
        {
            Binding detailCommandBinding = new Binding { Path = new PropertyPath("TweetDetailCommand") };

            Binding userDetailCommandBinding = new Binding { Path = new PropertyPath("UserDetailCommand") };

            Binding searchCommandBinding = new Binding() { Path = new PropertyPath("SearchCommand") };

            Binding browseCommandBinding = new Binding() { Path = new PropertyPath("BrowseCommand") };

            Binding previewImageCommandBinding = new Binding() { Path = new PropertyPath("ImagePreviewCommand") };


            SolidColorBrush runForeGround = Application.Current.Resources["ForegroundBrush"] as SolidColorBrush;
            FontFamily tweetFontFamily = Application.Current.Resources["MainFontFamily"] as FontFamily;
            double fontSize = (double)Application.Current.Resources["TimelineFontSize"];
            if ((value is Tweet))
            {

                Tweet tweet = value as Tweet;

                if (tweet.retweeted_status != null)
                {
                    tweet = tweet.retweeted_status;
                }

                RichTextBlock block = new RichTextBlock();
                Paragraph para = new Paragraph();
                
                List<EntitieBase> entities = new List<EntitieBase>();
                if (tweet.entities != null)
                {

                    if (tweet.entities.user_mentions != null)
                    {
                        entities.AddRange(tweet.entities.user_mentions);
                    }
                    if (tweet.entities.urls != null)
                    {
                        entities.AddRange(tweet.entities.urls);
                    }
                    if (tweet.entities.hashtags != null)
                    {
                        entities.AddRange(tweet.entities.hashtags);
                    }
                    if (tweet.entities.media != null)
                    {
                        entities.AddRange(tweet.entities.media);
                    }
                }
                

                try{
                    if (tweet.entities != null && entities.Count > 0)
                    {

                        entities.OrderBy(q => q.indices[0]);
                        string back = "";
                        int seek = 0;


                        foreach (EntitieBase entitiy in entities)
                        {

                            int start = entitiy.indices[0];
                            int end = entitiy.indices[1];
                            StringInfo infoText = new StringInfo(tweet.text);
                            back = tweet.text.SubStringByTextElements(end, infoText.LengthInTextElements - end);
                            string front = tweet.text.SubStringByTextElements(seek, start - seek);

                            para.Inlines.Add(new Run { Text = front, Foreground = runForeGround, FontSize = fontSize });
                            var link = new HyperlinkButton();
                            link.Padding = new Thickness(0);
                            // link.Foreground = Application.Current.Resources["AppThemeBrush"] as SolidColorBrush;
                            link.Style = Application.Current.Resources["NeuroniaTimelineHyperlinkButtonStyle"] as Style;
                            link.FontSize = fontSize;

                            var uiContainer = new InlineUIContainer();

                            if (entitiy is UserMention)
                            {
                                var en = entitiy as UserMention;

                                link.Content = "@" + en.screen_name;
                                uiContainer.Child = link;
                                link.CommandParameter = en.screen_name;
                                link.SetBinding(HyperlinkButton.CommandProperty, userDetailCommandBinding);
                            }
                            else if (entitiy is TweetUrl)
                            {
                                var en = entitiy as TweetUrl;
                                link.Content = en.display_url;
                                uiContainer.Child = link;
                                link.CommandParameter = en.url;
                                link.SetBinding(HyperlinkButton.CommandProperty, browseCommandBinding);

                            }
                            else if (entitiy is HashTag)
                            {
                                var en = entitiy as HashTag;
                                link.Content = "#" + en.text;
                                uiContainer.Child = link;
                                link.CommandParameter = "#" + en.text;
                                link.SetBinding(HyperlinkButton.CommandProperty, searchCommandBinding);
                            }
                            else if (entitiy is TweetMedia)
                            {
                                var en = entitiy as TweetMedia;

                                Button btn = new Button();
                                btn.Width = (double)Application.Current.Resources["previewImageWidth"];
                                
                                //btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                                btn.Height = (double)Application.Current.Resources["previewImageHeight"];
                                btn.Style = Application.Current.Resources["NeuroniaTimelineImageButtonStyle"] as Style;
                                var imageBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri(en.media_url)), Stretch = Stretch.UniformToFill };
                                btn.Background = imageBrush;
                                btn.CommandParameter = en;
                                btn.SetBinding(Button.CommandProperty,previewImageCommandBinding);
                                var flyout= Application.Current.Resources["ImagePreviewFlyout"] as Flyout;
                                flyout.Placement = FlyoutPlacementMode.Top;
                                
                                var grid = (flyout.Content as Grid);
                               
                                btn.Flyout = flyout;
                                uiContainer.Child = btn;
                                
                                para.Inlines.Add(new LineBreak());
                            }
                            para.Inlines.Add(uiContainer);
                            seek = end;
                        }

                        para.Inlines.Add(new Run { Text = back, Foreground = runForeGround, FontFamily = tweetFontFamily, FontSize = fontSize });


                    }
                    else
                    {
                        para.Inlines.Add(new Run { Text = tweet.text, Foreground = runForeGround, FontFamily = tweetFontFamily, FontSize = fontSize });
                    }

                    block.Blocks.Add(para);
                }
                catch (Exception e)
                {
                    //Debug.WriteLine(value.ToString());
                }
                 
                return block;
            }
            else
            {
                RichTextBlock block = new RichTextBlock();
                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run { Text = value.ToString(), Foreground = runForeGround, FontFamily = tweetFontFamily, FontSize = fontSize });

                block.Blocks.Add(para);
                return block;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string str)
        {
            return null;
        }
    }

}
