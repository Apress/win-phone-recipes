using System;
using System.Collections.ObjectModel;
using System.Linq;
using _08_07BackgroundTransfer.Model;
using Microsoft.Phone.BackgroundTransfer;

namespace _08_07BackgroundTransfer
{
    public class DownloadController
    {
        private static DownloadController _instance;
        public static DownloadController Instance
        {
            get { return _instance ?? (_instance = new DownloadController()); }
        }

        private DownloadController() { }

        public ObservableCollection<BackgroundTransferRequest> TransferRequests { get; set; }

        public void AddDownload(ImageResource resource)
        {
            var request = new BackgroundTransferRequest(resource.ImageUri)
                              {
                                  DownloadLocation = new Uri(string.Format("/shared/transfers/{0}.{1}", resource.Title, resource.ImageUri.OriginalString.Substring(resource.ImageUri.OriginalString.Length - 3, 3)), UriKind.Relative),
                                  TransferPreferences = TransferPreferences.AllowBattery,
                              };
            BackgroundTransferService.Add(request);
        }
    }
}