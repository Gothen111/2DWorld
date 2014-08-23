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
            this.inventory = new Inventory.Inventory();
        }

        public CreatureObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { 
            this.inventory = (Inventory.Inventory)info.GetValue("inventory", typeof(Inventory.Inventory));
            this.name = (String)info.GetValue("name", typeof(String));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("inventory", inventory, typeof(Inventory.Inventory));
            info.AddValue("name", this.name, typeof(String));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
        }

        public override void attack()
        {
            base.attack();
            this.swingWeapon(Model.Object.Equipment.Attack.AttackType.Front);
        }
        
        /*public GameLibrary.Model.Object.Equipment.EquipmentWeapon getWeaponInHand()
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
        }*/


        public void swingWeapon(Equipment.Attack.AttackType _AttackType)
        {
            GameLibrary.Model.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = this.Body.attack();//null;// this.getWeaponInHand();
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

        /*public GameLibrary.Model.Object.Equipment.EquipmentArmor getWearingArmor()
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
        }*/



        public override float calculateDamage(float _DamageAmount)
        {
            /*if (this.equipment != null)
            {
                GameLibrary.Model.Object.Equipment.EquipmentArmor var_EquipmentArmor = this.getWearingArmor();
                if (var_EquipmentArmor != null)
                {
                    _DamageAmount = _DamageAmount / ((float)((GameLibrary.Model.Object.Equipment.EquipmentArmor)var_EquipmentArmor).NormalArmor);
                }
            }*/

            _DamageAmount = _DamageAmount / ((float)this.Body.getDefence());
            return _DamageAmount;
        }

        public void addItemObjectToInventory(ItemObject _ItemObject)
        {
            if(this.inventory.addItemObjectToInventory(_ItemObject))
            {
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
            }
        }

        public void guiSetItemToInventory(ItemObject _ItemObject, int _FieldId)
        {
            this.inventory.itemDropedInInventory(this, _ItemObject, _FieldId);
            this.inventory.InventoryChanged = true;
        }

        public void setItemFromEquipmentToInventory(int _EquipmentPosition, int _InventoryPosition)
        {
            EquipmentObject var_ToRemove = this.Body.getEquipmentAt(_EquipmentPosition);
            if (this.inventory.addItemObjectToInventoryAt(var_ToRemove, _InventoryPosition))
            {
                this.Body.removeEquipment(var_ToRemove);
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateAnimatedObjectBodyMessage(this.Id, this.Body), GameMessageImportance.VeryImportant));                    
            }
        }

        public void guiSetItemToEquipment(ItemObject _ItemObject, int _FieldId)
        {
            if (_ItemObject.PositionInInventory != -1)
            {
                if(this.inventory.Items.Contains(_ItemObject))
                {
                    Event.EventList.Add(new Event(new GameLibrary.Connection.Message.CreatureInventoryToEquipmentMessage(this.Id, _ItemObject.PositionInInventory, _FieldId), GameMessageImportance.VeryImportant));
                }
                else
                {
                    //Kommt nicht aus dem Inventar, sondern woadners he. Aus anderem Inv., ode Equip.
                }
            }
            else
            {
                // Kommt aus der world ;)
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
                        //this.equipment.Add((EquipmentObject)var_ItemObject);
                        var_ItemObject.PositionInInventory = _EquipmentPosition;
                        this.Body.setEquipmentObject((EquipmentObject)var_ItemObject);
                        var_ToRemove = var_ItemObject;
                    }
                }
            }
            if (var_ToRemove != null)
            {
                this.inventory.Items.Remove(var_ToRemove);
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant));
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateAnimatedObjectBodyMessage(this.Id, this.Body), GameMessageImportance.VeryImportant));                    
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
        }
    }
}
