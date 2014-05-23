using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;
using Server.Factories.FactoryEnums;
using Microsoft.Xna.Framework;

namespace Server.Factories
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

        public Server.Model.Object.Equipment.EquipmentWeapon createEquipmentWeaponObject(WeaponEnum _WeaponEnum)
        {
            Server.Model.Object.Equipment.EquipmentWeapon equipmentWeaponObject = new Server.Model.Object.Equipment.EquipmentWeapon();
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
                        break;
                    }
            }

            return equipmentWeaponObject;
        }
    }
}
