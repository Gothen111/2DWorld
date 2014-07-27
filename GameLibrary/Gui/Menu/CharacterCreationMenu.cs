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

        public CharacterCreationMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

			this.playerNameTextField = new TextField(new Rectangle(200, 100, 289, 85));
			this.playerNameTextField.Text = "Name";
			this.add(this.playerNameTextField);

			this.createCharacterButton = new Button(new Rectangle(200, 300, 289, 85));
			this.createCharacterButton.Text = "Connect";
			this.add(this.createCharacterButton);
			this.createCharacterButton.Action = openCharacterMenu;
        }

		public void openCharacterMenu()
        {
			ContainerManager.containerManager.setMenu(new CharacterMenu());
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
