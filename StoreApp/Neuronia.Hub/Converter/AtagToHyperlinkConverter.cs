using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Neuronia.Hub.Converter
{
    public class AtagToHyperlinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Style linkStyle=Application.Current.Resources["NeuroniaTimelineHyperlinkButtonStyle"] as Style;
            if (value == null)
            {
                return null;
            }
            else if (value.ToString() == "web")
            {
                return new HyperlinkButton()
                {
                    NavigateUri=new Uri("http://www.twitter.com"),
                    Content=value,
                    Style = linkStyle
                };
            }
            else
            {
                string str = value.ToString();
                System.Text.RegularExpressions.MatchCollection mc =
        System.Text.RegularExpressions.Regex.Matches(
            str,
            @"<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" +
                @"(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
            | System.Text.RegularExpressions.RegexOptions.Singleline);

                string url = mc[0].Groups["url"].Value.ToString();
                string text = mc[0].Groups["text"].Value.ToString();

                return new HyperlinkButton()
                {
                    NavigateUri = new Uri(url),
                    Content = text,
                    Style=linkStyle
                };
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
