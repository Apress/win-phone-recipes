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

namespace AddGestureDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGesture_Tap(object sender, EventArgs e)
        {
            MessageBox.Show("You tapped the grid");
        }

        private void AddTap_Hold(object sender, EventArgs e)
        {
            MessageBox.Show("Hold gesture in the grid");
        }
    }
}