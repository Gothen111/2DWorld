using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Connection;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class CreatureObject : LivingObject
    {
        private Inventory.Inventory inventory;

        public Inventory.Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        private List<EquipmentObject> equipment;

        public List<EquipmentObject> Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }
        //protected Armor armor;
        //protected Skill skill;
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public CreatureObject()
        {
            this.equipment = new List<EquipmentObject>();
            this.inventory = new Inventory.Inventory();
        }

        public CreatureObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.equipment = (List<EquipmentObject>)info.GetValue("equipment", typeof(List<EquipmentObject>));
            this.inventory = (Inventory.Inventory)info.GetValue("inventory", typeof(Inventory.Inventory));
            this.name = (String)info.GetValue("name", typeof(String));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("equipment", equipment, typeof(List<EquipmentObject>));
            info.AddValue("inventory", inventory, typeof(Inventory.Inventory));
            info.AddValue("name", this.name, typeof(String));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
            updateEquippment();
        }

        public void updateEquippment()
        {
            if (this.equipment != null)
            {
                foreach (EquipmentObject var_EquipmentObject in this.equipment)
                {
                    if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentWeapon)
                    {
                        ((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).update();
                    }
                }
            }
            else
            {
                this.equipment = new List<EquipmentObject>();
            }
        }

        public void addEquipmentObject(EquipmentObject _EquipmentObject)
        {
            this.equipment.Add(_EquipmentObject);
        }

        public override void attack()
        {
            base.attack();
            this.swingWeapon(Model.Object.Equipment.Attack.AttackType.Front);
        }
        
        public GameLibrary.Model.Object.Equipment.EquipmentWeapon getWeaponInHand()
        {
            GameLibrary.Model.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = null;
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentWeapon)
                {
                    var_EquipmentWeaponForAttack = ((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject);
                    var_EquipmentWeaponForAttack.PositionInInventory = 0;
                }
            }
            return var_EquipmentWeaponForAttack;
        }


        public void swingWeapon(Equipment.Attack.AttackType _AttackType)
        {
            GameLibrary.Model.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = this.getWeaponInHand();
            if (var_EquipmentWeaponForAttack != null && var_EquipmentWeaponForAttack.isAttackReady(_AttackType))
	    {
                List<Object> var_Objects = GameLibrary.Model.Map.World.World.world.getObjectsInRange(this.Position, var_EquipmentWeaponForAttack.getAttack(_AttackType).Range, var_EquipmentWeaponForAttack.SearchFlags);
                var_Objects.Remove(this);
                foreach (Object var_Object in var_Objects)
                {
                    if (var_Object is LivingObject)
                    {
                        this.attackLivingObject((LivingObject)var_Object, var_EquipmentWeaponForAttack.NormalDamage);
                    }
                }
                if (var_Objects.Count > 0)
                {
                    var_EquipmentWeaponForAttack.executeAttack(_AttackType);
                }
            }
        }

        public GameLibrary.Model.Object.Equipment.EquipmentArmor getWearingArmor()
        {
            GameLibrary.Model.Object.Equipment.EquipmentArmor var_EquipmentArmor = null;
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentArmor)
                {
                    var_EquipmentArmor = (GameLibrary.Model.Object.Equipment.EquipmentArmor)var_EquipmentObject;
                    var_EquipmentArmor.PositionInInventory = 1;
                }
            }
            return var_EquipmentArmor;
        }



        public override float calculateDamage(float _DamageAmount)
        {
            if (this.equipment != null)
            {
                GameLibrary.Model.Object.Equipment.EquipmentArmor var_EquipmentArmor = this.getWearingArmor();
                if (var_EquipmentArmor != null)
                {
                    _DamageAmount = _DamageAmount / ((float)((GameLibrary.Model.Object.Equipment.EquipmentArmor)var_EquipmentArmor).NormalArmor);
                }
            }
            return _DamageAmount;
        }

        public void addItemObjectToInventory(ItemObject _ItemObject)
        {
            if(this.inventory.addItemObjectToInventory(_ItemObject))
            {
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
            }
        }

        public void setItemFromEquipmentToInventory(int _EquipmentPosition, int _InventoryPosition)
        {
            EquipmentObject var_ToRemove = null;
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject.PositionInInventory == _EquipmentPosition)
                {
                    if (this.inventory.addItemObjectToInventoryAt(var_EquipmentObject, _InventoryPosition))
                    {
                        var_ToRemove = var_EquipmentObject;
                    }
                }
            }
            if (var_ToRemove != null)
            {
                this.equipment.Remove(var_ToRemove);
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureEquipmentMessage(this.Id, this), GameMessageImportance.VeryImportant));                    
            }
        }

        public void setItemFromInventoryToEquipment(int _InventoryPosition, int _EquipmentPosition)
        {
            ItemObject var_ToRemove = null;
            foreach (ItemObject var_ItemObject in this.inventory.Items)
            {
                if (var_ItemObject.PositionInInventory == _InventoryPosition)
                {
                    if (var_ItemObject is EquipmentObject)
                    {
                        this.equipment.Add((EquipmentObject)var_ItemObject);
                        var_ToRemove = var_ItemObject;
                    }
                }
            }
            if (var_ToRemove != null)
            {
                this.inventory.Items.Remove(var_ToRemove);
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureEquipmentMessage(this.Id, this), GameMessageImportance.VeryImportant));                    
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            Vector2 var_PositionShadow = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X / 2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y);
             _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], var_PositionShadow, Color.White);

             Vector2 var_PositionState = new Vector2(this.Position.X, this.Position.Y) + new Vector2(-13, -7);
             if (this is PlayerObject)
             {
                 _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/CreatureState"], var_PositionState, Color.BlueViolet);//Color.DarkOrange);
             }
             else
             {
                 _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/CreatureState"], var_PositionState, Color.Red);
             }
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
            this.drawEquipment(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }

        private void drawEquipment(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X / 2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y);
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                var_EquipmentObject.Position = new Vector3(var_Position, 0);
                var_EquipmentObject.drawWearingEquipment(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
            }
        }
    }
}
