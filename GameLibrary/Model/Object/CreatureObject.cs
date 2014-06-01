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
        //protected Inventory inventory;
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
            this.LayerDepth = 0.1f;
            this.equipment = new List<EquipmentObject>();
            this.addEquipmentObject(GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(GameLibrary.Factory.FactoryEnums.WeaponEnum.Sword));
        }

        public CreatureObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
            updateEquippment();
        }

        public void updateEquippment()
        {
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is GameLibrary.Model.Object.Equipment.EquipmentWeapon)
                {
                    if (((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum != null)
                    {
                        ((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).update();
                    }
                }
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
                    if (((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum != null)
                    {
                        if (((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum == GameLibrary.Factory.FactoryEnums.WeaponEnum.Sword)
                        {
                            var_EquipmentWeaponForAttack = ((GameLibrary.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject);
                        }
                    }
                }
            }
            if (var_EquipmentWeaponForAttack != null && var_EquipmentWeaponForAttack.isAttackReady())
            {
                List<LivingObject> var_LivingObjects = GameLibrary.Model.Map.World.World.world.getObjectsInRange(this.Position, var_EquipmentWeaponForAttack.Range, var_EquipmentWeaponForAttack.SearchFlags);
                var_LivingObjects.Remove(this);
                foreach (LivingObject var_LivingObject in var_LivingObjects)
                {
                    this.attackLivingObject(var_LivingObject, var_EquipmentWeaponForAttack.NormalDamage);
                }
                if (var_LivingObjects.Count > 0)
                {
                    var_EquipmentWeaponForAttack.attack();
                }
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            Vector3 var_DrawPositionExtra = this.Animation.drawPositionExtra();
             Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X/2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y) + new Vector2(var_DrawPositionExtra.X, var_DrawPositionExtra.Y);
             _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], var_Position, Color.White); 
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
