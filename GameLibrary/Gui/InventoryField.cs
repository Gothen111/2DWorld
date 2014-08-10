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
    public class InventoryField : Container
    {
        private CreatureObject inventoryOwner;

        private int fieldId;

        public int FieldId
        {
            get { return fieldId; }
            set { fieldId = value; }
        }

        //Component itemSpace;

        InventoryItem item;

        public InventoryField(CreatureObject _InventoryOwner, int _FieldId, Rectangle _Bounds)
            :base(_Bounds)
        {
            this.inventoryOwner = _InventoryOwner;

            this.fieldId = _FieldId;

            /*this.itemSpace = new Component(new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height));
            this.itemSpace.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
            this.add(this.itemSpace);*/

            this.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";

            this.item = null;
        }

        public void removeItem()
        {
            this.remove(this.item);
            this.item = null;
        }

        public void setItem(ItemObject _ItemObject)
        {
            this.item = new InventoryItem(new Rectangle(this.Bounds.X + 8, this.Bounds.Y + 8, 16, 16));
            this.item.BackgroundGraphicPath = _ItemObject.GraphicPath;
            this.item.IsTextEditAble = false;
            this.item.Text = _ItemObject.OnStack.ToString();
            this.item.IsDragAndDropAble = true;
            this.item.ItemObject = _ItemObject;
            this.item.ItemObject.PositionInInventory = this.fieldId;

            this.add(this.item);
            this.inventoryOwner.Inventory.InventoryChanged = true;
            //Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.inventoryOwner.Id, this.inventoryOwner.Inventory), GameMessageImportance.VeryImportant));
        }

        private void itemDroped(ItemObject _ItemObject)
        {
            this.inventoryOwner.Inventory.itemDropedInInventory(inventoryOwner, _ItemObject, this.fieldId);
            this.inventoryOwner.Inventory.InventoryChanged = true;
        }

        public override bool componentIsDropedIn(Component _Component)
        {
            base.componentIsDropedIn(_Component);
            if(_Component is InventoryItem)
            {
                if (this.item == null || this.Components.Contains(this.item))
                {
                    //this.setItem(((InventoryItem)_Component).ItemObject);
                    this.itemDroped(((InventoryItem)_Component).ItemObject);
                    return true;
                }
            }
            return false;
        }
    }
}
