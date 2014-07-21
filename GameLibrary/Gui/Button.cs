using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    class Button : ImageComponent
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public Button()
            : base()
        {
            this.text = "";
        }

        public Button(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.text = "";
        }

        public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton)
        {
            base.onClick(mouseButton);
        }
    }
}
