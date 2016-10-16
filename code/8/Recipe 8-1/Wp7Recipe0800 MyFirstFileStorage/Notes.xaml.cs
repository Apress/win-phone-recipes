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

namespace Wp7Recipe0800_MyFirstFileStorage
{
    public partial class Notes : PhoneApplicationPage
    {
        public Notes()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                this.NotesListBox.ItemsSource = store.GetFileNames();
            }
        }

        private void Notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?note={0}", e.AddedItems[0]), UriKind.Relative));
        }
    }
}