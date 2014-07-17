using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;

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

        protected List<EquipmentObject> Equipment
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
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("equipment", equipment, typeof(List<EquipmentObject>));

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
            this.swingWeapon();
        }

        public void swingWeapon()
        {
            GameLibrary.Model.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = null;
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentWeapon)
                {
                    //if (((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum == GameLibrary.Factory.FactoryEnums.WeaponEnum.Sword)
                    //{
                        var_EquipmentWeaponForAttack = ((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject);
                    //}
                }
            }
            if (var_EquipmentWeaponForAttack != null && var_EquipmentWeaponForAttack.isAttackReady())
            {
                List<Object> var_Objects = GameLibrary.Model.Map.World.World.world.getObjectsInRange(this.Position, var_EquipmentWeaponForAttack.Range, var_EquipmentWeaponForAttack.SearchFlags);
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
                    var_EquipmentWeaponForAttack.attack();
                }
            }
        }

        public override int calculateDamage(int _DamageAmount)
        {
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentArmor)
                {
                    _DamageAmount = _DamageAmount / ((GameLibrary.Model.Object.Equipment.EquipmentArmor)var_EquipmentObject).NormalArmor;
                }
            }
            return _DamageAmount;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            Vector3 var_DrawPositionExtra = Vector3.Zero;
            if(this.Animation != null)
                var_DrawPositionExtra = this.Animation.drawPositionExtra();
             Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X/2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y) + new Vector2(var_DrawPositionExtra.X, var_DrawPositionExtra.Y);
             _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], var_Position, Color.White);    
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
            this.drawEquipment(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }

        private void drawEquipment(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            Vector3 var_DrawPositionExtra = Vector3.Zero;
            if (this.Animation != null)
                var_DrawPositionExtra = this.Animation.drawPositionExtra();
            Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X / 2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y) + new Vector2(var_DrawPositionExtra.X, var_DrawPositionExtra.Y);
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                var_EquipmentObject.Position = new Vector3(var_Position, 0);
                var_EquipmentObject.Animation = this.Animation;
                var_EquipmentObject.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
            }
        }
    }
}
