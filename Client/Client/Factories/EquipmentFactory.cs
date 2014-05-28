using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Object;
using Client.Factories.FactoryEnums;
using Microsoft.Xna.Framework;

namespace Client.Factories
{
    class EquipmentFactory
    {
        public static EquipmentFactory equipmentFactory = new EquipmentFactory();

        private EquipmentFactory()
        {
        }

        public EquipmentObject createEquipmentObject()
        {
            EquipmentObject equipmentObject = new EquipmentObject();
            equipmentObject.Scale = 1;
            equipmentObject.Velocity = new Vector3(0, 0, 0);

            return equipmentObject;
        }

        public Model.Object.Equipment.EquipmentWeapon createEquipmentWeaponObject(WeaponEnum _WeaponEnum)
        {
            Model.Object.Equipment.EquipmentWeapon equipmentWeaponObject = new Model.Object.Equipment.EquipmentWeapon();
            equipmentWeaponObject.Scale = 1;
            equipmentWeaponObject.Velocity = new Vector3(0, 0, 0);


            switch (_WeaponEnum)
            {
                case WeaponEnum.Sword:
                    {
                        equipmentWeaponObject.NormalDamage = 2;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        equipmentWeaponObject.Range = 50;
                        equipmentWeaponObject.AttackSpeed = 0;
                        equipmentWeaponObject.AttackSpeedMax = 60;
                        equipmentWeaponObject.SearchFlags.Add(new Model.Map.World.SearchFlags.NpcObjectFlag());
                        break;
                    }
            }

            return equipmentWeaponObject;
        }
    }
}
