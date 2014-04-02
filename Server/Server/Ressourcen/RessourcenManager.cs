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
        Dictionary<String, SpriteFont> fonts;

        public RessourcenManager()
        {
            fonts = new Dictionary<String, SpriteFont>();
        }

        public void loadGeneral(ContentManager _ContentManager)
        {
            addFont("Arial", _ContentManager.Load<SpriteFont>("Font/Arial"));
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
