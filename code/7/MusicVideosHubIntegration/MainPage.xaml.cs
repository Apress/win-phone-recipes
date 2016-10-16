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
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices;
using System.Windows.Resources;

namespace MusicVideosHubIntegration
{
    public partial class MainPage : PhoneApplicationPage
    {
        MediaLibrary library;
        const string _key = "nowPlayingSong";
        Song s;
        int songIndex = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            btnPlay = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            library = new MediaLibrary();
            MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(MediaPlayer_MediaStateChanged);
            s = null;
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Playing)
                btnPlay.IconUri = new Uri("/Images/appbar.transport.pause.rest.png", UriKind.Relative);
            else
                btnPlay.IconUri = new Uri("/Images/appbar.transport.play.rest.png", UriKind.Relative);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if (MediaPlayer.State == MediaState.Playing)
            {
                s = MediaPlayer.Queue.ActiveSong;
            }
            else if (NavigationContext.QueryString.ContainsKey(_key))
            {
                s = library.Songs[int.Parse(NavigationContext.QueryString[_key])];
                MediaPlayer.Play(s);
            }

            if (s != null)
            {
                tbSong.Text = "Now playing " + s.Artist + " " + s.Name;
                btnPlay.IconUri = new Uri("/Images/appbar.transport.pause.rest.png", UriKind.Relative);
            }

            base.OnNavigatedTo(e);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaHistory history = MediaHistory.Instance;

                if (s == null)
                {
                    s = DoShuffle();
                    if (s == null)
                    {
                        MessageBox.Show("The media library doesn't contain songs");
                        return;
                    }
                }

                tbSong.Text = "Now playing " + s.Artist + " " + s.Name;
                MediaPlayer.Play(s);
                UpdateNowPlaying();
                UpdateHistory();
            }
        }

        private void UpdateHistory()
        {
            MediaHistoryItem historyItem = new MediaHistoryItem();
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("NowPlayingIcon.jpg", UriKind.Relative));
            historyItem.ImageStream = sri.Stream;
            historyItem.Source = "";
            historyItem.Title = s.Name;
            historyItem.PlayerContext.Add(_key, songIndex.ToString());
            MediaHistory history = MediaHistory.Instance;
            history.WriteRecentPlay(historyItem);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            tbSong.Text = "Now stopped...";
            s = null;
        }

        private void UpdateNowPlaying()
        {
            MediaHistoryItem nowPlaying = new MediaHistoryItem();
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("NowPlayingIcon.jpg", UriKind.Relative));
            nowPlaying.ImageStream = sri.Stream;
            nowPlaying.Source = "";
            nowPlaying.Title = s.Name;
            nowPlaying.PlayerContext.Add(_key, songIndex.ToString());
            MediaHistory history = MediaHistory.Instance;
            history.NowPlaying = nowPlaying;
        }

        private Song DoShuffle()
        {
            int count = library.Songs.Count;

            if (count > 0)
            {
                Random rand = new Random();
                songIndex = rand.Next(0, count);

                return library.Songs[songIndex];
            }
            else
                return null;
        }
    }
}