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

namespace NavigatingWithGlobalVariableApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void chkRedColor_Checked(object sender, RoutedEventArgs e)
        {
            App a = Application.Current as App;
            a.RedPages = true;
            ContentPanel.Background = new SolidColorBrush(Colors.Red);
        }

        private void chkRedColor_Unchecked(object sender, RoutedEventArgs e)
        {
            App a = Application.Current as App;
            a.RedPages = false;
            ContentPanel.Background = new SolidColorBrush(Colors.Black);
        }

        private void hbToPage2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void hbToPage3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
        }
    }
}