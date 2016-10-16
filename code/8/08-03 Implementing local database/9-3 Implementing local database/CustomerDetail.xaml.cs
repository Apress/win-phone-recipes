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
using CustomersManager.Model;
using Microsoft.Phone.Controls;

namespace CustomersManager
{
    public partial class CustomerDetail : PhoneApplicationPage
    {
        private Customer _customer;
        private CustomersManagerDataContext dc = new CustomersManagerDataContext();
        public CustomerDetail()
        {
            InitializeComponent();
            Loaded += CustomerDetailLoaded;
        }

        private void CustomerDetailLoaded(object sender, RoutedEventArgs e)
        {
            if (!NavigationContext.QueryString.ContainsKey("Id"))
            {
                _customer = new Customer();
            }
            else
            {
                Guid guid = Guid.Parse(NavigationContext.QueryString["Id"]);
                _customer = dc.Customers.FirstOrDefault(c => c == new Customer { Id = guid });
            }
            ContentPanel.DataContext = _customer;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!dc.Customers.Contains(_customer)) 
                _customer = null;
            else if (MessageBox.Show("Are you sure?", "Confim", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                dc.Customers.DeleteOnSubmit(_customer);
                dc.SubmitChanges();
            }
            NavigationService.Navigate(new Uri("/Customers.xaml", UriKind.Relative));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!dc.Customers.Contains(_customer))
                dc.Customers.InsertOnSubmit(_customer);
            dc.SubmitChanges();
            NavigationService.Navigate(new Uri("/Customers.xaml", UriKind.Relative));
        }
    }
}