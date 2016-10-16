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

namespace Wp7SunsetSunriseRecipe
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher geo = new GeoCoordinateWatcher();
        SunsetSunriseCalculator ssc = new SunsetSunriseCalculator();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            geo.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geo_PositionChanged);
            geo.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(geo_StatusChanged);
            geo.Start();
            
            

        }

        void geo_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            this.textBlock2.Text = e.Status.ToString();
        }

        void geo_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!e.Position.Location.IsUnknown)
            {
                DateTime sunrise = new DateTime();
                DateTime sunset = new DateTime();
                bool isSunrise = false;
                bool isSunset = false;

                SunTimes.Instance.CalculateSunRiseSetTimes(e.Position.Location.Latitude, e.Position.Location.Longitude, DateTime.Now, ref sunrise, ref sunset, ref isSunrise, ref isSunset);
                textBlock1.Text = sunrise.ToString("HH:mm");
            }
        }
    }
}