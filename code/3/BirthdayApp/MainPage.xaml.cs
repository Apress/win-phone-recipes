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
using Microsoft.Phone.UserData;
using Microsoft.Phone.Shell;

namespace BirthdayApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        ProgressIndicator piBar = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            SystemTray.SetIsVisible(this, true);
            piBar = new ProgressIndicator();
            piBar.Text = "Searching...";
            piBar.IsIndeterminate = true;

            SystemTray.SetProgressIndicator(this, piBar);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            piBar.IsVisible = true;

            lbToday.Items.Clear();
            lbTomorrow.Items.Clear();

            Contacts birthdays = new Contacts();
            birthdays.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(birthdays_SearchCompleted);
            birthdays.SearchAsync(string.Empty, FilterKind.None, "today");
            birthdays.SearchAsync(string.Empty, FilterKind.None, "tomorrow");
            
            base.OnNavigatedTo(e);
        }

        void birthdays_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            IEnumerable<Contact> contacts = e.Results;
            IEnumerable<Contact> birthdays = null;
            App app = Application.Current as App;

            if (e.State.ToString() == "today")
            {
                birthdays = from b in contacts
                            where b.Birthdays.FirstOrDefault().Month == DateTime.Now.Month && 
                                  b.Birthdays.FirstOrDefault().Day == DateTime.Now.Day
                            select b;
                
                lbToday.ItemsSource = birthdays;

                if (birthdays.Count() > 0)
                {
                    app.msgReminder = "Today is ";
                    app.msgReminder += birthdays.First().DisplayName;
                    app.msgReminder += " birthday!";
                }
            }
            else
            {
                DateTime tomorrow = DateTime.Now.AddDays(1);
                birthdays = from b in contacts
                            where b.Birthdays.FirstOrDefault().Month == tomorrow.Month &&
                                  b.Birthdays.FirstOrDefault().Day == tomorrow.Day
                            select b;

                lbTomorrow.ItemsSource = birthdays;
            }

            piBar.IsVisible = false;
        }
    }
}