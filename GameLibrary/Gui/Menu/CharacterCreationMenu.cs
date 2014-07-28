using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Gui;

namespace GameLibrary.Gui.Menu
{
    public class CharacterCreationMenu : Container
    {
		TextField playerNameTextField;
		Button createCharacterButton;
        Component plattformComponent;
        Component characterComponent;

        public CharacterCreationMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

            this.plattformComponent = new Component(new Rectangle(290, 200, 230, 70));
            this.plattformComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Plattform";
            this.add(this.plattformComponent);

            this.characterComponent = new Component(new Rectangle(320, 50, 170, 190));
            this.characterComponent.BackgroundGraphicPath = "Character/Char1_Big";
            this.add(this.characterComponent);

			this.playerNameTextField = new TextField(new Rectangle(260, 280, 289, 85));
			this.playerNameTextField.Text = "Name";
			this.add(this.playerNameTextField);

			this.createCharacterButton = new Button(new Rectangle(260, 380, 289, 85));
			this.createCharacterButton.Text = "Accept";
			this.add(this.createCharacterButton);
			this.createCharacterButton.Action = openCharacterMenu;
        }

		public void openCharacterMenu()
        {
            MenuManager.menuManager.setMenu(new CharacterMenu());
        }

        public override List<Component> releaseComponents()
        {
            List<Component> var_Components = base.releaseComponents();
            var_Components.Add(this.plattformComponent);
            var_Components.Add(this.characterComponent);
            var_Components.Add(this.playerNameTextField);
            var_Components.Add(this.createCharacterButton);
            return var_Components;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
