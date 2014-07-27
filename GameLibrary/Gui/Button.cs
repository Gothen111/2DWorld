using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UserInterface.MouseEnum;

namespace GameLibrary.Gui
{
    public class Button : TextField
    {
        private Action action;

        public Action Action
        {
            get { return action; }
            set { action = value; }
        }

        public Button()
            : base()
        {
            this.TextAlign = TextAlign.Center;
			this.BackgroundGraphicPath = "Gui/Button";
        }

        public Button(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.TextAlign = TextAlign.Center;
			this.BackgroundGraphicPath = "Gui/Button";
        }

        public void StartAction()
        {
            if (this.action != null)
            {
                this.action();
            }
        }

        public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            this.StartAction();
        }
    }
}
