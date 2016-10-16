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
using Microsoft.Devices.Sensors;
using System.Threading;

namespace Wp7AccelerometerRecipe
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Accelerometer accelerometer = null;
        DateTimeOffset movementMoment = new DateTimeOffset();
        double firstShakeStep = 0;



        private void calculateShake(object obj)
        {
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            accelerometer = new Accelerometer();
            accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(Accelerometer_ReadingChanged);
            accelerometer.Start();
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            if (e.Timestamp.Subtract(movementMoment).Duration().TotalMilliseconds <= 500)
            {
                if ((e.X <= -1 || e.X >= 1) && (firstShakeStep <= Math.Abs(e.X)))
                    firstShakeStep = e.X;
                
                if (firstShakeStep != 0)
                    {
                        firstShakeStep = 0;
                        Deployment.Current.Dispatcher.BeginInvoke(() => ResetTextBox());
                        //ResetTextBox();
                    }
            }
            movementMoment = e.Timestamp;
        }

        private void ResetTextBox()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }

        private void initAccelerometer()
        {
            accelerometer = new Accelerometer();
            accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(Accelerometer_ReadingChanged);
            accelerometer.Start();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.textBox1.Text = "Test";
            this.textBox2.Text = "Test";
            this.textBox3.Text = "Test";
            this.textBox4.Text = "Test";
        }

    }
}