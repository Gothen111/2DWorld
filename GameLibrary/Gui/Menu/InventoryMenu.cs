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
        public InventoryMenu()
            :base()
        {
            this.Bounds = new Rectangle(300, 0, 700, 1000); // TODO: Größe an Bildschirm anpassen!

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

                        //TODO: Das könnte auch in die draw methode noch rein!
                        if (Connection.NetworkManager.client.PlayerObject.Inventory.Items.Count > y * 4 + x * 4)
                        {
                            Component var_Item = new Component(new Rectangle(this.Bounds.X + 36 * x, y * 36, 36, 36));
                            var_Item.BackgroundGraphicPath = "Character/GoldCoin";
                            this.add(var_Item);
                        }

                        var_BackbackSize -= 1;
                    }
                }
            }            
        }
    }
}
