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

namespace XNATombstoning
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteTexture;
        Texture2D ballTexture;
        Texture2D pauseScreen;
        Vector2 spriteLocation; 
        Vector2 spriteVelocity;
        GameSettings settings;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            settings = new GameSettings();
            settings.Paused = false;
            settings.Location = spriteLocation;
            settings.Velocity = spriteVelocity;

            TouchPanel.EnabledGestures = GestureType.Tap;
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
            ballTexture = Content.Load<Texture2D>("ball");
            pauseScreen = Content.Load<Texture2D>("pause2");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            settings = GameSettingsManager.Load();
            spriteLocation = settings.Location;
            spriteVelocity = settings.Velocity;

            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            settings.Paused = true;
            settings.Location = spriteLocation;
            settings.Velocity = spriteVelocity;

            GameSettingsManager.Save(settings);

            base.OnDeactivated(sender, args);
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

            // TODO: Add your update logic here
            if (settings.Paused == false)
            {
                spriteLocation = spriteLocation + spriteVelocity;
                spriteTexture = ballTexture;
            }
            else
            {
                spriteLocation = Vector2.Zero;
                spriteTexture = pauseScreen;

                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    switch (gs.GestureType)
                    {
                        case GestureType.Tap:
                            settings.Paused = false;
                            spriteLocation = settings.Location;
                            spriteVelocity = settings.Velocity;
                            spriteTexture = ballTexture;
                            break;
                    }
                }

            }

            if (spriteLocation.X < 0 || spriteLocation.X > graphics.GraphicsDevice.Viewport.Width - spriteTexture.Width)
            {   
                // If we get in here, we've hit a vertical wall   
                spriteVelocity.X = -spriteVelocity.X;   
                spriteLocation.X = spriteLocation.X + spriteVelocity.X;
            }
            
            if (spriteLocation.Y < 0 || spriteLocation.Y > graphics.GraphicsDevice.Viewport.Height - spriteTexture.Height)
            {   
                // If we get in here, we've hit a horizontal wall   
                spriteVelocity.Y = -spriteVelocity.Y;   
                spriteLocation.Y = spriteLocation.Y + spriteVelocity.Y;
            } 
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, spriteLocation, Color.White);
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
