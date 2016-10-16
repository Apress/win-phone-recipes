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
    public partial class Images : PhoneApplicationPage
    {
        public Images()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Images_Loaded);
        }

        void Images_Loaded(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                this.PhotoList.ItemsSource = store.GetFileNames();
            }
        }
    }
}