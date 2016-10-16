using System;
using System.IO;
using System.Net;
using System.Windows.Media.Animation;
using AugmentedReality.Enumerations;
using AugmentedReality.Helpers;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;
using Microsoft.Devices;

namespace AugmentedReality
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Motion _motionHandler;
        private PhotoCamera _photoCamera;
        private GeoCoordinateWatcher _geoWatcher;
        private WeatherProxy _weatherProxy;
        private TimeSpan _minimumRequestTime;
        private DateTime _lastRequestTime;
        private Storyboard _executingStoryboard;
        ////Cos'è??
        //Viewport viewport;

        //Matrix projection;
        //Matrix view;
        //Matrix attitude;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_photoCamera != null) _photoCamera.Dispose();
            if (_geoWatcher != null) _geoWatcher.Stop();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                // Initialize the camera and set the video brush source.
                if (!Motion.IsSupported)
                {
                    MessageBox.Show("the Motion API is not supported on this device.");
                    return;
                }

                _photoCamera = new PhotoCamera();
                realityVideoBrush.SetSource(_photoCamera);

                _minimumRequestTime = TimeSpan.FromSeconds(8);

                _geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                _geoWatcher.Start();
                _weatherProxy = new WeatherProxy();
                _weatherProxy.GetWeatherCompleted += GetForecastCompleted;

                if (_motionHandler == null)
                {
                    _motionHandler = new Motion
                                         {
                                             TimeBetweenUpdates = TimeSpan.FromMilliseconds(20)
                                         };
                    _motionHandler.CurrentValueChanged += MotionHandlerCurrentValueChanged;
                }

                // Try to start the Motion API.
                _motionHandler.Start();
            }
            catch (SensorFailedException)
            {
                MessageBox.Show("unable to start the Motion API.");                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void MotionHandlerCurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Dispatcher.BeginInvoke(() =>
                                       {
                                           if (e.SensorReading.Gravity.Z > 0.7)
                                           {
                                               if (DateTime.Now.Subtract(_lastRequestTime) > _minimumRequestTime)
                                               {
                                                   _lastRequestTime = DateTime.Now;
                                                   GetForecast(_geoWatcher.Position);
                                               }
                                           }
                                           else
                                           {
                                               if (_executingStoryboard != null) _executingStoryboard.Stop();

                                           }
                                           //else if (-0.1 < e.SensorReading.Gravity.Z && e.SensorReading.Gravity.Z < 0.1)
                                           //{
                                           //    InformationTextBlock.Text = "Looking for traffic...";
                                           //    //Contact a webservice to retrieve traffic informations
                                           //}
                                           //else if (e.SensorReading.Gravity.Z < -0.6)
                                           //{
                                           //    InformationTextBlock.Text = "Looking for Latest Hearthquake news...";                                               
                                           //    //retrieve information about Hearthquake
                                           //}
                                       });


            // This event arrives on a background thread. Use BeginInvoke
            // to call a method on the UI thread.
        }

        private void GetForecast(GeoPosition<GeoCoordinate> position)
        {
                InformationTextBlock.Text = "Looking for forecast...";
                _weatherProxy.GetWeatherAsync(position.Location);
        }

        private void GetForecastCompleted(object sender, WeatherProxy.GetWeatherCompletedEventArgs e)
        {
            Brush brush = null;
            switch (e.Weather)
            {
                case Weather.Sun:
                     brush = App.Current.Resources.MergedDictionaries.FirstOrDefault()["SunBrush"] as ImageBrush;
                    _executingStoryboard = SunStoryboard;
                    break;
                case Weather.Fog:
                    brush = App.Current.Resources.MergedDictionaries.FirstOrDefault()["FogBrush"] as LinearGradientBrush;
                    _executingStoryboard = FogStoryboard;
                    break;
                case Weather.Lightning:
                     brush = App.Current.Resources.MergedDictionaries.FirstOrDefault()["LightningBrush"] as ImageBrush;
                    _executingStoryboard = LightningStoryboard;
                    break;
                case Weather.Moon:
                    break;
                case Weather.Clouds:
                    break;
            }
            WeatherRectangle.Fill = brush;
            _executingStoryboard.Begin();
        }
    }
}