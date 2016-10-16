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
using System.Windows.Media.Imaging;

namespace TvSchedule7
{
    public partial class Details : PhoneApplicationPage
    {
        public Details()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("programme"))
            {
                ItemViewModel programme = PhoneApplicationService.Current.State["programme"] as ItemViewModel;
                tbDesc.Text = programme.Description;
                tbTitle.Text = programme.Title;
                tbStartEnd.Text = string.Format("Period: {0}", programme.StartEnd);

                string channel = string.Empty;
                if (NavigationContext.QueryString.ContainsKey("Channel"))
                    channel = NavigationContext.QueryString["Channel"];

                switch (channel)
                {
                    case "BBC1":
                        imgLogo.Source = new BitmapImage(new Uri("images/BBC1.png", UriKind.Relative));
                        break;
                    case "BBC2":
                        imgLogo.Source = new BitmapImage(new Uri("images/BBC2.png", UriKind.Relative));
                        break;
                    case "ITV1":
                        imgLogo.Source = new BitmapImage(new Uri("images/ITV1.png", UriKind.Relative));
                        break;

                }
            }
        }
    }
}