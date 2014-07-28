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

			this.charactersListView = new ListView (new Rectangle (500, 0, 289, 600));
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
            TextField var_TextField1 = new TextField(new Rectangle(0, 0, 289, 85));
            var_TextField1.IsTextEditAble = false;
            var_TextField1.Text = "2";
            this.charactersListView.addAtBottom(var_TextField1);

            TextField var_TextField2 = new TextField(new Rectangle(0, 0, 289, 85));
            var_TextField2.IsTextEditAble = false;
            var_TextField2.Text = "1";
            this.charactersListView.addAtTop(var_TextField2);
            
		}

		public void openCreateCharacterMenu()
		{
            MenuManager.menuManager.setMenu(new CharacterCreationMenu());
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
                MenuManager.menuManager.setMenu(new ConnectToServerMenu());
			}
		}

        public override List<Component> releaseComponents()
        {
            List<Component> var_Components = base.releaseComponents();
            var_Components.Add(this.charactersListView);
            var_Components.Add(this.createNewCharacterButton);
            var_Components.Add(this.connectToServerButton);
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
