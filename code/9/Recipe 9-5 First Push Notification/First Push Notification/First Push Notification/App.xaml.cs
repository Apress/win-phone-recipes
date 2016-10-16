using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;

namespace First_Push_Notification
{
    public partial class App : Application
    {
        private HttpNotificationChannel _notificationChannel;
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            ActivateNotificationChannel();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            ActivateNotificationChannel();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }


        private void ActivateNotificationChannel()
        {
            try
            {
                _notificationChannel = HttpNotificationChannel.Find(GlobalInformations.NotificationChannelName);
                if (_notificationChannel == null)
                {
                    _notificationChannel = new HttpNotificationChannel(GlobalInformations.NotificationChannelName);
                    _notificationChannel.Open();
                    _notificationChannel.ErrorOccurred += notificationChannel_ErrorOccurred;
                _notificationChannel.ChannelUriUpdated += notificationChannel_ChannelUriUpdated;
                _notificationChannel.ConnectionStatusChanged += _notificationChannel_ConnectionStatusChanged;
                }
                else
                {
                    Debug.WriteLine(_notificationChannel.ChannelUri);
                    _notificationChannel.ErrorOccurred += notificationChannel_ErrorOccurred;
                    _notificationChannel.ChannelUriUpdated += notificationChannel_ChannelUriUpdated;
                    _notificationChannel.ConnectionStatusChanged += _notificationChannel_ConnectionStatusChanged;
                }
                if (!_notificationChannel.IsShellTileBound)
                    _notificationChannel.BindToShellTile();

                if (!_notificationChannel.IsShellToastBound)
                    _notificationChannel.BindToShellToast();

                _notificationChannel.HttpNotificationReceived += notificationChannel_HttpNotificationReceived;
                _notificationChannel.ShellToastNotificationReceived +=
                    notificationChannel_ShellToastNotificationReceived;
            }
            catch (InvalidOperationException ex)
            {
                //notify the user of this problem  
                MessageBox.Show("If you want to use all funcionality of this application, you must disable notification in others app, because you have too many channels opened");
                //disable Push Notification in your application
            }

        }

        void _notificationChannel_ConnectionStatusChanged(object sender, NotificationChannelConnectionEventArgs e)
        {
            //if (_notificationChannel.ConnectionStatus == ChannelConnectionStatus.Disconnected)
            //    _notificationChannel.Open();
        }

        private void notificationChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void notificationChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void notificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Debug.WriteLine(e.ChannelUri);
        }

        private void notificationChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}