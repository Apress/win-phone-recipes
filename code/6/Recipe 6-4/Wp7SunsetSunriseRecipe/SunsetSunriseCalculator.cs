using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wp7SunsetSunriseRecipe
{
    public class SunsetSunriseCalculator
    {
        //private int calculateDay()
        //{
        //var N1 = Math.Floor(275 * DateTime.Now.Month / 9);
        //var N2 = Math.Floor((DateTime.Now.Month + 9) / 12);
        //var N3 = (1 + Math.Floor((DateTime.Now.Year - 4 * Math.Floor(DateTime.Now.Year / 4) + 2) / 3));
        //var N = N1 - (N2 * N3) + DateTime.Now.Day - 30;
        //}

        public DateTime GetSunset(double latitude, double longitude)
        {
            DateTime sunset = new DateTime();
            var zenith = 0;
            var longHour = longitude / 15;
            var t = DateTime.Now.DayOfYear + ((18 - longHour) / 24);
            var M = (0.9856 * t) - 3.289;
            var L = M + (1.916 * Math.Sin(M)) + (0.020 * Math.Sin(2 * M)) + 282.634;
            var RA = Math.Atan(0.91764 * Math.Tan(L));

            var Lquadrant = (Math.Floor(L / 90)) * 90;
            var RAquadrant = (Math.Floor(RA / 90)) * 90;
            RA = RA + (Lquadrant - RAquadrant);

            RA = RA / 15;

            var sinDec = 0.39782 * Math.Sin(L);
            var cosDec = Math.Cos(Math.Asin(sinDec));

            var cosH = (Math.Cos(zenith) - (sinDec * Math.Sin(latitude))) / (cosDec * Math.Cos(latitude));
             //if (cosH >  1) 
            //  the sun never rises on this location (on the specified date)
            //if (cosH < -1)
            //  the sun never sets on this location (on the specified date)

            var H = Math.Acos(cosH);
            H = H / 15;
            var T = H + RA - (0.06571 * t) - 6.622;
            var UT = T - longHour;
            var localT = UT + (DateTime.Now.ToUniversalTime() - DateTime.Now).Hours;
            return sunset;
        }

        public DateTime GetSunrise(double latitude, double longitude)
        {
            DateTime sunrise = new DateTime();
            var zenith = 0;
            var longHour = longitude / 15;
            var t = DateTime.Now.DayOfYear + ((6 - longHour) / 24);
            var M = (0.9856 * t) - 3.289;
            var L = M + (1.916 * Math.Sin(M)) + (0.020 * Math.Sin(2 * M)) + 282.634;
            var RA = Math.Atan(0.91764 * Math.Tan(L));

            var Lquadrant = (Math.Floor(L / 90)) * 90;
            var RAquadrant = (Math.Floor(RA / 90)) * 90;
            RA = RA + (Lquadrant - RAquadrant);

            RA = RA / 15;

            var sinDec = 0.39782 * Math.Sin(L);
            var cosDec = Math.Cos(Math.Asin(sinDec));
            var cosH = (Math.Cos(zenith) - (sinDec * Math.Sin(latitude))) / (cosDec * Math.Cos(latitude));
            var H = 360 - Math.Acos(cosH);
            H = H / 15;
            var T = H + RA - (0.06571 * t) - 6.622;
            var UT = T - longHour;
            var localT = UT + (DateTime.Now.ToUniversalTime() - DateTime.Now).Hours;
            return sunrise;
        }
    }
}
