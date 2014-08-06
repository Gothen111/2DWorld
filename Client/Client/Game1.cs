using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameLibrary.Configuration;
using GameLibrary.Connection;
using GameLibrary.Util;

using Client.Commands;
using Client.Connection;

using Client.Particle; // Remove

namespace Client
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private FrameCounter frameCounter = new FrameCounter();

        private ParticleEngine particleEngine; // Remove

         /*
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
         */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;

            this.IsMouseVisible = true;

            Configuration.isHost = false;
            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();

            GameLibrary.Connection.NetworkManager.client = new GameLibrary.Connection.Client();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameLibrary.Camera.Camera.camera = new GameLibrary.Camera.Camera(GraphicsDevice.Viewport);

            //Configuration.networkManager.Start("127.0.0.1", "14242");

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

            GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.loadGeneral(Content);


            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Particle/circle"));
            textures.Add(Content.Load<Texture2D>("Particle/star"));    // Remove
            textures.Add(Content.Load<Texture2D>("Particle/diamond"));
            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));


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
            GameLibrary.Model.Player.PlayerContoller.playerContoller.update();
            if (this.IsActive)
            {
                GameLibrary.Peripherals.KeyboardManager.keyboardManager.update();
                GameLibrary.Peripherals.MouseManager.mouseManager.update();
            }
            if (GameLibrary.Model.Map.World.World.world != null)
            {
                GameLibrary.Model.Map.World.World.world.update();
            }
            Configuration.networkManager.update();
            GameLibrary.Camera.Camera.camera.update(gameTime);
            // TODO: Add your update logic here


            //particleEngine.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            //particleEngine.Update(); // Remove



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameLibrary.Gui.MenuManager.menuManager.ActiveContainer.draw(GraphicsDevice, spriteBatch);

            spriteBatch.Begin();
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                frameCounter.Update(deltaTime);

                var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);

                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + fps, new Vector2(100, 0), Color.White);

                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0, 0), Color.White);
            }

            spriteBatch.End();

            particleEngine.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
