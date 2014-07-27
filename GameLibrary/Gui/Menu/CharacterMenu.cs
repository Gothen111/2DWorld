using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Gui;

namespace GameLibrary.Gui.Menu
{
	public class CharacterMenu : Container
    {
		ListView charactersListView;
		Button createNewCharacterButton;
		Button connectToServerButton;

		public CharacterMenu()
            :base()
        {
            this.Bounds = new Rectangle(0,0,1000,1000); // TODO: Größe an Bildschirm anpassen!
            this.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/Background";

            this.AllowMultipleFocus = true;

			this.charactersListView = new ListView (new Rectangle (300, 0, 289, 600));
			this.add(this.charactersListView);
			this.fillCharactersListView();

			this.createNewCharacterButton = new Button(new Rectangle(200, 200, 289, 85));
			this.createNewCharacterButton.Text = "Create Character";
			this.add(this.createNewCharacterButton);
			this.createNewCharacterButton.Action = openCreateCharacterMenu;

			this.connectToServerButton = new Button(new Rectangle(200, 300, 289, 85));
			this.connectToServerButton.Text = "Connect";
			this.add(this.connectToServerButton);
			this.connectToServerButton.Action = openConnectToServerMenu;
		}

		//TODO: Lade Charactere aus Datei oä. und füge sie der Liste hinzu.
		private void fillCharactersListView()
		{

		}

		public void openCreateCharacterMenu()
		{
			ContainerManager.containerManager.setMenu(new CharacterCreationMenu());
		}

		//TODO: Character noch "entnehmen", bzw als spieler character festlegen
		private bool characterHasBeenChoosen()
		{
			return this.charactersListView.getSelectedComponent () != null ? true : false;
		}

		public void openConnectToServerMenu()
		{
			if (this.characterHasBeenChoosen()) 
			{
				ContainerManager.containerManager.setMenu (new ConnectToServerMenu ());
			}
		}

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            base.draw(_GraphicsDevice, _SpriteBatch);
            _SpriteBatch.End();
        }
    }
}
