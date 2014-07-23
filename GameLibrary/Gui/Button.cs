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
        }

        public Button(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.TextAlign = TextAlign.Center;
        }

        public void StartAction()
        {
            if (this.action != null)
            {
                this.action();
            }
        }

        public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton)
        {
            base.onClick(mouseButton);
            this.StartAction();
        }
    }
}
