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

namespace _7Drum
{
    public partial class TimeCounter : PhoneApplicationPage
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public TimeCounter()
        {
            InitializeComponent();

            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (tbCounter.Text.Length > 0)
            {
                int min = 0;
                int.TryParse(tbCounter.Text.Split(':')[0], out min);
                int sec = 0;
                int.TryParse(tbCounter.Text.Split(':')[1], out sec);

                if (sec == 0 && min == 0)
                {
                    tbCounter.Text = string.Format("{0}:{1}", min, sec.ToString().PadLeft(2, '0'));

                    dispatcherTimer.Stop();

                    // Play stop sound

                    return;
                }

                if (sec == 0)
                {
                    sec = 60;
                    min--;
                }

                sec--;

                tbCounter.Text = string.Format("{0}:{1}", min, sec.ToString().PadLeft(2, '0'));
            }            
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Duration"))
            {
                int min = 0;
                int.TryParse(this.NavigationContext.QueryString["Duration"], out min);
                tbCounter.Text = string.Format("{0}:00", min);
            }
        }
    }
}