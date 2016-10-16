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
using System.IO.IsolatedStorage;

namespace wp7_recipe_08_04_Save_a_camerashot
{
    public partial class CreateAlbum : PhoneApplicationPage
    {
        public CreateAlbum()
        {
            InitializeComponent();
        }

        private void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.DirectoryExists(this.AlbumNameTextbox.Text))
                    MessageBox.Show("Album already exists");
                else
                    store.CreateDirectory(AlbumNameTextbox.Text);
            }
        }
    }
}