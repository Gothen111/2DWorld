using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using GameLibrary.Connection;

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

        private int getFreePlace()
        {
            if(!this.isInventoryFull())
            {
                List<int> var_FreeSpace = new List<int>();
                for (int i = 0; i < this.maxItems; i++)
                {
                    var_FreeSpace.Add(i);
                }
                foreach (ItemObject var_ItemObject in this.items)
                {
                    var_FreeSpace.Remove(var_ItemObject.PositionInInventory);
                }
                return var_FreeSpace[0]; //First
            }
            return -1;
        }

        public bool addItemObjectToInventory(ItemObject _ItemObject)
        {
            ItemObject var_ItemObject = getItemObjectEqual(_ItemObject);
            if(var_ItemObject!=null)
            {
                if (addItemObjectToItemStack(var_ItemObject, _ItemObject))
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
                    _ItemObject.PositionInInventory = this.getFreePlace();
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

        private bool addItemObjectToItemStack(ItemObject _ItemObject, ItemObject _AddItemObject)
        {
            if (_ItemObject.OnStack < _ItemObject.StackMax + _AddItemObject.OnStack)
            {
                _ItemObject.OnStack += _AddItemObject.OnStack;
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

        public void itemDropedInInventory(CreatureObject _InventoryOwner, ItemObject _ItemObject, int _NewPosition)
        {
            if (this.items.Contains(_ItemObject))
            {
                this.changeItemPosition(_InventoryOwner, _ItemObject, _NewPosition);
            }
            else
            {
                //TODO: Kommt wohl von wo anders... anderes Inventar usw..
            }
        }

        //Sendet Änderung zum clienten bzw. server.
        public void changeItemPosition(CreatureObject _InventoryOwner, ItemObject _ItemObject, int _NewPosition)
        {
            int var_OldPosition = _ItemObject.PositionInInventory;
            _ItemObject.PositionInInventory = _NewPosition;
            if (var_OldPosition == _NewPosition)
            {

            }
            else
            {
                //Sende jeweilige Änderung
                if (Configuration.Configuration.isHost)
                {

                }
                else
                {
                    Event.EventList.Add(new Event(new GameLibrary.Connection.Message.CreatureInventoryItemPositionChangeMessage(_InventoryOwner.Id, var_OldPosition, _NewPosition), GameMessageImportance.VeryImportant));
                }
            }
        }

        //Dient nur zum wechseln! Ohne Senden!
        public void changeItemPosition(int _OldPosition, int _NewPosition)
        {
            ItemObject var_ItemToChange = null;

            foreach (ItemObject var_ItemObject in this.items)
            {
                if (var_ItemObject.PositionInInventory == _OldPosition)
                {
                    var_ItemToChange = var_ItemObject;
                    break;
                }
            }

            if (var_ItemToChange != null)
            {
                //TODO: Gucke ob an __NewPosition kein objekt!
                var_ItemToChange.PositionInInventory = _NewPosition;
            }
        }
    }
}
