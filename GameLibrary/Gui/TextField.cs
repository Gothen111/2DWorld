using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    class TextField : ImageComponent
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public TextField()
            : base()
        {
            this.text = "";
        }

        public TextField(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.text = "";
        }

        public override void keyboardButtonClicked(Microsoft.Xna.Framework.Input.Keys buttonPressed)
        {
            base.keyboardButtonClicked(buttonPressed);
            if (this.IsFocused)
            {
                if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.Back)
                {
                    this.text = this.text.Substring(0, this.text.Length - 1);
                }
                else if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftShift || buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftAlt)
                {

                }
                else
                {
                    if (Peripherals.KeyboardManager.shiftPressed)
                    {
                        this.text += buttonPressed.ToString().ToUpper();
                    }
                    else
                    {
                        this.text += buttonPressed.ToString().ToLower();
                    }
                }
            }
        }
    }
}
