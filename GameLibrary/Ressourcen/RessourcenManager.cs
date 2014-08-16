using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GameLibrary.Ressourcen.Font;

namespace GameLibrary.Ressourcen
{
    public class RessourcenManager
    {
        public static RessourcenManager ressourcenManager = new RessourcenManager();

        private Dictionary<String, SpriteFont> fonts;

        public Dictionary<String, SpriteFont> Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        private Dictionary<String, Texture2D> texture;

        public Dictionary<String, Texture2D> Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private int loadingErrorsCount;

        public RessourcenManager()
        {
            fonts = new Dictionary<String, SpriteFont>();
            texture = new Dictionary<string, Texture2D>();
            this.loadingErrorsCount = 0;
        }

        public void loadGeneral(ContentManager _ContentManager)
        {
            Logger.Logger.LogInfo("Lade Ressource ...");

            addFont("Arial", _ContentManager.Load<SpriteFont>("Font/Arial"));

            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Ground1", "Region/Grassland/Block/Layer1/Gras");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Wall", "Region/Grassland/Block/Layer1/Wall");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer2/Ground1", "Region/Grassland/Block/Layer2/Gras");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer2/Ground2", "Region/Grassland/Block/Layer2/Dirt");

            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Center", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Center");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner1", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner2", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner2");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner3", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner3");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Corner4", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Corner4");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Left", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Left");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Right", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Right");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Top", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Top");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_Bottom", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_Bottom");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner1", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner2", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner2");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner3", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner3");
            loadTexture(_ContentManager, "Region/Grassland/Block/Layer1/Hill1_InsideCorner4", "Region/Grassland/Block/Layer1/Hill1/GrasHill1_InsideCorner4");

            loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Tree/Tree1", "Region/Grassland/Block/Environment/Tree/Tree1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Tree/Tree1_Dead", "Region/Grassland/Block/Environment/Tree/Tree1_Dead");
            loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Flower/Flower1", "Region/Grassland/Block/Environment/Flower/Flower1");
            loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Chest/Chest", "Region/Grassland/Block/Environment/Chest/Chest");
            loadTexture(_ContentManager, "Region/Grassland/Block/Environment/Farm/FarmHouse1", "Region/Grassland/Block/Environment/Farm/FarmHouse1");

            loadTexture(_ContentManager, "Region/Snowland/Block/Layer1/Ground1", "Region/Snowland/Block/Layer1/Snow");
            loadTexture(_ContentManager, "Region/Snowland/Block/Layer2/Ground1", "Region/Snowland/Block/Layer2/Gras");
            loadTexture(_ContentManager, "Region/Snowland/Block/Layer2/Ground2", "Region/Snowland/Block/Layer2/Dirt");

            loadTexture(_ContentManager, "Region/Snowland/Block/Environment/Tree/Tree1", "Region/Snowland/Block/Environment/Tree/Tree1");


            loadTexture(_ContentManager, "Character/Char1_Small", "Object/Character/Char1_Small");
            loadTexture(_ContentManager, "Character/Char1_Small_Attack", "Object/Character/Char1_Small_Attack");

            loadTexture(_ContentManager, "Character/Ogre1", "Object/Character/Ogre1");

            loadTexture(_ContentManager, "Character/Lifebar", "Object/Character/Lifebar");

            loadTexture(_ContentManager, "Character/Shadow", "Object/Character/Shadow");
            loadTexture(_ContentManager, "Character/CreatureState", "Object/Character/CreatureState");

            loadTexture(_ContentManager, "Character/Cloth1", "Object/Character/Cloth1");

            loadTexture(_ContentManager, "Character/GoldCoin", "Object/Character/GoldCoin");

            loadTexture(_ContentManager, "Gui/Button", "Gui/Button");
            loadTexture(_ContentManager, "Gui/Button_Hover", "Gui/Button_Hover");
            loadTexture(_ContentManager, "Gui/Button_Pressed", "Gui/Button_Pressed");
            loadTexture(_ContentManager, "Gui/TextField", "Gui/TextField");
            loadTexture(_ContentManager, "Gui/Background", "Gui/Background");

            loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/Background", "Gui/Menu/CharacterCreation/Background");
            loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/Plattform", "Gui/Menu/CharacterCreation/Plattform");
            loadTexture(_ContentManager, "Gui/Menu/CharacterCreation/ColorField", "Gui/Menu/CharacterCreation/ColorField");

            loadTexture(_ContentManager, "Gui/Menu/GameSurface/Interface", "Gui/Menu/GameSurface/Interface");
            loadTexture(_ContentManager, "Gui/Menu/GameSurface/Health", "Gui/Menu/GameSurface/Health");
            loadTexture(_ContentManager, "Gui/Menu/GameSurface/Mana", "Gui/Menu/GameSurface/Mana");

            loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryItemSpace", "Gui/Menu/Inventory/InventoryItemSpace");
            loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryButton", "Gui/Menu/Inventory/InventoryButton");
            loadTexture(_ContentManager, "Gui/Menu/Inventory/InventoryMenu", "Gui/Menu/Inventory/InventoryMenu");

            loadTexture(_ContentManager, "Character/Char1_Big", "Object/Character/Char1_Big");

            loadTexture(_ContentManager, "Object/Item/Small/Cloth1", "Object/Item/Small/Cloth1");
            loadTexture(_ContentManager, "Object/Item/Small/Sword1", "Object/Item/Small/Sword1");

            Logger.Logger.LogInfo("Ressourcen wurden mit " + this.loadingErrorsCount + " Fehler(n) geladen");
        }

        public bool containsTextue(String _Name, Texture2D _Texture2D)
        {
            return texture.ContainsKey(_Name) || texture.ContainsValue(_Texture2D);
        }

        public void addTexture(String _Name, Texture2D _Texture2D)
        {
            if (!this.containsTextue(_Name, _Texture2D))
            {
                this.texture.Add(_Name, _Texture2D);
            }
        }

        public void loadTexture(ContentManager _ContentManager, String _Name, String _Texture2DPath)
        {
            try
            {
                this.addTexture(_Name, _ContentManager.Load<Texture2D>(_Texture2DPath));
                Logger.Logger.LogInfo("RessourcenManager->loadTexture(...) : " + _Texture2DPath + " wurde geladen!");
            }
            catch (Exception e)
            {
                Logger.Logger.LogErr("RessourcenManager->loadTexture(...) : " + _Texture2DPath + " konnte nicht gefunden werden!");
                this.loadingErrorsCount += 1;
            }
        }

        public bool containsFont(String _Name, SpriteFont _SpiteFont)
        {
            return fonts.ContainsKey(_Name) || fonts.ContainsValue(_SpiteFont);
        }

        public void addFont(String _Name, SpriteFont _SpiteFont)
        {
            if(!this.containsFont(_Name, _SpiteFont))
            {
                this.fonts.Add(_Name, _SpiteFont);
            }
        }
    }
}
