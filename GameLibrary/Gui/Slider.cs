using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UserInterface.MouseEnum;

namespace GameLibrary.Gui
{
    public class Slider : TextField
    {
        private float value;

        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private float maxValue;

        public float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public Slider()
            : base()
        {
            this.TextAlign = TextAlign.Center;
        }

        public Slider(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.TextAlign = TextAlign.Center;
        }

        public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);

            float var_Value = _MousePosition.X - this.Bounds.X;
            this.value = var_Value;
        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _TextShiftPosition)
        {
            base.draw(_GraphicsDevice, _SpriteBatch, _TextShiftPosition + new Vector2(0, -50));
        }
    }
}
