using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameLibrary.Gui;
using GameLibrary.Model.Object;

namespace GameLibrary.Gui
{
    public class InventoryItem : TextField
    {
        private ItemObject itemObject;

        public ItemObject ItemObject
        {
            get { return itemObject; }
            set { itemObject = value; }
        }

        public InventoryItem(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.itemObject = null;
        }
    }
}
