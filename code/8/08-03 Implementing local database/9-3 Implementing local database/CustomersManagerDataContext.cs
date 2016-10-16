using System;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CustomersManager.Model;

namespace CustomersManager
{
    public class CustomersManagerDataContext : DataContext
    {
        private const string DbConnectionString = "DataSource=isostore:/CustomerManager.sdf";

        public CustomersManagerDataContext()
            : this(DbConnectionString)
        {
        }

        public CustomersManagerDataContext(string connectionString)
            : base(connectionString)
        {
            if (!DatabaseExists())
                CreateDatabase();
        }

        public Table<Customer> Customers;
    }
}
