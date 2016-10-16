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
using Microsoft.Phone.Shell;

namespace AdvertisingApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        private void adControl1_Tap(object sender, GestureEventArgs e)
        {
            if (!PhoneApplicationService.Current.State.ContainsKey("ads"))
                PhoneApplicationService.Current.State.Add("ads", "");
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("ads"))
            {
                NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
            }
        }


    }
}