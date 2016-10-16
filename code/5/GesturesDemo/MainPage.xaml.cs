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
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace GesturesDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
            TouchPanel.EnabledGestures = GestureType.Tap 
                                         | GestureType.DoubleTap 
                                         | GestureType.Flick
                                         | GestureType.Pinch 
                                         | GestureType.HorizontalDrag 
                                         | GestureType.VerticalDrag 
                                         | GestureType.PinchComplete 
                                         | GestureType.DragComplete 
                                         | GestureType.Hold 
                                         | GestureType.FreeDrag;
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {            
            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                
                PageTitle.Text = gesture.GestureType.ToString();

                if (gesture.GestureType == GestureType.Flick)
                    PageTitle.Text += " " + gesture.Delta.ToString();
            }
        }
    }
}