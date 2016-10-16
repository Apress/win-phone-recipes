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
using _08_07BackgroundTransfer.Model;
using _08_07BackgroundTransfer.Repository;
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Controls;

namespace _08_07BackgroundTransfer
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            ImagesListBox.ItemsSource = ImagesRepository.Images;
        }

        private void DownloadImage_Tap(object sender, GestureEventArgs e)
        {
            //DownloadController.Instance.AddDownload(ImagesListBox.SelectedItem as ImageResource);
            if (ImagesListBox.SelectedItem is ImageResource)
            {
                var resource = (ImagesListBox.SelectedItem as ImageResource);
                var request = new BackgroundTransferRequest(resource.ImageUri)
                {
                    DownloadLocation = new Uri(string.Format("/shared/transfers/{0}.{1}", resource.Title, resource.ImageUri.OriginalString.Substring(resource.ImageUri.OriginalString.Length - 3, 3)), UriKind.Relative),
                    TransferPreferences = TransferPreferences.AllowBattery,
                };
                NavigationService.Navigate(new Uri("/TransferStatus.xaml", UriKind.Relative));
                BackgroundTransferService.Add(request);
            }
        }
    }
}