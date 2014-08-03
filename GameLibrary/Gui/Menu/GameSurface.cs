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

using GameLibrary.Gui;

namespace GameLibrary.Gui.Menu
{
    public class GameSurface : Container
    {
        Component interfaceComponent;
        Component healthComponent;
        Component manaComponent;

        InventoryMenu inventoryMenu;

        public GameSurface()
            :base()
        {
            this.Bounds = new Rectangle(0, 0, 1000, 1000); // TODO: Größe an Bildschirm anpassen!

            this.AllowMultipleFocus = true;

            this.healthComponent = new Component(new Rectangle(800 / 2 - 187 / 2 - 284, 500 - 188 - 7, 187, 188));
            this.healthComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Health";
            this.add(this.healthComponent);

            this.manaComponent = new Component(new Rectangle(800 / 2 - 187/2 + 285, 500 - 188 - 6, 187, 188));
            this.manaComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Mana";
            this.add(this.manaComponent);

            this.interfaceComponent = new Component(new Rectangle(800 / 2 - 767/2, 500-201, 767, 201));
            this.interfaceComponent.BackgroundGraphicPath = "Gui/Menu/GameSurface/Interface";
            this.add(this.interfaceComponent);

            this.inventoryMenu = new InventoryMenu();
            this.add(this.inventoryMenu);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend, null, null, null, null,
                    GameLibrary.Camera.Camera.camera.getMatrix());

            if (GameLibrary.Camera.Camera.camera.Target != null)
            {
                GameLibrary.Model.Map.World.World.world.drawBlocks(_GraphicsDevice, _SpriteBatch, GameLibrary.Camera.Camera.camera.Target);
            }
            else
            {
                _SpriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "Dein Charakter ist leider gestorben :(", new Vector2(50, 50), Color.White);
            }

            _SpriteBatch.End();

            _SpriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend, null, null, null, null,
                    GameLibrary.Camera.Camera.camera.getMatrix());//spriteBatch.Begin();//SpriteSortMode.FrontToBack, BlendState.Opaque);

            if (GameLibrary.Camera.Camera.camera.Target != null)
            {
                GameLibrary.Model.Map.World.World.world.drawObjects(_GraphicsDevice, _SpriteBatch, GameLibrary.Camera.Camera.camera.Target);
            }

            _SpriteBatch.End();

            _SpriteBatch.Begin();
            //int var_Diff = (int)((GameLibrary.Connection.NetworkManager.client.PlayerObject.HealthPoints / GameLibrary.Connection.NetworkManager.client.PlayerObject.MaxHealthPoints) * this.healthComponent.Bounds.Height);
            //this.healthComponent.SourceRectangle = new Rectangle(0, this.healthComponent.Bounds.Height - var_Diff, this.healthComponent.Bounds.Width, var_Diff);
            //this.healthComponent.Bounds = new Rectangle(this.healthComponent.Bounds.X, this.healthComponent.Bounds.Y + this.healthComponent.SourceRectangle.Y, this.healthComponent.Bounds.Width, this.healthComponent.Bounds.Height);
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();

        }
    }
}
