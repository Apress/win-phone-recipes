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
using System.Windows.Threading;

namespace MediaElementDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private TimeSpan duration;
        private DispatcherTimer timer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            meVideo.BufferingProgressChanged += new RoutedEventHandler(meVideo_BufferingProgressChanged);
            meVideo.CurrentStateChanged += new RoutedEventHandler(meVideo_CurrentStateChanged);
            meVideo.MediaOpened += new RoutedEventHandler(meVideo_MediaOpened);
            meVideo.MediaEnded += new RoutedEventHandler(meVideo_MediaEnded);
            btnPlay = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            sPosition.Value = 0;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (meVideo.CurrentState == MediaElementState.Playing)
            {
                double currentPostition = meVideo.Position.TotalMilliseconds; 
                double progressPosition = (currentPostition * 100) / duration.TotalMilliseconds; 
                sPosition.Value = progressPosition; 
            }
        }

        void meVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            meVideo.Position = TimeSpan.Zero;
            sPosition.Value = 0;
            btnPlay.IconUri = new Uri("/Images/appbar.transport.play.rest.png", UriKind.Relative);
        }

        void meVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            duration = meVideo.NaturalDuration.TimeSpan;
        }

        void meVideo_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (meVideo.CurrentState == MediaElementState.Playing)
                btnPlay.IconUri = new Uri("/Images/appbar.transport.pause.rest.png", UriKind.Relative);
            else
                btnPlay.IconUri = new Uri("/Images/appbar.transport.play.rest.png", UriKind.Relative);
        }

        void meVideo_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            tbBuffering.Text = string.Format("Buffering...{0:P}", meVideo.BufferingProgress);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (meVideo.CurrentState == MediaElementState.Playing)
            {
                meVideo.Pause();
                timer.Stop();
            }
            else
            {
                timer.Start();
                meVideo.Play();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            meVideo.Stop();
        }
    }
}