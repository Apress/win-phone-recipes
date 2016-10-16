using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;

namespace AddGestureDemo
{
    public class AddTap : Behavior<UIElement>
    {
        public event EventHandler Tap;
        public event EventHandler Hold;
        private bool _IsMousePressed;
        private TimeSpan _mouseLeftButtonDownTime;

        protected override void OnAttached()
        {
            this.AssociatedObject.MouseLeftButtonDown += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
            this.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            base.OnDetaching();
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _IsMousePressed = true;
            _mouseLeftButtonDownTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan _elapsedTime = TimeSpan.FromTicks(DateTime.Now.Ticks) - _mouseLeftButtonDownTime;
            if (_IsMousePressed && _elapsedTime.Ticks < TimeSpan.TicksPerSecond)
                DoTap();
            else
                DoHold();

            _IsMousePressed = false;
        }

        void DoTap()
        {
            if (Tap != null)
                Tap(AssociatedObject, EventArgs.Empty);
        }

        void DoHold()
        {
            if (Hold != null)
                Hold(AssociatedObject, EventArgs.Empty);
        }        
    }
}
