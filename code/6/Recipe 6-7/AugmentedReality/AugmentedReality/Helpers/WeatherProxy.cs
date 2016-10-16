using System;
using System.Device.Location;
using AugmentedReality.Enumerations;

namespace AugmentedReality.Helpers
{
    public class WeatherProxy
    {
        public event GetWeatherCompletedEventHandler GetWeatherCompleted;

        public void GetWeatherAsync(GeoCoordinate coordinate)
        {
            if (GetWeatherCompleted != null)
            {
                if (DateTime.Now.Minute % 2 == 0)
                    GetWeatherCompleted(this, new GetWeatherCompletedEventArgs(Weather.Sun));
                else
                    GetWeatherCompleted(this, new GetWeatherCompletedEventArgs(Weather.Lightning));

            }
        }

        public delegate void GetWeatherCompletedEventHandler(object sender, GetWeatherCompletedEventArgs e);

        public class GetWeatherCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
        {
            public GetWeatherCompletedEventArgs(Weather weather)
            {
                Weather = weather;
            }
            public Weather Weather { get; private set; }
        }
    }
}