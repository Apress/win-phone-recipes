using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
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
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Controls;

namespace _08_07BackgroundTransfer
{
    public partial class TransferStatus : PhoneApplicationPage
    {
        private ObservableCollection<ImageResource> _downloadedImages;

        public TransferStatus()
        {
            InitializeComponent();
            Loaded += TransferStatusLoaded;
        }

        private void TransferStatusLoaded(object sender, RoutedEventArgs e)
        {
            TransferListBox.ItemsSource = BackgroundTransferService.Requests;
            foreach (var request in BackgroundTransferService.Requests)
            {
                request.TransferProgressChanged += TransferProgressChanged;
                request.TransferStatusChanged += TransferStatusChanged;
            }
            GetDownloadedFiles();
        }

        private void TransferStatusChanged(object sender, BackgroundTransferEventArgs e)
        {
            switch (e.Request.TransferStatus)
            {
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.None:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.Transferring:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.Waiting:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.WaitingForWiFi:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.WaitingForExternalPower:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.WaitingForExternalPowerDueToBatterySaverMode:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.WaitingForNonVoiceBlockingNetwork:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.Paused:
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.Completed:
                    if (e.Request.StatusCode == 200)
                    {
                        BackgroundTransferService.Remove(e.Request);
                        using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            string filename = e.Request.DownloadLocation.OriginalString.Split('\\').Last();
                            if (isoStore.FileExists(filename))
                            {
                                isoStore.DeleteFile(filename);
                            }
                            isoStore.MoveFile(e.Request.DownloadLocation.OriginalString, filename);
                            GetDownloadedFiles();
                        }
                    }
                    break;
                case Microsoft.Phone.BackgroundTransfer.TransferStatus.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            TransferListBox.ItemsSource = BackgroundTransferService.Requests;
        }

        private void TransferProgressChanged(object sender, BackgroundTransferEventArgs e)
        {
            TransferListBox.ItemsSource = BackgroundTransferService.Requests;
        }

        private void GetDownloadedFiles()
        {
            _downloadedImages = new ObservableCollection<ImageResource>();
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                foreach (var fileName in isoStore.GetFileNames())
                    _downloadedImages.Add(new ImageResource { Title = fileName });
            }
            DowloadedFilesListBox.ItemsSource = _downloadedImages;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var requestToCancel = BackgroundTransferService.Requests.FirstOrDefault(r => r.RequestId == (sender as Button).Tag.ToString());
            BackgroundTransferService.Remove(requestToCancel);
            TransferListBox.ItemsSource = BackgroundTransferService.Requests;
        }
    }
}