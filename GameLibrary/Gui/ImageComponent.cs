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

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);
            if (this.backgroundGraphicPath != null && !this.backgroundGraphicPath.Equals(""))
            {
                if (!this.IsHovered)
                {
                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                }
                else
                {
                    try
                    {
                        _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Hover"], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                    }
                    catch (Exception e)
                    {
                        _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                    }
                }
            }
        }
    }
}
