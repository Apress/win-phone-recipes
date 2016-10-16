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
using Microsoft.Devices.Radio;
using System.Threading;
using System.Globalization;

namespace Wp7Recipe_06_06_Using_Radio
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const double minFrequencyEurope = 87.6;
        private const double maxFrequencyEurope = 107.7;

        private const double minFrequencyUnitedStates = 87.8;
        private const double maxFrequencyUnitedStates = 108;

        private const double minFrequencyJapan = 76;
        private const double maxFrequencyJapan = 90;

        FMRadio radio = FMRadio.Instance;
        Timer signalCheckTimer;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            RadioSwitch.Checked += new EventHandler<RoutedEventArgs>(RadioSwitch_CheckChanged);
            RadioSwitch.Unchecked += new EventHandler<RoutedEventArgs>(RadioSwitch_CheckChanged);
            signalCheckTimer = new Timer((o) =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    this.SignalSlider.Value = radio.SignalStrength;
                    this.FrequencyTextBlock.Text = radio.Frequency.ToString();
                    this.SignalTB.Text = radio.SignalStrength.ToString();
                    this.RadioSwitch.IsChecked = radio.PowerMode == RadioPowerMode.On;
                });
            }, null, 100, 100);

        }

        private void ManageRadioDisabledException(RadioDisabledException ex)
        {
            MessageBox.Show("The radio is not available in this moment");
        }

        private bool IsFrequencyCorrect(double frequecy)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            bool isCorrect = false;
            string frequencyAsString = Math.Round(frequecy, 1).ToString();
            switch (radio.CurrentRegion)
            {
                case RadioRegion.Europe:
                    isCorrect = frequencyAsString.EndsWith(string.Format("{0}1", separator)) ||
                                frequencyAsString.EndsWith(string.Format("{0}3", separator)) ||
                                frequencyAsString.EndsWith(string.Format("{0}5", separator)) ||
                                frequencyAsString.EndsWith(string.Format("{0}6", separator)) ||
                                frequencyAsString.EndsWith(string.Format("{0}8", separator)) ||
                                frequencyAsString.EndsWith(string.Format("{0}0", separator));
                    break;
                case RadioRegion.Japan:
                    //check here the correctness of frequecy for this position
                    break;
                case RadioRegion.UnitedStates:
                    //check here the correctness of frequecy for this position
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return isCorrect;
        }

        private void ManageFrequencyChange(double delta)
        {
            try
            {
                double frequency = radio.Frequency;
                do
                {
                    frequency += delta;
                    switch (radio.CurrentRegion)
                    {
                        case RadioRegion.Europe:
                            if (frequency < minFrequencyEurope)
                                frequency = maxFrequencyEurope;
                            if (frequency > maxFrequencyEurope)
                                frequency = minFrequencyEurope;
                            break;
                        case RadioRegion.Japan:
                            if (frequency < minFrequencyJapan)
                                frequency = maxFrequencyJapan;
                            if (frequency > maxFrequencyJapan)
                                frequency = minFrequencyJapan;
                            break;
                        case RadioRegion.UnitedStates:
                            if (frequency < minFrequencyUnitedStates)
                                frequency = maxFrequencyUnitedStates;
                            if (frequency > maxFrequencyUnitedStates)
                                frequency = minFrequencyUnitedStates;
                            
                            break;
                        default:
                            break;
                    }

                } while (!IsFrequencyCorrect(frequency));
                radio.Frequency = Math.Round(frequency, 1);

            }
            catch (RadioDisabledException ex)
            {
                ManageRadioDisabledException(ex);
            }
        }

        void RadioSwitch_CheckChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                bool powerCheck = RadioSwitch.IsChecked.HasValue
                        ? RadioSwitch.IsChecked.Value
                        : false;
                radio.PowerMode = powerCheck
                    ? RadioPowerMode.On
                    : RadioPowerMode.Off;
            }
            catch (RadioDisabledException ex)
            {
                ManageRadioDisabledException(ex);
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            ManageFrequencyChange(0.1);
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            ManageFrequencyChange(-0.1);
        }

        private void FastBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                do
                {
                    ManageFrequencyChange(-0.1);
                } while (radio.SignalStrength * 100 < 65);
            }
            catch (RadioDisabledException ex)
            {
                ManageRadioDisabledException(ex);
            }
        }

        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                do
                {
                    ManageFrequencyChange(0.1);
                } while (radio.SignalStrength * 100 < 65);
            }
            catch (RadioDisabledException ex)
            {
                ManageRadioDisabledException(ex);
            }
        }
    }
}