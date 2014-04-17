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

        public EquipmentObject createEquipmentObject(WeaponEnum _WeaponEnum)
        {
            EquipmentObject equipmentObject = new EquipmentObject();
            equipmentObject.Scale = 1;
            equipmentObject.Velocity = new Vector3(0, 0, 0);
            equipmentObject.WeaponEnum = _WeaponEnum;

            switch (_WeaponEnum)
            {
                case WeaponEnum.Sword:
                    {
                        equipmentObject.NormalDamage = 2;
                        break;
                    }
            }

            return equipmentObject;
        }
    }
}
