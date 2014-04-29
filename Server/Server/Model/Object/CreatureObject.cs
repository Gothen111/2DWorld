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
            this.addEquipmentObject(Server.Factories.EquipmentFactory.equipmentFactory.createEquipmentObject(Factories.FactoryEnums.WeaponEnum.Sword));
        }

        public override void update()
        {
            base.update();
        }

        public void addEquipmentObject(EquipmentObject _EquipmentObject)
        {
            this.equipment.Add(_EquipmentObject);
        }

        public void attackWithWeaponInHand()
        {

        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], new Vector2(this.Position.X - this.Size.X / 2, this.Position.Y - this.Size.Y), Color.White); 
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
