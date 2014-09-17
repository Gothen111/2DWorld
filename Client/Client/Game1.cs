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
using Utility;

using GameLibrary.Setting;

using Client.Commands;
using Client.Connection;

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

         /*
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
         */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            this.IsMouseVisible = true;

            Configuration.isHost = false;
            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();

            Setting.logInstance = "Log/ClientLog-" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString().Replace(":", ".") + ".txt";

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

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

            GameLibrary.Gui.MenuManager.menuManager.ActiveContainer.update();

            // TODO: Add your update logic here

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

            base.Draw(gameTime);
        }
    }
}
