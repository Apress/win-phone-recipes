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

namespace DetectingThemeChanging
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            btnPlay = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            btnStop = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (ThemeDetector.IsDarkTheme)
            {
                btnPlay.IconUri = new Uri("/Images/dark/appbar.transport.play.rest.png", UriKind.Relative);
                btnStop.IconUri = new Uri("/Images/dark/appbar.stop.rest.png", UriKind.Relative);
                PageTitle.Text = "Dark Theme";
            }
            else
            {
                btnPlay.IconUri = new Uri("/Images/light/appbar.transport.play.rest.png", UriKind.Relative);
                btnStop.IconUri = new Uri("/Images/light/appbar.stop.rest.png", UriKind.Relative);
                PageTitle.Text = "Light Theme";
            }
        }
    }
}