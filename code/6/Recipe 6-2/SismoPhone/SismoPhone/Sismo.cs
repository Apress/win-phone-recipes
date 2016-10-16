using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices.Sensors;

namespace SismoPhone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Sismo : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Accelerometer accelerometer = null;
        VertexBuffer vertexBuffer = null;
        List<VertexPositionColor[]> vertici;
        float yPosition = 240;
        public Sismo()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.

            TargetElapsedTime = TimeSpan.FromTicks(333333);

        }

        void accelerometer_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {

           double magnitude = e == null ? 0 : Math.Sqrt(Math.Pow(e.X, 2) + Math.Pow(e.Y, 2) + Math.Pow(e.Z, 2));
            VertexPositionColor[] vertex = new VertexPositionColor[1];
            vertex[0] = new VertexPositionColor(new Vector3(0, yPosition + (float)magnitude, 1), Color.White);
            vertici.Add(vertex);

            if (vertici.Count > 800)
                vertici.RemoveAt(0);
            vertici.ForEach(v => v[0].Position.X++);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            accelerometer = new Accelerometer();
            accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(accelerometer_ReadingChanged);
            accelerometer.Start();
            vertexBuffer = new VertexBuffer(graphics.GraphicsDevice, typeof(VertexPositionColor), 2, BufferUsage.WriteOnly);
            vertici = new List<VertexPositionColor[]>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //force reading accelerometer data
            accelerometer_ReadingChanged(null, null);
            vertici.ForEach(v => vertexBuffer.SetData<VertexPositionColor>(v));
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, vertici.Count);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
