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
    public class CharacterCreationMenu : Container
    {
		TextField playerNameTextField;
		Button createCharacterButton;
        Component plattformComponent;
        Component characterComponent;

        Container bodyColorPicker;

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


            this.bodyColorPicker = new Container(new Rectangle(500, 50, 170, 190));
            this.createColors();
            this.add(this.bodyColorPicker);

			this.playerNameTextField = new TextField(new Rectangle(260, 280, 289, 85));
			this.playerNameTextField.Text = "Name";
			this.add(this.playerNameTextField);

			this.createCharacterButton = new Button(new Rectangle(260, 380, 289, 85));
			this.createCharacterButton.Text = "Accept";
			this.add(this.createCharacterButton);
            this.createCharacterButton.Action = acceptCharacter;
        }

        private void createColors()
        {
            int y = 0;
            for (int x = 0; x < 10; x++)
            {
                for (; y < 5; y++)
                {
                    Component var_ColorComponent = new Component(new Rectangle(this.bodyColorPicker.Bounds.X + x * 16 + (y%2)*8, this.bodyColorPicker.Bounds.Y + y * 13, 16, 17));
                    var_ColorComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/ColorField";
                    var_ColorComponent.ComponentColor = new Color(y * 25, x * 25, x * 25);
                    this.bodyColorPicker.add(var_ColorComponent);
                }
                for (; y >=5 && y < 10; y++)
                {
                    Component var_ColorComponent = new Component(new Rectangle(this.bodyColorPicker.Bounds.X + x * 16 + (y % 2) * 8, this.bodyColorPicker.Bounds.Y + y * 13, 16, 17));
                    var_ColorComponent.BackgroundGraphicPath = "Gui/Menu/CharacterCreation/ColorField";
                    var_ColorComponent.ComponentColor = new Color(255 - y * 10, 255 - x * 25, x * 25);
                    this.bodyColorPicker.add(var_ColorComponent);
                }
                y = 0;
            }
        }

        private void openCharacterMenu()
        {
            MenuManager.menuManager.setMenu(new CharacterMenu());
        }

        private void acceptCharacter()
        {
            bool var_CreationProblem = false;

            String var_Path = "Save/Characters/" + this.playerNameTextField.Text + ".sav";

            if (File.Exists(var_Path))
            {
                var_CreationProblem = true;
            }
            if(!var_CreationProblem)
            {
                PlayerObject var_PlayerObject = Factory.CreatureFactory.creatureFactory.createPlayerObject(Factory.FactoryEnums.RaceEnum.Human, Factory.FactoryEnums.FactionEnum.Beerdrinker, Factory.FactoryEnums.CreatureEnum.Farmer, Factory.FactoryEnums.GenderEnum.Male);
                var_PlayerObject.Name = this.playerNameTextField.Text;
                Util.Serializer.SerializeObject(var_Path, var_PlayerObject);
                this.openCharacterMenu();
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
