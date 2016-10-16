using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Runtime.Serialization;
using Wp7Recipe0902_Atom_Reader.EntitiesAtom;
using System.Xml.Serialization;
using Microsoft.Phone.Tasks;

namespace Wp7Recipe0902_Atom_Reader
{
    public partial class MainPage : PhoneApplicationPage
    {
        WebBrowserTask webBrowserTask = null;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc.OpenReadAsync(new Uri("http://feeds.bbci.co.uk/news/world/rss.xml"));
            if (webBrowserTask == null) 
                webBrowserTask = new WebBrowserTask();
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            //string stream = new System.IO.StreamReader(e.Result).ReadToEnd();

            //DataContractSerializer dcs = new DataContractSerializer(typeof(Rss));
            //Rss obj = dcs.ReadObject(e.Result) as Rss;

            XmlSerializer xms = new XmlSerializer(typeof(Rss));
            Rss des = xms.Deserialize(e.Result) as Rss;
            NewsList.ItemsSource = des.Channel.Items;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hpb = sender as HyperlinkButton;
            webBrowserTask.URL = hpb.Content.ToString();
            webBrowserTask.Show();
        }
    }
}