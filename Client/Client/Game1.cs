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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            GameLibrary.Configuration.Configuration.isHost = false;
            GameLibrary.Configuration.Configuration.commandManager = new Commands.ClientCommandManager();
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

            GameLibrary.Camera.Camera.camera = new GameLibrary.Camera.Camera(GraphicsDevice.Viewport);

            GameLibrary.Model.Map.World.World.world = new GameLibrary.Model.Map.World.World("Welt");
            
            ClientNetworkManager.clientNetworkManager.Start("127.0.0.1", "14242");

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            GameLibrary.Model.Player.PlayerContoller.playerContoller.update();
            if (this.IsActive)
            {
                GameLibrary.Peripherals.KeyboardManager.keyboardManager.update();
                GameLibrary.Peripherals.MouseManager.mouseManager.update();
            }
            GameLibrary.Model.Map.World.World.world.update();
            ClientNetworkManager.clientNetworkManager.update();
            GameLibrary.Camera.Camera.camera.update(gameTime);
            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (GameLibrary.Camera.Camera.camera.Zoom == 1f)
                {
                    GameLibrary.Camera.Camera.camera.Zoom = 0.1f;
                }
                else
                {
                    GameLibrary.Camera.Camera.camera.Zoom = 1f;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend, null, null, null, null,
                    GameLibrary.Camera.Camera.camera.getMatrix());

            if (GameLibrary.Camera.Camera.camera.Target != null)
            {
                GameLibrary.Model.Map.World.World.world.drawBlocks(GraphicsDevice, spriteBatch, GameLibrary.Camera.Camera.camera.Target);
            }
            else
            {
                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Dein Charakter ist leider gestorben :(", new Vector2(50, 50), Color.White);
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                    BlendState.AlphaBlend, null, null, null, null,
                    GameLibrary.Camera.Camera.camera.getMatrix());//spriteBatch.Begin();//SpriteSortMode.FrontToBack, BlendState.Opaque);

            if (GameLibrary.Camera.Camera.camera.Target != null)
            {
                GameLibrary.Model.Map.World.World.world.drawObjects(GraphicsDevice, spriteBatch, GameLibrary.Camera.Camera.camera.Target);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            if(gameTime.ElapsedGameTime.Milliseconds > 0)
                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Units: " + world.QuadTree.Root.quadObjects.ToString(), new Vector2(100, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
