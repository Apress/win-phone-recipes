using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNATombstoning
{
    public class GameSettings
    {
        public bool Paused { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }

        public GameSettings()
        {
            Paused = false;
            Location = Vector2.Zero;
            Velocity = new Vector2(1f, 1f);
        }
    }
}
