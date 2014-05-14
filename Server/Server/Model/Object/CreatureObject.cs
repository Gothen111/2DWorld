using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace Server.Model.Object
{
    class CreatureObject : LivingObject
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
            this.addEquipmentObject(Server.Factories.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(Factories.FactoryEnums.WeaponEnum.Sword));
        }

        public override void update()
        {
            base.update();
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
            Server.Model.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = null;
            foreach (EquipmentObject var_EquipmentObject in this.equipment)
            {
                if (var_EquipmentObject is Server.Model.Object.Equipment.EquipmentWeapon)
                {
                    if (((Server.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum != null)
                    {
                        if (((Server.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject).WeaponEnum == Factories.FactoryEnums.WeaponEnum.Sword)
                        {
                            var_EquipmentWeaponForAttack = ((Server.Model.Object.Equipment.EquipmentWeapon)var_EquipmentObject);
                        }
                    }
                }
            }
            if (var_EquipmentWeaponForAttack != null)
            {
                List<LivingObject> var_LivingObjects = this.World.getObjectsInRange(this.Position, var_EquipmentWeaponForAttack.Range);
                var_LivingObjects.Remove(this);
                foreach (LivingObject var_LivingObject in var_LivingObjects)
                {
                    this.attackLivingObject(var_LivingObject, var_EquipmentWeaponForAttack.NormalDamage);
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
