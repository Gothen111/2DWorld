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

using GameLibrary.Model;
using GameLibrary.Model.Map;
using GameLibrary.Factory;
using GameLibrary.Model.Object;
using GameLibrary.Ressourcen;
using GameLibrary.Configuration;
using GameLibrary.Connection;

using Server.Connection;

namespace Server
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int counter = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 800;

            Configuration.isHost = true;
            Configuration.commandManager = new Commands.ServerCommandManager();
            Configuration.networkManager = new ServerNetworkManager();


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
            GameLibrary.Camera.Camera.camera = new GameLibrary.Camera.Camera(GraphicsDevice.Viewport);

            Configuration.networkManager.Start("", "14242");

            GameLibrary.Model.Map.World.World.world = new GameLibrary.Model.Map.World.World("Welt");

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

            Configuration.networkManager.update();


            /*if (counter <= 0)
            {

                GameLibrary.Model.Object.NpcObject var_NpcObject = GameLibrary.Factory.CreatureFactory.creatureFactory.createNpcObject(GameLibrary.Factory.FactoryEnums.RaceEnum.Human, GameLibrary.Factory.FactoryEnums.FactionEnum.Beerdrinker, GameLibrary.Factory.FactoryEnums.CreatureEnum.Archer, GameLibrary.Factory.FactoryEnums.GenderEnum.Female);

                int var_X = GameLibrary.Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = GameLibrary.Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_NpcObject.Position = new Vector3(var_X, var_Y, 0);

                GameLibrary.Model.Map.World.World.world.addLivingObject(var_NpcObject);

                counter = 10;
            }
            else
            {
                counter -= 1;
            }*/



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
