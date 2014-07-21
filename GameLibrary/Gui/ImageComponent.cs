using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    class ImageComponent : Component
    {
        private String backgroundGraphicPath;

        public String BackgroundGraphicPath
        {
            get { return backgroundGraphicPath; }
            set { backgroundGraphicPath = value; }
        }

        public ImageComponent()
            :base()
        {

        }

        public ImageComponent(Rectangle _Bounds)
            :base(_Bounds)
        {

        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
            if (this.backgroundGraphicPath != null && !this.backgroundGraphicPath.Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
            }
        }
    }
}
