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

namespace GameLibrary.Gui.Menu
{
    public class InventoryMenu : Container
    {
        Container itemContainer;

        public InventoryMenu()
            :base()
        {
            this.Bounds = new Rectangle(500, 0, 700, 1000); // TODO: Größe an Bildschirm anpassen!

            this.AllowMultipleFocus = true;

            int var_BackbackSize = Connection.NetworkManager.client.PlayerObject.Inventory.MaxItems;

            int var_SizeY = var_BackbackSize / 4 + var_BackbackSize % 4;

            for (int y = 0; y < var_SizeY; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (var_BackbackSize > 0)
                    {
                        Component var_ItemSpace = new Component(new Rectangle(this.Bounds.X + 36*x, y*36,36,36));
                        var_ItemSpace.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
                        this.add(var_ItemSpace);

                        var_BackbackSize -= 1;
                    }
                }
            }
            this.itemContainer = new Container(this.Bounds);
            this.checkInventoryItems();

            this.add(this.itemContainer);
        }

        public void checkInventoryItems()
        {
            this.itemContainer.clear();

            int var_BackbackSize = Connection.NetworkManager.client.PlayerObject.Inventory.MaxItems;

            int var_SizeY = var_BackbackSize / 4 + var_BackbackSize % 4;

            for (int y = 0; y < var_SizeY; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (var_BackbackSize > 0)
                    {
                        int var_ItemId = y * 4 + x;
                        if (Connection.NetworkManager.client.PlayerObject.Inventory.Items.Count > var_ItemId)
                        {
                            TextField var_Item = new TextField(new Rectangle(this.Bounds.X + 36 * x + 8, y * 36 + 8, 16, 16));
                            var_Item.BackgroundGraphicPath = Connection.NetworkManager.client.PlayerObject.Inventory.Items[var_ItemId].GraphicPath;
                            var_Item.IsTextEditAble = false;
                            var_Item.Text = Connection.NetworkManager.client.PlayerObject.Inventory.Items[var_ItemId].OnStack.ToString();
                            this.itemContainer.add(var_Item);
                        }
                        var_BackbackSize -= 1;
                    }
                }
            }
        }
    }
}
