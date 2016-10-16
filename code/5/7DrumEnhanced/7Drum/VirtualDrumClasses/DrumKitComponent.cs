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

namespace _7Drum
{
    public abstract class DrumKitComponent
    {
        public virtual Color maskColor
        {
            get { return Colors.Black; }
        }

        public virtual string audioFileName
        {
            get { return string.Empty; }
        }
    }
}
