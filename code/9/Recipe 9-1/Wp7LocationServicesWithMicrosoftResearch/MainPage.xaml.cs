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
using System.Device.Location;
using Wp7LocationServicesWithMicrosoftResearch.TerraService;
using System.Windows.Media.Imaging;
using System.IO;


namespace Wp7LocationServicesWithMicrosoftResearch
{
    public partial class MainPage : PhoneApplicationPage
    {
        TerraService.TerraServiceSoapClient proxy = null;
        GeoCoordinateWatcher geoWatcher = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InizializeServices();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }
        
        private void InizializeServices()
        {
            proxy = new TerraService.TerraServiceSoapClient();
            geoWatcher = new GeoCoordinateWatcher();
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                proxy.GetAreaFromPtCompleted += new EventHandler<TerraService.GetAreaFromPtCompletedEventArgs>(proxy_GetAreaFromPtCompleted);
                proxy.GetTileCompleted += new EventHandler<TerraService.GetTileCompletedEventArgs>(proxy_GetTileCompleted);

                geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
                geoWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(geoWatcher_StatusChanged);
                geoWatcher.Start();
            }
            else
                MessageBox.Show("No Network available");
        }

        void geoWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            this.LocationStatusTextBox.Text = e.Status.ToString();
        }

        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!e.Position.Location.IsUnknown)
            {

                proxy.GetAreaFromPtAsync(
                    //the ActualPosition
                    new TerraService.LonLatPt()
                    {
                        Lat = e.Position.Location.Latitude,
                        Lon = e.Position.Location.Longitude
                    },
                    1,
                    TerraService.Scale.Scale2km,
                    (int)ContentPanel.ActualWidth,
                    (int)ContentPanel.ActualHeight);
            }

        }

        void proxy_GetTileCompleted(object sender, TerraService.GetTileCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Image img = e.UserState as Image;
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(new MemoryStream(e.Result));
                img.Source = bitmap;
            }
        }

        void proxy_GetAreaFromPtCompleted(object sender, TerraService.GetAreaFromPtCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                int startX = e.Result.NorthWest.TileMeta.Id.X;
                int endX = e.Result.NorthEast.TileMeta.Id.X;
                int startY = e.Result.NorthWest.TileMeta.Id.Y;
                int endY = e.Result.SouthWest.TileMeta.Id.Y;

                for (int x = startX; x < endX; x++)
                    for (int y = startY; y >= endY; y--)
                    {
                        Image image = new Image();
                        image.Stretch = Stretch.None;
                        image.Margin = new Thickness((x - startX) * 200 - e.Result.NorthWest.Offset.XOffset, (startY - y) * 200 - e.Result.NorthWest.Offset.YOffset, 0, 0);
                        ContentPanel.Children.Insert(0, image);

                        TileId tileId = e.Result.NorthWest.TileMeta.Id;
                        tileId.X = x;
                        tileId.Y = y;

                        proxy.GetTileAsync(tileId, image);
                    }
            }
            else
                MessageBox.Show(e.Error.Message);
        }


    }
}