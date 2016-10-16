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

namespace BirthdayApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Clear text
            tbFriends.Text = string.Empty;

            // Dummty text
            tbFriends.Text = "TODAY\n";
            tbFriends.Text += "John Smith\n\n\n";
            tbFriends.Text += "TOMORROW\n";
            tbFriends.Text += "No birthdays";
            base.OnNavigatedTo(e);
        }
    }
}