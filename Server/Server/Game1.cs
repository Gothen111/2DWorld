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
            foreach (Model.Behaviour.BehaviourItem<Race> behaviourItem in race.BehaviourMember)
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

            world.addRegion(region);
            for (int i = 0; i < 0; i++)
            {
                Model.Object.LivingObject var_LivingObject = CreatureFactory.creatureFactory.createNpcObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
                Logger.Logger.LogDeb("LivingObject wurde erstellt");
                Server.Commands.CommandTypes.WalkRandomCommand command = new Server.Commands.CommandTypes.WalkRandomCommand(var_LivingObject);
                Server.Commands.Executer.Executer.executer.addCommand(command);
                Server.Commands.CommandTypes.AttackRandomCommand command2 = new Server.Commands.CommandTypes.AttackRandomCommand(var_LivingObject);
                Server.Commands.Executer.Executer.executer.addCommand(command2);
                var_LivingObject.Position = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeX * (Model.Map.Block.Block.BlockSize - 1)), Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeY * (Model.Map.Block.Block.BlockSize - 1)), 0);
                //var_LivingObject.Position = new Vector3(20*i, 50, 0);
                var_LivingObject.GraphicPath = "Character/Char1_Small";
                var_LivingObject.Scale = 1f;
                //var_LivingObject.Velocity = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(5, 6) * 0.05f, Server.Util.Random.GenerateGoodRandomNumber(5, 6) * 0.05f, 0);
                //var_LivingObject.Velocity = new Vector3(1,0,0);

                var_LivingObject.World = world;
                world.addLivingObject(var_LivingObject);
                //Logger.Logger.LogDeb(var_LivingObject.Velocity.X + " : " + var_LivingObject.Velocity.Y); 
            }

            for (int i = 0; i < 0; i++)
            {
                Model.Object.EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(EnvironmentEnum.Tree_Normal_1);

                var_EnvironmentObject.Position = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeX * (Model.Map.Block.Block.BlockSize - 1)), Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeY * (Model.Map.Block.Block.BlockSize - 1)), 0);
                var_EnvironmentObject.World = world;
                //region.getChunkAtPosition(0, 0).getBlockAtCoordinate(var_EnvironmentObject.Position.X, var_EnvironmentObject.Position.Y).objectsLaterEnviorment.Add(var_EnvironmentObject);
                //world.addLivingObject(var_EnvironmentObject, false);
            }

            for (int i = 0; i < 0; i++)
            {
                Model.Object.EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(EnvironmentEnum.Flower_1);

                var_EnvironmentObject.Position = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeX * (Model.Map.Block.Block.BlockSize - 1)), Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeY * (Model.Map.Block.Block.BlockSize - 1)), 0);
                var_EnvironmentObject.World = world;

                //region.getChunkAtPosition(0, 0).getBlockAtCoordinate(var_EnvironmentObject.Position.X, var_EnvironmentObject.Position.Y).objectsPreEnviorment.Add(var_EnvironmentObject);


                //world.addLivingObject(var_EnvironmentObject, false);
            }

            Model.Object.PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            var_PlayerObject.Position = new Vector3(200, 200, 0);
            var_PlayerObject.GraphicPath = "Character/Char1_Small";
            var_PlayerObject.World = world;
            world.addLivingObject(var_PlayerObject);

            Model.Object.EnvironmentObject var_Chest = EnvironmentFactory.environmentFactory.createEnvironmentObject(EnvironmentEnum.Chest);

            var_Chest.Position = new Vector3(250, 200, 0);
            var_Chest.World = world;
            world.addLivingObject(var_Chest);
            //region.getChunkAtPosition(0, 0).getBlockAtCoordinate(var_Chest.Position.X, var_Chest.Position.Y).objectsPreEnviorment.Add(var_Chest);

            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.W }, new Commands.CommandTypes.WalkUpCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.S }, new Commands.CommandTypes.WalkDownCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.A }, new Commands.CommandTypes.WalkLeftCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.D }, new Commands.CommandTypes.WalkRightCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.Space }, new Commands.CommandTypes.AttackWithWeaponCommand(var_PlayerObject)));


            watch.Stop();
            Logger.Logger.LogDeb("Time spent: " + watch.Elapsed);

            Util.MapHandler var_MapHandler = new Util.MapHandler(40, 20, 35);
            var_MapHandler.PrintMap();

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
            Commands.Executer.Executer.executer.update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            Model.Player.PlayerContoller.playerContoller.update();
            world.update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();//SpriteSortMode.FrontToBack, BlendState.Opaque);

            world.DrawTest2(GraphicsDevice, spriteBatch);
            spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Units: " + region.Chunks[0, 0].getCountofAllObjects().ToString(), new Vector2(100, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
