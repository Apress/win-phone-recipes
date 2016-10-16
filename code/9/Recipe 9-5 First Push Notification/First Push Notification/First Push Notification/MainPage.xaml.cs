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
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;

namespace First_Push_Notification
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void CreateTileButton_Click(object sender, RoutedEventArgs e)
        {
            ShellTile.Create(
                new Uri("/MainPage.xaml", UriKind.Relative),
                new StandardTileData
                    {
                        BackgroundImage = new Uri("\\ApplicationIcon.png", UriKind.Relative),
                        Title = "New York"
                    });
        }
    }
}