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
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Shell;
using Microsoft.Live;
using Microsoft.Live.Controls;

namespace BackgroundAudioSampleUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        LiveConnectClient _client;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
            btnPlay = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
        }

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            SetAppState();
        }

        private void SetAppState()
        {
            if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
                btnPlay.IconUri = new Uri("/Images/appbar.transport.pause.rest.png", UriKind.Relative);
            else
                btnPlay.IconUri = new Uri("/Images/appbar.transport.play.rest.png", UriKind.Relative);

            if (BackgroundAudioPlayer.Instance.Track != null)
            {
                txtPlaying.Text = string.Format("Song: {0}\n\nAuthor: {1}",
                                                BackgroundAudioPlayer.Instance.Track.Title,
                                                BackgroundAudioPlayer.Instance.Track.Artist);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            txtPlaying.Text = "";
            BackgroundAudioPlayer.Instance.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetAppState();

            base.OnNavigatedTo(e);
        }

        private void btnSignin_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Session != null &&
                e.Session.Status == LiveConnectSessionStatus.Connected)
            {
                _client = new LiveConnectClient(e.Session);
                _client.GetAsync("--Your file here--");
                _client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(_client_GetMp3Url);
            }
        }

        void _client_GetMp3Url(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Dictionary<string, object> file = (Dictionary<string, object>)e.Result;
                string songURL = file["source"].ToString();
                Uri songUri = new Uri(songURL);
                AudioTrack at = new AudioTrack(songUri, "Title", "Artist", "Album", null);

                BackgroundAudioPlayer.Instance.Track = at;                
            }
        }
    }
}