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

namespace GesturesWithToolkit
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool _isDrag;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _isDrag = false;

        }

        private void GestureListener_DragStarted(object sender, DragStartedGestureEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            _isDrag = true;
            rect.CaptureMouse();
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (_isDrag)
            {
                Rectangle rect = sender as Rectangle;
                rect.SetValue(Canvas.TopProperty, (double)rect.GetValue(Canvas.TopProperty) + e.VerticalChange);
                rect.SetValue(Canvas.LeftProperty, (double)rect.GetValue(Canvas.LeftProperty) + e.HorizontalChange);
            }
        }

        private void GestureListener_DragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            _isDrag = false;
            rect.ReleaseMouseCapture();

            if (CollisionDetected())
                MessageBox.Show("Collision detected!");
        }

        private bool CollisionDetected()
        {
            double rSourcePos = rSource.Height + (double)rSource.GetValue(Canvas.TopProperty);
            if (rSourcePos >= 660)
                return true;
            else
                return false;
        }
    }
}