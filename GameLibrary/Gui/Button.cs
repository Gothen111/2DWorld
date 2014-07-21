using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    public class Button : Component
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        private Action action;

        public Action Action
        {
            get { return action; }
            set { action = value; }
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
