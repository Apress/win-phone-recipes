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
    public static class SuntimesHelper
    {
        public static DateTime GetSunSet(double latitude, double longitude,DateTime forDay, out bool haveSunSet)
        {
            haveSunSet = false;
            return DateTime.Now;
        }

        public static DateTime GetSunRise(double latitude, double longitude, DateTime forDay, out bool haveSunRise)
        {
            haveSunRise = false;
            return DateTime.Now;
        }
    }
}
