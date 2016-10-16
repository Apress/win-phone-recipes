using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using _08_07BackgroundTransfer.Model;
using Microsoft.Phone.BackgroundTransfer;

namespace _08_07BackgroundTransfer.Repository
{
    public static class ImagesRepository
    {
        private static ObservableCollection<ImageResource> _images;
        public static ObservableCollection<ImageResource> Images
        {
            get { return _images ?? InitImages(); }
        }

        private static ObservableCollection<ImageResource> InitImages()
        {
            _images = new ObservableCollection<ImageResource>
                          {
                              new ImageResource
                                  {
                                      ImageUri = new Uri("http://www.plasmator.net/wallpaper/2005-06-26.jpg",UriKind.Absolute),
                                      Title = "Fractal 1"
                                  },
                                  new ImageResource
                                  {
                                      ImageUri = new Uri("http://t2.gstatic.com/images?q=tbn:ANd9GcRJC4aogaU6WhHqwp0_so0ia6IHiaG1EFLsxKQTk1H2c2TG6Ccp",UriKind.Absolute),
                                      Title = "Mango"
                                  },
                                  new ImageResource
                                  {
                                      ImageUri = new Uri("http://t0.gstatic.com/images?q=tbn:ANd9GcQrLsp_LHEH0eytMrBsa1_WLbwzF_XfWbNVjdwFljkv6_i0By_rjw",UriKind.Absolute),
                                      Title = "Phone"
                                  },
                                  new ImageResource
                                  {
                                      ImageUri = new Uri("http://www.plasmator.net/wallpaper/starBurst.jpg",UriKind.Absolute),
                                      Title = "Fractal 2"
                                  },
                                  new ImageResource
                                  {
                                      ImageUri = new Uri("http://s.camptocamp.org/uploads/images/1265716960_373714172.jpg",UriKind.Absolute),
                                      Title = "Mountain 1" 
                                  },
                                  new ImageResource
                                  {
                                      ImageUri = new Uri("http://s.camptocamp.org/uploads/images/1290511517_554929250.jpg",UriKind.Absolute),
                                      Title = "Mountain 2"
                                  }
                          };
            return _images;
        }
    }
}
