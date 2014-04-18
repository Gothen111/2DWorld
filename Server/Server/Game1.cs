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

        Server.Model.Map.World.World world;
        Server.Model.Map.Region.Region region; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 800;

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

            Race race = BehaviourFactory.behaviourFactory.getRace(RaceEnum.Human);
            Faction faction = BehaviourFactory.behaviourFactory.getFaction(FactionEnum.Castle_Test2);
            foreach(Model.Behaviour.BehaviourItem<Race> behaviourItem in race.BehaviourMember)
            {
                Logger.Logger.LogInfo("Rasse " + race.Type.ToString() + " hat ein Verhalten zu " + behaviourItem.Item.Type.ToString() + " mit " + behaviourItem.Value.ToString());
            }

            foreach (Model.Behaviour.BehaviourItem<Faction> behaviourItem in faction.BehaviourMember)
            {
                Logger.Logger.LogInfo("Fraktion " + faction.Type.ToString() + " hat ein Verhalten zu " + behaviourItem.Item.Type.ToString() + " mit " + behaviourItem.Value.ToString());
            }

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            world = new Model.Map.World.World("Welt");
            region = RegionFactory.regionFactory.generateRegion(0, "Region", 0, 0, Model.Map.Region.RegionEnum.Grassland, world);

            for (int i = 0; i < 20; i++)
            {
                Model.Object.LivingObject var_LivingObject = CreatureFactory.creatureFactory.createNpcObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
                Logger.Logger.LogDeb(var_LivingObject.ToString());
                var_LivingObject.Position = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(0, ChunkFactory.chunkSizeX * Model.Map.Block.Block.BlockSize), Server.Util.Random.GenerateGoodRandomNumber(0, ChunkFactory.chunkSizeY * Model.Map.Block.Block.BlockSize), 0);
                //var_LivingObject.Position = new Vector3(200*i, 50, 0);
                var_LivingObject.GraphicPath = "Character/Char1_Small";
                var_LivingObject.Velocity = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(5, 6) * 0.05f, Server.Util.Random.GenerateGoodRandomNumber(5, 6) * 0.05f, 0);
                var_LivingObject.World = world;
                region.Chunks[0, 0].addLivingObjectToChunk(var_LivingObject);
                //Logger.Logger.LogDeb(var_LivingObject.Velocity.X + " : " + var_LivingObject.Velocity.Y); 
            }

            world.addRegion(region);

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

            Ressourcen.RessourcenManager.ressourcenManager.loadGeneral(Content);

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
            Commands.Executer.Executer.executer.update(0);
            region.Chunks[0,0].update();

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

            world.DrawTest(GraphicsDevice, spriteBatch);
            spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0,0), Color.White);
            spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Units: " + region.Chunks[0, 0].getCountofAllObjects().ToString(), new Vector2(100, 0), Color.White);
            
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
