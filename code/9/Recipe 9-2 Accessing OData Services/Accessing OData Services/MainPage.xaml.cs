using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Accessing_OData_Services.Mix11Reference;
using Microsoft.Phone.Controls;

namespace Accessing_OData_Services
{
    public partial class MainPage : PhoneApplicationPage
    {
        private DataServiceCollection<Session> sessions;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	eventsEntities entities = new eventsEntities(new Uri("http://live.visitmix.com/odata"));

            sessions = new DataServiceCollection<Session>(entities);

            var query = from s in entities.Sessions
                        select s;

            // Register for the LoadCompleted event.
            sessions.LoadCompleted
                += sessions_LoadCompleted;

            // Load the customers feed by executing the LINQ query.
            sessions.LoadAsync(query);

        }

        private void sessions_LoadCompleted(object sender, LoadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                // Handling for a paged data feed.
                if (sessions.Continuation != null)
                {
                    // Automatically load the next page.
                    sessions.LoadNextPartialSetAsync();
                }
                else
                {
                    // Set the data context of the list box control to the sample data.
                    this.SessionsListBox.ItemsSource = sessions;
                }
            }
            else
            {
                MessageBox.Show(string.Format("An error has occurred: {0}", e.Error.Message));
            }

        }
    }
}