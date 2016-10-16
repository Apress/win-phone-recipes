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
using CustomersManager;
using CustomersManager.Model;
using Microsoft.Phone.Controls;

namespace _9_3_Implementing_local_database
{
    public partial class Customers : PhoneApplicationPage
    {
        public Customers()
        {
            InitializeComponent();
            Loaded += Customers_Loaded;
        }

        void Customers_Loaded(object sender, RoutedEventArgs e)
        {
            using (CustomersManagerDataContext dc = new CustomersManagerDataContext())
            {
                CustomersListBox.ItemsSource = from c in dc.Customers
                                                select c;

            }
        }

        private void Edit_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(
                new Uri(string.Format("/CustomerDetail.xaml?Id={0}", (CustomersListBox.SelectedItem as Customer).Id),UriKind.Relative));
        }

        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CustomerDetail.xaml",UriKind.Relative) );
        }
    }
}