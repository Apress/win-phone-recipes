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
using System.Globalization;

namespace Wp7LocationServiceRecipe
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher geoWatcher = null;
        ICivicAddressResolver civicResolver = null;
        Dictionary<string, string> cultures = new Dictionary<string, string>();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InitWatcher();
            InitResolver();
        }

        

        private void InitResolver()
        {
            civicResolver = new CivicAddressResolver();
            //civicResolver = new CustomAddressResolver();
            civicResolver.ResolveAddressCompleted += new EventHandler<ResolveAddressCompletedEventArgs>(civicResolver_ResolveAddressCompleted);
            civicResolver.ResolveAddressAsync(new GeoCoordinate(41, 14, 4));
        }

        void civicResolver_ResolveAddressCompleted(object sender, ResolveAddressCompletedEventArgs e)
        {
            RegionInfo myRI1 = RegionInfo.CurrentRegion;
            System.Diagnostics.Debug.WriteLine(myRI1.Name);
            string test = e.Address.CountryRegion;
            //ri.
            //CultureInfo culture = new CultureInfo();
            
            //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            
            //do something with resolved address

            //e.Address.AddressLine1
            //e.Address.AddressLine2
            //e.Address.Building
            //e.Address.City
            //e.Address.CountryRegion
            //e.Address.FloorLevel
            //e.Address.PostalCode
            //e.Address.StateProvince

        }

        private void InitWatcher()
        {
            geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
            geoWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(geoWatcher_StatusChanged);
            geoWatcher.Start();
        }

        void geoWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            this.ApplicationTitle.Text = e.Status.ToString();
        }

        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            map1.SetView(e.Position.Location, 14d);

            var civic = civicResolver.ResolveAddress(e.Position.Location);
        }
    }
}