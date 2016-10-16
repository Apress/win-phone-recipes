using System;
using System.Collections.Generic;
using System.Device.Location;
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

namespace CivicAddressResolverRecipe
{
    public partial class MainPage : PhoneApplicationPage
    {
        //private GeoCoordinateWatcher geoWatcher;
        //private CivicAddressResolver civicResolver;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //geoWatcher = new GeoCoordinateWatcher();
            //geoWatcher.PositionChanged += PositionChanged;
            //civicResolver = new CivicAddressResolver();
            //civicResolver.ResolveAddressCompleted += CivicAddressResolved;
            //geoWatcher.Start();
        }

        private void CivicAddressResolved(object sender, ResolveAddressCompletedEventArgs e)
        {
            ContentPanel.DataContext = e.Address;
        }

        private void PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //civicResolver.ResolveAddressAsync(e.Position.Location);
        }
    }
}