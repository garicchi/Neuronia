using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Neuronia.Utility
{
    public class ToastText
    {
        XmlDocument xmlDoc;
        public ToastText(string message, ToastTemplateType type)
        {
            xmlDoc = ToastNotificationManager.GetTemplateContent(type);
            var textTag = xmlDoc.GetElementsByTagName("text").First();
            textTag.AppendChild(xmlDoc.CreateTextNode(message));

        }

        public void Show()
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(new ToastNotification(xmlDoc));
        }
    }
}
