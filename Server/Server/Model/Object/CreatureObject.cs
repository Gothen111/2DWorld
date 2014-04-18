using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
