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

        public Inventory()
        {
            this.maxItems = 10;
            this.items = new List<ItemObject>();
        }

        public Inventory(SerializationInfo info, StreamingContext ctxt)
        {
            this.maxItems = (int)info.GetValue("maxItems", typeof(int));
            this.items = (List<ItemObject>)info.GetValue("items", typeof(List<ItemObject>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("maxItems", maxItems, typeof(int));
            info.AddValue("items", items, typeof(List<ItemObject>));
        }

        public void addItemObjectToInventory(ItemObject _ItemObject)
        {
            if (this.containsItemObject(_ItemObject))
            {
                ItemObject var_ItemObject = getItemObjectEqual(_ItemObject);
                if (addItemObjectToItemStack(var_ItemObject))
                {
                }
                else
                {
                    //TODO: Nehme Item nicht auf usw .... 
                }
            }
            else
            {
                if (this.isInventoryFull())
                {
                }
                else
                {
                    this.items.Add(_ItemObject);
                    GameLibrary.Model.Map.World.World.world.removeObjectFromWorld(_ItemObject);
                }
            }
        }

        public bool containsItemObject(ItemObject _ItemObject)
        {
            //TODO: Gucke ob Item enthalten
            return false;//items.Contains(_ItemObject);
        }

        public ItemObject getItemObjectEqual(ItemObject _ItemObject)
        {
            //TODO: Gebe gleiches Item anhand von x werten zurück
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
                _ItemObject.OnStack += 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool removeItemObjectToItemStack(ItemObject _ItemObject)
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
