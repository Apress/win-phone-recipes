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
using System.IO;
using System.IO.IsolatedStorage;

namespace Wp7Recipe0800_MyFirstFileStorage
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
            if (NavigationContext.QueryString.ContainsKey("note"))
            {
                string filename = this.NavigationContext.QueryString["note"];
                if (!string.IsNullOrEmpty(filename))
                {
                    using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    using (var stream = new IsolatedStorageFileStream(filename, FileMode.Open, FileAccess.ReadWrite, store))
                    {
                        StreamReader reader = new StreamReader(stream);
                        this.NoteTextBox.Text = reader.ReadToEnd();
                        this.FilenameTextBox.Text = filename;
                        reader.Close();
                    }
                }
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                using (var stream = new IsolatedStorageFileStream(FilenameTextBox.Text, FileMode.Create, FileAccess.Write, store))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(NoteTextBox.Text);
                    writer.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving the file");
            }
        }

        private void ListButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Notes.xaml", UriKind.Relative));
        }
    }
}