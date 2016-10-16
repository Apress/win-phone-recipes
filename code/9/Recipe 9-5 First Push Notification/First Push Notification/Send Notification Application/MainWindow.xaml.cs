using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Send_Notification_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendTileNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            string subscriptionUri = this.NotificationUriTextBox.Text;
            HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(subscriptionUri);

            sendNotificationRequest.Method = "POST";

            sendNotificationRequest.Headers.Add("X-MessageID", Guid.NewGuid().ToString());
            sendNotificationRequest.ContentType = "text/xml";
            sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "token");
            sendNotificationRequest.Headers.Add("X-NotificationClass", "1");
            
            string tileTag = string.IsNullOrWhiteSpace(TileIdTextBox.Text)
                                 ? "<wp:Tile>"
                                 : "<wp:Tile Id=\""+ TileIdTextBox.Text +"\">";


            string tileMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                                      tileTag +
                                          "<wp:BackgroundImage>{0}</wp:BackgroundImage>" +
                                          "<wp:Count>{1}</wp:Count>" +
                                          "<wp:Title>{2}</wp:Title>" +
                                          "<wp:BackBackgroundImage>{3}</wp:BackBackgroundImage>" +
                                          "<wp:BackContent>{4}</wp:BackContent>" +
                                          "<wp:BackTitle>{5}</wp:BackTitle>" +
                                       "</wp:Tile> " +
                                    "</wp:Notification>";

            byte[] notificationMessage = new System.Text.UTF8Encoding().GetBytes(
                     string.Format(tileMessage,
                     BackgroundTextBox.Text,
                     CountTextBox.Text,
                     TitleTextBox.Text,
                     BackBackgroundTextBox.Text,
                     BackContentTextBox.Text,
                     BackTitleTextBox.Text));

            // Sets the web request content length.
            sendNotificationRequest.ContentLength = notificationMessage.Length;

            using (Stream requestStream = sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            HttpWebResponse response = (HttpWebResponse)
                            sendNotificationRequest.GetResponse();

            StringBuilder sb = new StringBuilder();
            foreach (var item in response.Headers)
            {
                sb.AppendLine(string.Format("{0}-->{1}",
                         item.ToString(),
                         response.Headers[item.ToString()]));
            }

            ResponseTextBox.Text = sb.ToString();

        }

        private void SendToastNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            string subscriptionUri = this.NotificationUriTextBox.Text;
            HttpWebRequest sendNotificationRequest = (HttpWebRequest)
                                  WebRequest.Create(subscriptionUri);

            sendNotificationRequest.Method = "POST";
            sendNotificationRequest.Headers.Add("X-MessageID", Guid.NewGuid().ToString());
            sendNotificationRequest.ContentType = "text/xml";
            sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
            sendNotificationRequest.Headers.Add("X-NotificationClass", "2");


            string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                      "<wp:Notification xmlns:wp=\"WPNotification\">" +
                                            "<wp:Toast>" +
                                              "<wp:Text1>{0}</wp:Text1>" +
                                              "<wp:Text2>{1}</wp:Text2>" +
                                           "</wp:Toast>" +
                                        "</wp:Notification>";

            byte[] notificationMessage = new System.Text.UTF8Encoding().GetBytes(
                          string.Format(toastMessage,
                                        FirstRowTextBox.Text,
                                        SecondRowTextBox.Text));

            sendNotificationRequest.ContentLength = notificationMessage.Length;

            using (Stream requestStream = sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            HttpWebResponse response = (HttpWebResponse)
                                  sendNotificationRequest.GetResponse();

            StringBuilder sb = new StringBuilder();
            foreach (var item in response.Headers)
            {
                sb.AppendLine(string.Format("{0}-->{1}",
                              item.ToString(),
                              response.Headers[item.ToString()]));
            }

            ResponseTextBox.Text = sb.ToString();
        }


    }
}
