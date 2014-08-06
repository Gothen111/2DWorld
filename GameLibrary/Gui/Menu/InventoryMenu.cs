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

namespace GameLibrary.Gui.Menu
{
    public class InventoryMenu : Container
    {
        CreatureObject inventoryOwner;

        Container equipmentContainer;
        TextField weaponComponent;
        TextField armorComponent;

        Container itemContainer;

        public InventoryMenu(CreatureObject _InventoryOwner)
            :base()
        {
            this.inventoryOwner = _InventoryOwner;

            this.Bounds = new Rectangle(500, 0, 700, 1000); // TODO: Größe an Bildschirm anpassen!

            this.AllowMultipleFocus = true;

            this.equipmentContainer = new Container(this.Bounds);

            Component var_ItemSpaceWeapon = new Component(new Rectangle(this.Bounds.X, 0, 36, 36));
            var_ItemSpaceWeapon.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
            this.equipmentContainer.add(var_ItemSpaceWeapon);

            this.weaponComponent = new TextField(new Rectangle(this.Bounds.X, 0, 36, 36));
            //this.equipmentContainer.add(this.weaponComponent);

            Component var_ItemSpaceArmor = new Component(new Rectangle(this.Bounds.X, 36, 36, 36));
            var_ItemSpaceArmor.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
            this.equipmentContainer.add(var_ItemSpaceArmor);

            this.armorComponent = new TextField(new Rectangle(this.Bounds.X, 36, 36, 36));
            //this.equipmentContainer.add(this.armorComponent);

            int var_BackbackSize = this.inventoryOwner.Inventory.MaxItems;

            int var_SizeY = var_BackbackSize / 4 + var_BackbackSize % 4;

            for (int y = 0; y < var_SizeY; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (var_BackbackSize > 0)
                    {
                        Component var_ItemSpace = new Component(new Rectangle(this.Bounds.X + 36*x, 100 + y*36,36,36));
                        var_ItemSpace.BackgroundGraphicPath = "Gui/Menu/Inventory/InventoryItemSpace";
                        this.add(var_ItemSpace);

                        var_BackbackSize -= 1;
                    }
                }
            }
            this.itemContainer = new Container(this.Bounds);
            this.checkItems();
            this.add(this.itemContainer);

            this.add(this.equipmentContainer);
        }

        public void checkItems()
        {
            this.checkInventoryItems();
            this.checkEquipmentItems();
            //TODO: Das gefällt mir ganz und gar nciht mit dem InventoryChanged = false; hier. es muss noch ne exta varialbe hier geben und das chang ein creatue geupdated ...
            this.inventoryOwner.Inventory.InventoryChanged = false;         
        }

        private void checkInventoryItems()
        {
            if (this.inventoryOwner.Inventory.InventoryChanged)
            {
                this.itemContainer.clear();

                int var_BackbackSize = this.inventoryOwner.Inventory.MaxItems;

                int var_SizeY = var_BackbackSize / 4 + var_BackbackSize % 4;

                for (int y = 0; y < var_SizeY; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (var_BackbackSize > 0)
                        {
                            int var_ItemId = y * 4 + x;
                            if (this.inventoryOwner.Inventory.Items.Count > var_ItemId)
                            {
                                TextField var_Item = new TextField(new Rectangle(this.Bounds.X + 36 * x + 8, 100 + y * 36 + 8, 16, 16));
                                var_Item.BackgroundGraphicPath = this.inventoryOwner.Inventory.Items[var_ItemId].GraphicPath;
                                var_Item.IsTextEditAble = false;
                                var_Item.Text = this.inventoryOwner.Inventory.Items[var_ItemId].OnStack.ToString();
                                var_Item.IsDragAndDropAble = true;
                                this.itemContainer.add(var_Item);
                            }
                            var_BackbackSize -= 1;
                        }
                    }
                }
            }
        }

        private void checkEquipmentItems()
        {
            if (this.inventoryOwner.Inventory.InventoryChanged)
            {
                this.equipmentContainer.remove(this.weaponComponent);
                if (this.inventoryOwner.getWeaponInHand() != null)
                {
                    this.weaponComponent.BackgroundGraphicPath = "Object/Item/Small/Sword1";
                    this.weaponComponent.IsTextEditAble = false;
                    this.weaponComponent.IsDragAndDropAble = true;
                    this.equipmentContainer.add(this.weaponComponent);
                }

                this.equipmentContainer.remove(this.armorComponent);
                if (this.inventoryOwner.getWearingArmor() != null)
                {
                    this.armorComponent.BackgroundGraphicPath = "Object/Item/Small/Cloth1";
                    this.armorComponent.IsTextEditAble = false;
                    this.armorComponent.IsDragAndDropAble = true;
                    this.equipmentContainer.add(this.armorComponent);
                }
            }
        }
    }
}
