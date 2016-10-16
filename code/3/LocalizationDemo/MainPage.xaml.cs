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
using System.Threading;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace LocalizationDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            imgBanner.Source = new BitmapImage(new Uri(string.Format("{0}/amazon.png", ci.Name), UriKind.Relative));
        }
    }
}