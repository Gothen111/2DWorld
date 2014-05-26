using System;
using System.IO;
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

using Server.Factories;
using Server.Factories.FactoryEnums;
using Server.Model.Behaviour.Member;

using Server.Camera;

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

        Camera.Camera camera;

        Model.Object.PlayerObject playerObject;

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

            camera = new Camera.Camera(GraphicsDevice.Viewport);

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            world = new Model.Map.World.World("Welt");
            region = RegionFactory.regionFactory.generateRegion(0, "Region", 0, 0, Model.Map.Region.RegionEnum.Grassland, world);

            world.addRegion(region);
            for (int i = 0; i < 50; i++)
            {
                Model.Object.LivingObject var_LivingObject = CreatureFactory.creatureFactory.createNpcObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
                //Logger.Logger.LogDeb("LivingObject wurde erstellt");
                Server.Commands.CommandTypes.WalkRandomCommand command = new Server.Commands.CommandTypes.WalkRandomCommand(var_LivingObject);
                Server.Commands.Executer.Executer.executer.addCommand(command);
                //Server.Commands.CommandTypes.AttackRandomCommand command2 = new Server.Commands.CommandTypes.AttackRandomCommand(var_LivingObject);
                //Server.Commands.Executer.Executer.executer.addCommand(command2);
                var_LivingObject.Position = new Vector3(Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeX * (Model.Map.Block.Block.BlockSize - 1)), Server.Util.Random.GenerateGoodRandomNumber(1, Model.Map.Chunk.Chunk.chunkSizeY * (Model.Map.Block.Block.BlockSize - 1)), 0);
                var_LivingObject.GraphicPath = "Character/Char1_Small";
                var_LivingObject.Scale = 1f;

                var_LivingObject.World = world;
                world.addLivingObject(var_LivingObject);
            }

            Model.Object.PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            var_PlayerObject.Position = new Vector3(0, 0, 0);
            //var_PlayerObject.CollisionBounds.Add(new Rectangle(var_PlayerObject.DrawBounds.Left + 5, var_PlayerObject.DrawBounds.Bottom - 15, var_PlayerObject.DrawBounds.Width - 10, 15));
            var_PlayerObject.GraphicPath = "Character/Char1_Small";
            var_PlayerObject.World = world;
            //var_PlayerObject.Size = new Vector3(32, 48, 0);
            //var_PlayerObject.Scale = 2f;
            //world.addLivingObject(var_PlayerObject);
            world.addPlayerObject(var_PlayerObject);
            playerObject = var_PlayerObject;

            camera.setTarget(playerObject);

            Model.Object.EnvironmentObject var_Chest = EnvironmentFactory.environmentFactory.createEnvironmentObject(EnvironmentEnum.Chest);

            var_Chest.Position = new Vector3(650, 200, 0);
            var_Chest.World = world;
            world.addLivingObject(var_Chest);

            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.W }, new Commands.CommandTypes.WalkUpCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.S }, new Commands.CommandTypes.WalkDownCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.A }, new Commands.CommandTypes.WalkLeftCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.D }, new Commands.CommandTypes.WalkRightCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.Space }, new Commands.CommandTypes.AttackWithWeaponCommand(var_PlayerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.E }, new Commands.CommandTypes.InteractCommand(var_PlayerObject)));


            watch.Stop();
            Util.Serializer.SerializeObject("world.obj", world);
            Logger.Logger.LogInfo("Gr��e der World: " + new System.IO.FileInfo("world.obj").Length / 1000 + "KB");
            Model.Map.World.World world2 = (Model.Map.World.World)Util.Serializer.DeSerializeObject("world.obj");
            //Logger.Logger.LogDeb("Time spent: " + watch.Elapsed);

            //Util.MapHandler var_MapHandler = new Util.MapHandler(40, 20, 35);
            //var_MapHandler.PrintMap();

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
            camera.update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (camera.Zoom == 1f)
                {
                    camera.Zoom = 0.1f;
                }
                else
                {
                    camera.Zoom = 1f;
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
                    camera.getMatrix());

            world.drawBlocks(GraphicsDevice, spriteBatch, playerObject);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                    BlendState.AlphaBlend, null, null, null, null,
                    camera.getMatrix());//spriteBatch.Begin();//SpriteSortMode.FrontToBack, BlendState.Opaque);

            world.drawObjects(GraphicsDevice, spriteBatch, playerObject);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0,0), Color.White);
            //spriteBatch.DrawString(Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Units: " + world.QuadTree.Root.quadObjects.ToString(), new Vector2(100, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
