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

namespace Wp7Recipe_08_03_Settings
{
    public partial class Settings : PhoneApplicationPage
    {
        private const string _toastEnabledKey = "ToastEnabled";
        private const string _soundsEnabledkey = "SoundsEnabled";

        public Settings()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Settings_Loaded);
        }

        void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_toastEnabledKey))
            {
                var toast = IsolatedStorageSettings.ApplicationSettings[_toastEnabledKey].ToString();
                this.ToastSwitch.IsChecked = bool.Parse(toast);
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_soundsEnabledkey))
            {
                var sounds = IsolatedStorageSettings.ApplicationSettings[_soundsEnabledkey].ToString();
                this.SoundsSwitch.IsChecked = bool.Parse(sounds);
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings[_toastEnabledKey] = this.ToastSwitch.IsChecked;
            IsolatedStorageSettings.ApplicationSettings[_soundsEnabledkey] = this.SoundsSwitch.IsChecked;
        }
    }
}