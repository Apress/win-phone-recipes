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
using System.Windows.Threading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace _7Drum
{
    public partial class Metronome : PhoneApplicationPage
    {
        DispatcherTimer timer;
        SoundEffect se = null;

        public Metronome()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            se = SoundEffect.FromStream(TitleContainer.OpenStream("audio/click_b.wav"));
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            se.Play();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                timer.Interval = TimeSpan.FromMilliseconds(1000 * (60 / sTempo.Value));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
        }

        private void sTempo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sTempo != null)
            {
                tbTempo.Text = string.Format("Tempo: {0} bpm", (int)sTempo.Value);

                if (timer.IsEnabled)
                {
                    timer.Interval = TimeSpan.FromMilliseconds(1000 * (60 / sTempo.Value));
                }
            }
        }
    }
}