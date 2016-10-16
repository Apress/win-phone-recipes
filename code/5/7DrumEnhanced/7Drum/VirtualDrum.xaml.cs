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
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Microsoft.Xna.Framework;

namespace _7Drum
{
    public partial class VirtualDrum : PhoneApplicationPage
    {
        WriteableBitmap wbmp;
        BitmapImage bmpMask;
        List<DrumKitComponent> drum;

        // Constructor
        public VirtualDrum()
        {
            InitializeComponent();

            bmpMask = new BitmapImage(new Uri("images/mask_480x800.png", UriKind.Relative));
            bmpMask.CreateOptions = BitmapCreateOptions.None;
            drum = new List<DrumKitComponent>();
            drum.Add(new Crash1());
            drum.Add(new Crash2());
            drum.Add(new Kick());
            drum.Add(new Snare());
            drum.Add(new Tom1());
            drum.Add(new Tom2());
            drum.Add(new Tom3());
            drum.Add(new Tom4());
            drum.Add(new Ride());
            drum.Add(new Hihat());

            Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            TouchPointCollection tpc = e.GetTouchPoints(this);

            foreach (TouchPoint t in tpc)
            {
                if (t.Action == TouchAction.Down)
                {
                    System.Windows.Media.Color pickedColor = PickTappedColor(t.Position.X, t.Position.Y);

                    DrumKitComponent component = (from d in drum
                                                  where d.maskColor == pickedColor
                                                  select d).SingleOrDefault<DrumKitComponent>();

                    if (component != null)
                    {
                        SoundEffect se = SoundEffect.FromStream(TitleContainer.OpenStream(component.audioFileName));
                        se.Play();
                    }
                }
            }
        }

        private System.Windows.Media.Color PickTappedColor(double X, double Y)
        {
            wbmp = new WriteableBitmap(bmpMask);
            int pixel = (int)Y * (int)wbmp.PixelWidth + (int)X;
            int i = wbmp.Pixels[pixel];
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(255, (byte)((i >> 16) & 0xFF), (byte)((i >> 8) & 0xFF), (byte)(i & 0xFF));

            return color;
        }
    }
}