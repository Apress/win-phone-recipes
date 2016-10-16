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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
using Microsoft.Devices;
using System.IO;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;

namespace BabyMonitor
{
    public partial class MainPage : PhoneApplicationPage
    {
        private VideoBrush videoRecorderBrush;
        private CaptureSource captureSource;
        private VideoCaptureDevice videoCaptureDevice;

        DispatcherTimer _timer;

        Client _client;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100); 
            _timer.Tick += new EventHandler(_timer_Tick);

            _timer.Start();

            // change the first value with your machine name or IP address
            _client = new Client("ferracchiati-pc", 13001); 
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (captureSource.State == CaptureState.Started)    
            {       
                captureSource.CaptureImageAsync();    
            } 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InitializeVideoRecorder();

            captureSource.CaptureImageAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisposeVideoRecorder();

            base.OnNavigatedFrom(e);
        }
     
        public void InitializeVideoRecorder()
        {
            if (captureSource == null)
            {
                captureSource = new CaptureSource();

                videoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                
                // Uncomment to send black and white images
                //videoCaptureDevice.DesiredFormat = new VideoFormat(PixelFormatType.Format8bppGrayscale, 640, 480, 30);
                //captureSource.AudioCaptureDevice = null;
                //captureSource.VideoCaptureDevice = videoCaptureDevice;

                captureSource.CaptureImageCompleted += new EventHandler<CaptureImageCompletedEventArgs>(captureSource_CaptureImageCompleted);
                captureSource.CaptureFailed += new EventHandler<ExceptionRoutedEventArgs>(OnCaptureFailed);

                if (videoCaptureDevice != null)
                {
                    videoRecorderBrush = new VideoBrush();
                    videoRecorderBrush.SetSource(captureSource);

                    viewfinderRectangle.Fill = videoRecorderBrush;

                    captureSource.Start();
                }
            }
        }

        void captureSource_CaptureImageCompleted(object sender, CaptureImageCompletedEventArgs e)
        {
            // Avoiding to send data when the phone is not connected 
            // to WiFi and power source
            if (DeviceNetworkInformation.IsWiFiEnabled &&
                DeviceNetworkInformation.IsCellularDataEnabled &&
                DeviceNetworkInformation.IsNetworkAvailable &&
                DeviceStatus.PowerSource == PowerSource.External)
            {
                // Convert WriteableImage to Jpeg
                WriteableBitmap wb = (WriteableBitmap)e.Result;
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveJpeg(ms, wb.PixelWidth, wb.PixelHeight, 0, 100);

                    // Send image to the client
                    _client.SendData(ms);
                }
            }
        }

        private void DisposeVideoRecorder()
        {
            if (captureSource != null)
            {
                if (captureSource.VideoCaptureDevice != null
                    && captureSource.State == CaptureState.Started)
                {
                    captureSource.Stop();
                }
                captureSource.CaptureImageCompleted -= captureSource_CaptureImageCompleted;
                captureSource.CaptureFailed -= OnCaptureFailed;

                captureSource = null;
                videoCaptureDevice = null;
                videoRecorderBrush = null;

                _timer.Stop();
            }
        }

        private void OnCaptureFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
                MessageBox.Show("ERROR: " + e.ErrorException.Message.ToString());
            });
        }
    }
}