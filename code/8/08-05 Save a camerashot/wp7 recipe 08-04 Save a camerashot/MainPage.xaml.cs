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
using Microsoft.Phone.Tasks;
using System.IO;
using Microsoft.Phone;
using System.Windows.Media.Imaging;

namespace wp7_recipe_08_04_Save_a_camerashot
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask cameraTask;
        byte[] _imageAsByte;
        WriteableBitmap imageAsBitmap;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            cameraTask = new CameraCaptureTask();
            cameraTask.Completed += new EventHandler<PhotoResult>(cameraTask_Completed);
            AlbumsListBox.ItemsSource = IsolatedStorageFile.GetUserStoreForApplication().GetDirectoryNames();
            if (AlbumsListBox.Items.Count > 0)
                AlbumsListBox.SelectedIndex = 0;
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            cameraTask.Show();
        }

        void cameraTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                _imageAsByte = new byte[e.ChosenPhoto.Length];
                e.ChosenPhoto.Read(_imageAsByte, 0, _imageAsByte.Length);

                e.ChosenPhoto.Seek(0, SeekOrigin.Begin);

                this.image.Source = PictureDecoder.DecodeJpeg(e.ChosenPhoto);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_imageAsByte == null)
                MessageBox.Show("Nothing to Save");

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {

                if (!store.FileExists(System.IO.Path.Combine(AlbumsListBox.SelectedItem as string,FileNameTextBox.Text)) || 
                    store.FileExists(System.IO.Path.Combine(AlbumsListBox.SelectedItem as string,FileNameTextBox.Text)) && MessageBox.Show("Overwrite the file?", "Question", MessageBoxButton.OKCancel) == MessageBoxResult.OK) 
                {
                    using (var stream = store.CreateFile(System.IO.Path.Combine(AlbumsListBox.SelectedItem as string, FileNameTextBox.Text))) 
                    {
                        stream.Write(_imageAsByte, 0, _imageAsByte.Length);
                    }
                }
            }
        }

        private void PhotosButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Albums.xaml", UriKind.Relative));
        }

        private void CreateAlbumButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateAlbum.xaml", UriKind.Relative));
        }


    }
}