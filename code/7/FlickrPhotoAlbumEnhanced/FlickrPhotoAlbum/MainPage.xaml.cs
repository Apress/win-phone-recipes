﻿using System;
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
using FlickrNet;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Windows.Navigation;


namespace FlickrPhotoAlbum
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask camera = null;
        Popup popup = null;
        PhotoChooserTask chooser = null;
        bool _doTask = false;
        string _token = string.Empty;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            camera = new CameraCaptureTask();
            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);

            popup = new Popup();
            popup.Child = new MySplashScreen();
            popup.IsOpen = true;

            chooser = new PhotoChooserTask();
            chooser.Completed += new EventHandler<PhotoResult>(chooser_Completed);
        }

        void chooser_Completed(object sender, PhotoResult e)
        {
            DoCompletedTask(e);
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            DoCompletedTask(e);
        }

        private void DoCompletedTask(PhotoResult e)
        {
            App app = Application.Current as App;

            if (e.TaskResult == TaskResult.OK)
            {
                byte[] data;
                using (var br = new BinaryReader(e.ChosenPhoto)) data =
                       br.ReadBytes((int)e.ChosenPhoto.Length);
                app.settings.Image = new MemoryStream(data);

                BitmapImage bi = new BitmapImage(
                                     new Uri(e.OriginalFileName));
                bi.CreateOptions = BitmapCreateOptions.DelayCreation;
                bi.SetSource(new MemoryStream(data));
                imgPhoto.Source = new WriteableBitmap(bi);
                btnUpload.IsEnabled = true;
                _doTask = true;
            }
        }

        private void btnAuthenticate_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;

            app.FlickrService.AuthGetTokenAsync(app.settings.Frob, r =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    if (r.HasError)
                    {
                        MessageBox.Show(r.Error.Message);
                    }
                    else
                    {
                        app.settings.Token = r.Result;
                        btnTakePicture.IsEnabled = true;
                        btnLoad.IsEnabled = true;
                        btnAuthenticate.IsEnabled = false;
                        wbBrowser.Visibility = System.Windows.Visibility.Collapsed;
                        imgPhoto.Visibility = System.Windows.Visibility.Visible;

                        if (_token != string.Empty)
                            btnUpload.IsEnabled = true;
                    }
                });
            });            
        }

        private void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            camera.Show();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectToFlickr();
        }

        private void ConnectToFlickr()
        {
            App a = Application.Current as App;

            Flickr flickr = a.FlickrService;

            flickr.AuthGetFrobAsync(r =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    if (r.HasError)
                    {
                        MessageBox.Show(r.Error.Message);
                    }
                    else
                    {
                        a.settings.Frob = r.Result;
                        string url = flickr.AuthCalcUrl(a.settings.Frob, AuthLevel.Write);
                        Uri uri = new Uri(url);
                        wbBrowser.Navigate(uri);
                    }
                });
            });
        }

        private void wbBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.popup.IsOpen = false;
            btnAuthenticate.IsEnabled = true;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Upload.xaml", UriKind.Relative));
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            chooser.Show();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IDictionary<string, string> queryStrings = this.NavigationContext.QueryString;

            if (queryStrings.ContainsKey("token") && !_doTask)
            {
                MediaLibrary library = new MediaLibrary();
                _token = queryStrings["token"];
                Picture picture = library.GetPictureFromToken(_token);

                BitmapImage bitmap = new BitmapImage();
                bitmap.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmap.SetSource(picture.GetImage());

                WriteableBitmap picLibraryImage = new WriteableBitmap(bitmap);
                imgPhoto.Source = picLibraryImage;

            }
        }
    }
}