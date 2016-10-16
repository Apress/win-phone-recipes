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
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace FunnyRepeater
{
    public partial class MainPage : PhoneApplicationPage
    {
        Microphone mic = Microphone.Default;
        byte[] data = null;
        MemoryStream audio = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            mic.BufferReady += new EventHandler<EventArgs>(mic_BufferReady);
        }

        void mic_BufferReady(object sender, EventArgs e)
        {
            mic.GetData(data);
            audio.Write(data, 0, data.Length);
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            if (mic.State == MicrophoneState.Stopped)
            {
                mic.BufferDuration = TimeSpan.FromMilliseconds(100);
                data = new byte[mic.GetSampleSizeInBytes(mic.BufferDuration)];
                audio = new MemoryStream();
                mic.Start();
                this.PageTitle.Text = "recording...";
                btnRecord.Content = "Stop";
            }
            else
            {
                mic.Stop();
                this.PageTitle.Text = "playing";
                btnRecord.Content = "Record";
                btnRecord.IsEnabled = false;
                PlayRecordedAudio();
                this.PageTitle.Text = "ready";
            }
        }

        private void PlayRecordedAudio()
        {
            SoundEffect se = new SoundEffect(audio.ToArray(), mic.SampleRate, AudioChannels.Mono);
            se.Play(1.0f, 1.0f, 0.0f);
            btnRecord.IsEnabled = true;
        }
    }
}