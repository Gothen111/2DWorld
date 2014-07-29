using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Microsoft.Xna.Framework;

using GameLibrary.Gui;
using GameLibrary.Model.Object;

namespace GameLibrary.Gui.Menu
{
	public class CharacterMenu : Container
    {
		ListView charactersListView;
		Button createNewCharacterButton;
		Button connectToServerButton;

        List<PlayerObject> charactersList;

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
        private void loadCharactersFromFile(List<PlayerObject> _CharactersList)
        {
            String var_Path = "Save/Characters/";

            if (!Directory.Exists(var_Path))
            {
                Directory.CreateDirectory(var_Path);
            }

            String[] var_Names = Directory.GetFiles(var_Path);

            for (int i = 0; i < var_Names.Length; i++)
            {
                PlayerObject var_PlayerObject = (PlayerObject) Util.Serializer.DeSerializeObject(var_Names[i]);
                _CharactersList.Add(var_PlayerObject);
            }
        }

        private void createTextFieldFromCharacter(ListView _CharactersListView, PlayerObject _PlayerObject)
        {
            TextField var_TextField1 = new TextField(new Rectangle(0, 0, 289, 85));
            var_TextField1.IsTextEditAble = false;
            var_TextField1.Text = _PlayerObject.Name;
            _CharactersListView.addAtBottom(var_TextField1);
        }

        private void createTextFields(ListView _CharactersListView, List<PlayerObject> _CharactersList)
        {
            foreach (PlayerObject var_PlayerObject in _CharactersList)
            {
                this.createTextFieldFromCharacter(_CharactersListView, var_PlayerObject);
            }
        }
	
		private void fillCharactersListView()
		{
            this.charactersList = new List<PlayerObject>();

            this.loadCharactersFromFile(this.charactersList);

            this.createTextFields(this.charactersListView, this.charactersList);
		}

		private void openCreateCharacterMenu()
		{
            MenuManager.menuManager.setMenu(new CharacterCreationMenu());
		}

		private bool characterHasBeenChoosen()
		{
			return this.charactersListView.getSelectedComponent() != null ? true : false;
		}

        private PlayerObject getPlayerObjectFromCharactersListView()
        {
            foreach (PlayerObject var_PlayerObject in this.charactersList)
            {
                if (((TextField)this.charactersListView.getSelectedComponent()).Text.Equals(var_PlayerObject.Name))
                {
                    return var_PlayerObject;
                }
            }
            return null;
        }

		private void openConnectToServerMenu()
		{
			if (this.characterHasBeenChoosen()) 
			{
                GameLibrary.Connection.NetworkManager.client.PlayerObject = this.getPlayerObjectFromCharactersListView();
                MenuManager.menuManager.setMenu(new ConnectToServerMenu());
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
