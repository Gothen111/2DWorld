using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Gui
{
    class Healthbar : Component
    {
        public Healthbar(Rectangle bounds)
            : base(bounds)
        {

        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            float percentageLife = Configuration.Configuration.networkManager.client.PlayerObject.HealthPoints / Configuration.Configuration.networkManager.client.PlayerObject.MaxHealthPoints;
            Rectangle source = new Rectangle(0, (int)((1-percentageLife) * this.Bounds.Height), this.Bounds.Width, (int)(this.Bounds.Height * percentageLife));
            Rectangle destination = new Rectangle(this.Bounds.X, (int)(this.Bounds.Y + (1 - percentageLife) * this.Bounds.Height), this.Bounds.Width, (int)(this.Bounds.Height * percentageLife));
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.BackgroundGraphicPath], destination, source , this.ComponentColor);            
        }
    }
}
