using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    public class TextField : Component
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        private Color foreGroundColor;

        public Color ForeGroundColor
        {
            get { return foreGroundColor; }
            set { foreGroundColor = value; }
        }

        public TextField()
            : base()
        {
            this.text = "";
            foreGroundColor = Color.Black;
        }

        public TextField(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.text = "";
            foreGroundColor = Color.Black;
        }

        public override void keyboardButtonClicked(Microsoft.Xna.Framework.Input.Keys buttonPressed)
        {
            base.keyboardButtonClicked(buttonPressed);
            if (this.IsFocused)
            {
                if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.Back)
                {
                    if(this.text.Length > 0)
                        this.text = this.text.Substring(0, this.text.Length - 1);
                }
                else if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftShift || buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftAlt)
                {

                }
                else
                {
                    this.text += Peripherals.KeyboardManager.TryConvertKey(buttonPressed);
                }
            }
        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);
            SpriteFont font = Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"];
            _SpriteBatch.DrawString(font, this.text, new Vector2(this.Bounds.X + 20, this.Bounds.Y + this.Bounds.Height/3), this.foreGroundColor);
        }
    }
}
