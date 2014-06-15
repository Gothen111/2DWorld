using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Server.Connection;

using GameLibrary.Model;
using GameLibrary.Model.Map;
using GameLibrary.Factory;
using GameLibrary.Model.Object;
using GameLibrary.Ressourcen;

namespace Server
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //GameLibrary.Model.Map.Region.Region region;

        GameLibrary.Model.Object.PlayerObject playerObject;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 800;

            ServerNetworkManager.serverNetworkManager.Connect(14242);

            GameLibrary.Configuration.Configuration.isHost = true;
            GameLibrary.Configuration.Configuration.commandManager = new Commands.ServerCommandManager();

            this.IsMouseVisible = true;
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

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            GameLibrary.Model.Map.World.World.world = new GameLibrary.Model.Map.World.World("Welt");
            //region = GameLibrary.Factory.RegionFactory.regionFactory.generateRegion("Region", 0, 0, GameLibrary.Model.Map.Region.RegionEnum.Grassland, GameLibrary.Model.Map.World.World.world);

            //GameLibrary.Model.Map.World.World.world.addRegion(region);

            watch.Stop();
            GameLibrary.Logger.Logger.LogInfo(watch.Elapsed.ToString());

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
            GameLibrary.Commands.Executer.Executer.executer.update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            GameLibrary.Model.Player.PlayerContoller.playerContoller.update();
            GameLibrary.Model.Map.World.World.world.update();
            GameLibrary.Camera.Camera.camera.update(gameTime);

            ServerNetworkManager.serverNetworkManager.update();

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
