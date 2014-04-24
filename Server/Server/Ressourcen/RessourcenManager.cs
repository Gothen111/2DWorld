using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Server.Ressourcen.Font;

namespace Server.Ressourcen
{
    class RessourcenManager
    {
        public static RessourcenManager ressourcenManager = new RessourcenManager();
        private Dictionary<String, SpriteFont> fonts;
        private Dictionary<String, Texture2D> texture;

        public Dictionary<String, Texture2D> Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Dictionary<String, SpriteFont> Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        public RessourcenManager()
        {
            fonts = new Dictionary<String, SpriteFont>();
            texture = new Dictionary<string, Texture2D>();
        }

        public void loadGeneral(ContentManager _ContentManager)
        {
            addFont("Arial", _ContentManager.Load<SpriteFont>("Font/Arial"));
            addTexture("Wall", _ContentManager.Load<Texture2D>("Block/Wall"));
            addTexture("WoodenPlank", _ContentManager.Load<Texture2D>("Block/WoodenPlank"));
            
            addTexture("Layer1/Gras", _ContentManager.Load<Texture2D>("Block/Layer1/Gras"));
            addTexture("Layer1/Wall", _ContentManager.Load<Texture2D>("Block/Layer1/Wall"));
            addTexture("Layer2/Gras", _ContentManager.Load<Texture2D>("Block/Layer2/Gras"));
            addTexture("Layer2/Dirt", _ContentManager.Load<Texture2D>("Block/Layer2/Dirt"));

            addTexture("Environment/Tree/Tree1", _ContentManager.Load<Texture2D>("Block/Environment/Tree/Tree1"));

            addTexture("Character/Char1_Small", _ContentManager.Load<Texture2D>("Object/Character/Char1_Small"));
            addTexture("Character/Char1_Small_Attack", _ContentManager.Load<Texture2D>("Object/Character/Char1_Small_Attack"));
            addTexture("Character/Lifebar", _ContentManager.Load<Texture2D>("Object/Character/Lifebar"));
            addTexture("Character/Shadow", _ContentManager.Load<Texture2D>("Object/Character/Shadow"));
        }

        public void addTexture(String _Name, Texture2D _Texture2D)
        {
            if (!this.containTextue(_Name, _Texture2D))
            {
                this.texture.Add(_Name, _Texture2D);
            }
        }

        public bool containTextue(String _Name, Texture2D _Texture2D)
        {
            return texture.ContainsKey(_Name) || texture.ContainsValue(_Texture2D);
        }

        public void addFont(String _Name, SpriteFont _SpiteFont)
        {
            if(!this.containFont(_Name, _SpiteFont))
            {
                this.fonts.Add(_Name, _SpiteFont);
            }
        }

        public bool containFont(String _Name, SpriteFont _SpiteFont)
        {
            return fonts.ContainsKey(_Name) || fonts.ContainsValue(_SpiteFont);
        }
    }
}
