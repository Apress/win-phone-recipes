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
using FlickrNet;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace FlickrPhotoAlbum
{
    public partial class Upload : PhoneApplicationPage
    {
        public Upload()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Please, specify the photo name");
                return;
            }

            App app = Application.Current as App;

            app.FlickrService.UploadPictureAsync(app.settings.Image,
                                                txtName.Text,
                                                txtName.Text,
                                                txtDescription.Text,
                                                txtTag.Text,
                                                true,
                                                true,
                                                true,
                                                ContentType.Photo,
                                                SafetyLevel.None,
                                                HiddenFromSearch.None, r =>
                                                {
                                                    Dispatcher.BeginInvoke(() =>
                                                    {
                                                        if (r.HasError)
                                                        {
                                                            MessageBox.Show("Ooops, error during upload...");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Upload done successfully...");
                                                        }
                                                    });
                                                });
        }

    }
}