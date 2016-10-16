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
using Microsoft.Devices.Radio;
using System.Device.Location;
using Microsoft.Devices.Sensors;


namespace ObscureUnobscuredApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        Accelerometer acc;
        GeoCoordinateWatcher geoW;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            acc = new Accelerometer();
            geoW = new GeoCoordinateWatcher();

            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            PhoneApplicationFrame rootFrame = ((App)Application.Current).RootFrame;
            rootFrame.Obscured += new EventHandler<ObscuredEventArgs>(rootFrame_Obscured);
            rootFrame.Unobscured += new EventHandler(rootFrame_Unobscured);
        }

        void rootFrame_Unobscured(object sender, EventArgs e)
        {
            FMRadio.Instance.PowerMode = RadioPowerMode.On;
            acc.Start();
            geoW.Start();
        }

        void rootFrame_Obscured(object sender, ObscuredEventArgs e)
        {            
            FMRadio.Instance.PowerMode = RadioPowerMode.Off;
            acc.Stop();
            geoW.Stop();
        }
    }
}