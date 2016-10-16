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

namespace TvSchedule7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
                PhoneApplicationService.Current.State["programme"] = e.AddedItems[0];
            switch (lb.Name)
            {
                case "lbBBC1":
                    this.NavigationService.Navigate(new Uri("/Details.xaml?Channel=BBC1", UriKind.Relative));
                    break;
                case "lbBBC2":
                    this.NavigationService.Navigate(new Uri("/Details.xaml?Channel=BBC2", UriKind.Relative));
                    break;
                case "lbITV1":
                    this.NavigationService.Navigate(new Uri("/Details.xaml?Channel=ITV1", UriKind.Relative));
                    break;
            }
        }
    }
}