using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Object.Inventory
{
    [Serializable()]
    public class Inventory : ISerializable
    {
        private int maxItems;

        public int MaxItems
        {
            get { return maxItems; }
            set { maxItems = value; }
        }

        private List<ItemObject> items;

        public List<ItemObject> Items
        {
            get { return items; }
            set { items = value; }
        }

        private bool inventoryChanged;

        public bool InventoryChanged
        {
            get { return inventoryChanged; }
            set { inventoryChanged = value; }
        }

        public Inventory()
        {
            this.maxItems = 10;
            this.items = new List<ItemObject>();
            this.inventoryChanged = true;
        }

        public Inventory(SerializationInfo info, StreamingContext ctxt)
            :base()
        {
            this.maxItems = (int)info.GetValue("maxItems", typeof(int));
            this.items = (List<ItemObject>)info.GetValue("items", typeof(List<ItemObject>));
            this.inventoryChanged = (bool)info.GetValue("inventoryChanged", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("maxItems", maxItems, typeof(int));
            info.AddValue("items", items, typeof(List<ItemObject>));
            info.AddValue("inventoryChanged", inventoryChanged, typeof(bool));
        }

        public bool addItemObjectToInventory(ItemObject _ItemObject)
        {
            ItemObject var_ItemObject = getItemObjectEqual(_ItemObject);
            if(var_ItemObject!=null)
            {
                if (addItemObjectToItemStack(var_ItemObject))
                {
                    GameLibrary.Model.Map.World.World.world.removeObjectFromWorld(_ItemObject);
                    this.inventoryChanged = true;
                    return true;
                }
                else
                {
                    //Nehme Item nicht auf usw .... 
                    return false;
                }
            }
            else
            {
                if (this.isInventoryFull())
                {
                    return false;
                }
                else
                {
                    this.items.Add(_ItemObject);
                    GameLibrary.Model.Map.World.World.world.removeObjectFromWorld(_ItemObject);
                    this.inventoryChanged = true;
                    return true;
                }
            }
        }

        public ItemObject getItemObjectEqual(ItemObject _ItemObject)
        {
            foreach (ItemObject var_ItemObject in this.items)
            {
                if (var_ItemObject.ItemEnum == _ItemObject.ItemEnum)
                {
                    if (var_ItemObject.OnStack + _ItemObject.OnStack < var_ItemObject.StackMax)
                    {
                        return var_ItemObject;
                    }
                }
            }
            return null;
        }

        public bool isInventoryFull()
        {
            if (this.items.Count >= this.maxItems)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool addItemObjectToItemStack(ItemObject _ItemObject)
        {
            if (_ItemObject.OnStack < _ItemObject.StackMax)
            {
                _ItemObject.OnStack += _ItemObject.OnStack;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool removeItemObjectFromItemStack(ItemObject _ItemObject)
        {
            _ItemObject.OnStack -= 1;
            if (_ItemObject.OnStack <= 0)
            {
                this.items.Remove(_ItemObject);
            }
            return true;
        }
    }
}
