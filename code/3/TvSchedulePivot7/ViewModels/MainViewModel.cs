using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net;
using System.Xml.Linq;


namespace TvSchedulePivot7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.BBC1Items = new ObservableCollection<ItemViewModel>();
            this.BBC2Items = new ObservableCollection<ItemViewModel>();
            this.ITV1Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> BBC1Items { get; private set; }
        public ObservableCollection<ItemViewModel> BBC2Items { get; private set; }
        public ObservableCollection<ItemViewModel> ITV1Items { get; private set; }

        private WebClient web = null;

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            web = new WebClient();

            web.DownloadStringAsync(new Uri("http://www.bleb.org/tv/data/listings/0/bbc1.xml"), "BBC1");
            web.DownloadStringCompleted += new DownloadStringCompletedEventHandler(web_DownloadStringCompleted);
        }

        void web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null && e.Error.Message != string.Empty)
                return;

            if (e.Cancelled)
                return;
            
            string result = e.Result;
            XElement xml = XElement.Parse(result);

            if (xml != null)
            {
                foreach (XElement programme in xml.Descendants("programme"))
                {
                    ItemViewModel item = new ItemViewModel();
                    item.Description = programme.Element("desc").Value;
                    item.Title = programme.Element("title").Value;
                    item.StartEnd = programme.Element("start").Value + " - " + programme.Element("end").Value;

                    switch ((string)e.UserState)
                    {
                        case "BBC1":
                            this.BBC1Items.Add(item);
                            break;
                        case "BBC2":
                            this.BBC2Items.Add(item);
                            break;
                        case "ITV1":
                            this.ITV1Items.Add(item);
                            break;
                    }
                }
            }

            switch ((string)e.UserState)
            {
                case "BBC1":
                    web.DownloadStringAsync(new Uri("http://www.bleb.org/tv/data/listings/0/bbc2.xml"), "BBC2");
                    break;
                case "BBC2":
                    web.DownloadStringAsync(new Uri("http://www.bleb.org/tv/data/listings/0/itv1.xml"), "ITV1");
                    break;
            }

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}