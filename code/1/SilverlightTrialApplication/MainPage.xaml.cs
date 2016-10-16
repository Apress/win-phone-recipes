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

using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Phone.Marketplace;
using Microsoft.Xna.Framework;


namespace SilverlightTrialApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

#if DEBUG
            Guide.SimulateTrialMode = true;
#endif        
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Guide.IsTrialMode)
            {
                lbMessage.Text = "The Application is in Trial Mode!";
            }
            else
                btnBuy.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Guide.ShowMarketplace(PlayerIndex.One);
        }
    }
}