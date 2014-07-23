using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    public enum TextAlign
    {
        Left,
        Center,
        Right
    }

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

        private TextAlign textAlign;

        public TextAlign TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; }
        }

        public TextField()
            : base()
        {
            this.text = "";
            foreGroundColor = Color.Black;
            this.textAlign = TextAlign.Left;
        }

        public TextField(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.text = "";
            foreGroundColor = Color.Black;
            this.textAlign = TextAlign.Left;
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
            this.draw(_GraphicsDevice, _SpriteBatch, Vector2.Zero);
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _TextShiftPosition)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);
            SpriteFont font = Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"];
            if (this.textAlign == TextAlign.Left)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + 20, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
            else if (this.textAlign == TextAlign.Center)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + this.Bounds.Width / 2 - font.MeasureString(this.text).X / 2, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
            else if (this.textAlign == TextAlign.Right)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + this.Bounds.Width - 20 - font.MeasureString(this.text).X, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
        }
    }
}
