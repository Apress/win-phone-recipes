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
using Microsoft.Silverlight.Testing;

namespace Wp7Recipe_10_2_MVVM
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: Uncomment this code if you want to run unit test
            //SystemTray.IsVisible = false;
            //var testExecutionPage = UnitTestSystem.CreateTestPage() as IMobileTestPage;
            //BackKeyPress += (k, ek) => ek.Cancel = testExecutionPage.NavigateBack();
            //(Application.Current.RootVisual as PhoneApplicationFrame).Content = testExecutionPage;
        }
    }
}