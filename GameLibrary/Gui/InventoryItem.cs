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
using GameLibrary.Connection;

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

        private Container parent;

        private CreatureObject inventoryOwner;

        public InventoryItem(Rectangle _Bounds, Container _Parent, CreatureObject _InventoryOwner)
            : base(_Bounds)
        {
            this.itemObject = null;
            this.parent = _Parent;
            this.inventoryOwner = _InventoryOwner;
        }

        /*public override void onDrag(Vector2 _Position)
        {
            base.onDrag(_Position);
            if (this.parent is InventoryField)
            {
                ((InventoryField)this.parent).removeItem();
            }
            if (this.parent is EquipmentField)
            {
                ((EquipmentField)this.parent).removeItem();
            }
            MenuManager.menuManager.ActiveContainer.add(this);
            Event.EventList.Add(new Event(new GameLibrary.Connection.Message.CreatureInventoryItemPositionChangeMessage(this.inventoryOwner.Id, itemObject.PositionInInventory, -2), GameMessageImportance.VeryImportant));
        }

        public override bool onDrop(Vector2 _Position)
        {
            //MenuManager.menuManager.ActiveContainer.remove(this);


            return base.onDrop(_Position);
        }*/
    }
}
