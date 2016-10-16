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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using System.IO;

namespace ShuffleMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        MediaLibrary library = null;
        Song _lastSong = null;
        DispatcherTimer timer = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            library = new MediaLibrary();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);

            // The shuffle routine has to work even the screen is locked
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        void timer_Tick(object sender, EventArgs e)
        {            
            PlaySong();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            App app = Application.Current as App;

            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New)
                PlaySong();
            else
            {
                tbAuthor.Text = app.settings.Title;
                if (app.settings.AlbumImage != null)
                {
                    MemoryStream ms = new MemoryStream(app.settings.AlbumImage);
                    WriteableBitmap wbimg = PictureDecoder.DecodeJpeg(ms);
                    imgCover.Source = wbimg;
                }
                else
                    imgCover.Source = new BitmapImage(new Uri("/images/nocover.png", UriKind.Relative));

                TimeSpan remainTime = library.Songs[app.settings.SongNumber].Duration - MediaPlayer.PlayPosition;
                timer.Interval = remainTime + TimeSpan.FromSeconds(1);
                timer.Start();
            }

            base.OnNavigatedTo(e);
        }

        private void PlaySong()
        {
            Song s = DoShuffle();
            if ((s != null && s != _lastSong)||(library.Songs.Count == 1))
            {
                App app = Application.Current as App;

                _lastSong = s;

                tbAuthor.Text = s.Artist.Name + ": " + s.Name;
                app.settings.Title = tbAuthor.Text;

                if (s.Album.HasArt)
                {
                    WriteableBitmap wbimg = PictureDecoder.DecodeJpeg(s.Album.GetAlbumArt());
                    imgCover.Source = wbimg;
                    using (var br = new BinaryReader(s.Album.GetAlbumArt())) app.settings.AlbumImage = br.ReadBytes((int)s.Album.GetAlbumArt().Length);
                }
                else
                {
                    app.settings.AlbumImage = null;
                    imgCover.Source = new BitmapImage(new Uri("/images/nocover.png", UriKind.Relative));
                }

                MediaPlayer.Play(s);
                timer.Interval = s.Duration + TimeSpan.FromSeconds(1);
                timer.Start();
            }
            else
                PlaySong();
        }

        private Song DoShuffle()
        {
            App app = Application.Current as App;

            int count = library.Songs.Count;

            Random rand = new Random();
            int songIndex = rand.Next(0, count);

            app.settings.SongNumber = songIndex;
            return library.Songs[songIndex];
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            PlaySong();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MediaPlayer.Stop();
            timer.Stop();

            base.OnBackKeyPress(e);
        }
    }
}