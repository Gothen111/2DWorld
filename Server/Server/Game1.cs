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

using Server.Factories;
using Server.Factories.FactoryEnums;
using Server.Model.Behaviour.Member;

namespace Server
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Server.Model.Map.Region.Region region; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            Race race = BehaviourFactory.behaviourFactory.getRace(RaceEnum.Human);
            Faction faction = BehaviourFactory.behaviourFactory.getFaction(FactionEnum.Wizard);

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            region = RegionFactory.regionFactory.generateRegion(0, "Test", 20, 20, Model.Map.Region.RegionEnum.Grassland);

            for (int i = 0; i < 50; i++)
            {
                Model.Object.AnimatedObject var_AnimatedObject = CreatureFactory.creatureFactory.createNpcObject(CreatureEnum.Human_Female);
                Random Rnd = new Random();

                var_AnimatedObject.Position = new Vector3(Rnd.Next(0, ChunkFactory.chunkSizeX * Model.Map.Block.Block.BlockSize), Rnd.Next(0, ChunkFactory.chunkSizeY * Model.Map.Block.Block.BlockSize), 0);
                region.Chunks.ElementAt(0).addAnimatedObjectToChunk(var_AnimatedObject);
            }

            watch.Stop();
            Logger.Logger.LogDeb("Time spent: " + watch.Elapsed);

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

            spriteBatch.Begin();
            region.DrawTest(GraphicsDevice, spriteBatch);
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
