using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UserInterface.MouseEnum;

namespace GameLibrary.Gui
{
    public class Checkbox : Component
    {
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public Checkbox()
            : base()
        {
            this.isChecked = false;
        }

        public Checkbox(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.isChecked = false;
        }

        public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 _MousePosition)
        {
            base.onClick(mouseButton, _MousePosition);
            if (this.isChecked)
            {
                this.isChecked = false;
            }
            else
            {
                this.isChecked = true;
            }
        }
    }
}
